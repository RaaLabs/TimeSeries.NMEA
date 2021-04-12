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
            | HEHDT      | HeadingTrue | 5.0   | 1617915312000 |
            | HEVHW      | HeadingTrue | 5.2   | 1617915312000 |
        Then the following events of type NMEADatapointOutput is produced
            | Source | Tag         | Value | Timestamp     |
            | NMEA   | HeadingTrue | 5.0   | 1617915312000 |

    Scenario: Handling incoming parsed events with different priority. Where second event received has highest priority.
        When the following events of type EventParsed is produced
            | Identifier | Tag         | Value | Timestamp     |
            | HEVHW      | HeadingTrue | 5.2   | 1617915312000 |
            | HEHDT      | HeadingTrue | 5.0   | 1617915312000 |
        Then the following events of type NMEADatapointOutput is produced
            | Source | Tag         | Value | Timestamp     |
            | NMEA   | HeadingTrue | 5.2   | 1617915312000 |
            | NMEA   | HeadingTrue | 5.0   | 1617915312000 |
