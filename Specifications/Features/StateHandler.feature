Feature: StateHandler

    Background: Given a handler
        Given the prioritized tags
            | Tag         | Priority                | Threshold |
            | HeadingTrue | HEHDT,HEVHW,GPHDT,GPVHW | 60000     |
        And a handler of type StateHandler

    Scenario: Handling incoming parsed events
        When the following events of type EventParsed is produced
            | Identifier | Tag        | Value | Timestamp     |
            | SDDPT      | WaterDepth | 5.2   | 1617915312000 |
        Then the following events of type NMEADatapointOutput is produced
            | Source | Tag        | Value | Timestamp     |
            | NMEA   | WaterDepth | 5.2   | 1617915312000 |

    Scenario: Handling incoming parsed events with different priority. Where first event received has highest priority.
        When the following events of type EventParsed is produced
            | Identifier | Tag         | Value | Timestamp     |
            | HEHDT      | HeadingTrue | 1.0   | 1617915312000 |
            | HEVHW      | HeadingTrue | 2.0   | 1617915312000 |
        Then the following events of type NMEADatapointOutput is produced
            | Source | Tag         | Value | Timestamp     |
            | NMEA   | HeadingTrue | 1.0   | 1617915312000 |

    Scenario: Handling incoming parsed events with different priority. Where second event received has highest priority.
        When the following events of type EventParsed is produced
            | Identifier | Tag         | Value | Timestamp     |
            | HEVHW      | HeadingTrue | 2.0   | 1617915312000 |
            | HEHDT      | HeadingTrue | 1.0   | 1617915312000 |
        Then the following events of type NMEADatapointOutput is produced
            | Source | Tag         | Value | Timestamp     |
            | NMEA   | HeadingTrue | 2.0   | 1617915312000 |
            | NMEA   | HeadingTrue | 1.0   | 1617915312000 |

    Scenario: Handling stall: Time between next event above threshold
        The first event has highest priority. Second event has
        lower priority and recevied after 61 seconds.

        When the following events of type EventParsed is produced
            | Identifier | Tag         | Value | Timestamp     |
            | HEHDT      | HeadingTrue | 1.0   | 1600000000000 |
            | HEVHW      | HeadingTrue | 2.0   | 1600000061000 |
        Then the following events of type NMEADatapointOutput is produced
            | Source | Tag         | Value | Timestamp     |
            | NMEA   | HeadingTrue | 1.0   | 1600000000000 |
            | NMEA   | HeadingTrue | 2.0   | 1600000061000 |

    Scenario: Handling stall: Time between next event below threshold
        The first event has highest priority. Second event has
        lower priority and recevied after 59 seconds.
        When the following events of type EventParsed is produced
            | Identifier | Tag         | Value | Timestamp     |
            | HEHDT      | HeadingTrue | 1.0   | 1600000000000 |
            | HEVHW      | HeadingTrue | 2.0   | 1600000059000 |
        Then the following events of type NMEADatapointOutput is produced
            | Source | Tag         | Value | Timestamp     |
            | NMEA   | HeadingTrue | 1.0   | 1600000000000 |