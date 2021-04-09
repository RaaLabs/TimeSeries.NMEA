Feature: StateHandler

    Scenario: Handling incoming parsed events
        Given a handler of type StateHandler
        When the following events of type EventParsed is produced
            | Identifier | Tag        | Value | Timestamp     |
            | SDDPT      | WaterDepth | 5.2   | 1617915312000 |
        Then the following events of type NMEADatapointOutput is produced
            | Source | Tag        | Value | Timestamp     |
            | NMEA   | WaterDepth | 5.2   | 1617915312000 |
