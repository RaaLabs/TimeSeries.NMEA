Feature: NMEALineHandler

    Scenario: Handling incoming events
        Given a handler of type NMEALineHandler
        When the following events of type NMEASentenceReceived is produced
            | sentence                                                                 |
            | $GPRMC,050318.004,A,5954.110,N,01043.074,E,038.9,241.4,090920,000.0,W*7A |

        Then the following events of type EventParsed is produced
            | Talker | Tag             | Timetamp      | Value     |
            | GPRMC  | SpeedOverGround | 1617835385039 | 20.01189  |
            | GPRMC  | Latitude        | 1617835385039 | 59.901833 |
            | GPRMC  | Longitude       | 1617835385039 | 10.7179   |
