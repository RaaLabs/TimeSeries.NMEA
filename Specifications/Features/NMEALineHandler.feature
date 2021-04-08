Feature: NMEALineHandler

    Scenario: Handling incoming DPT events
        Given a handler of type NMEALineHandler
        When the following events of type NMEASentenceReceived is produced
            | sentence               |
            | $SDDPT,5.2,0.0,10.0*63 |

        Then the following events of type EventParsed is produced
            | Talker | Tag        | Value |
            | SDDPT  | WaterDepth | 5.2   |

    Scenario: Handling incoming GGA events
        Given a handler of type NMEALineHandler
        When the following events of type NMEASentenceReceived is produced
            | sentence                                                              |
            | $GPGGA,114757.00,4041.482,N,07408.271,W,1,08,01.0,+0022,M,-034,M,,*47 |

        Then the following events of type EventParsed is produced
            | Talker | Tag           | Value                                     |
            | GPGGA  | GPSsatellites | 8.0                                       |
            | GPGGA  | HDOP          | 1.0                                       |

    Scenario: Handling incoming RMC events
        Given a handler of type NMEALineHandler
        When the following events of type NMEASentenceReceived is produced
            | sentence                                                                 |
            | $GPRMC,050318.004,A,5954.110,N,01043.074,E,038.9,241.4,090920,000.0,W*7A |

        Then the following events of type EventParsed is produced
            | Talker | Tag             | Value                                     |
            | GPRMC  | SpeedOverGround | 20.01189                                  |
            | GPRMC  | Latitude        | 59.901833                                 |
            | GPRMC  | Longitude       | 10.7179                                   |
            | GPRMC  | Position        | {Latitude: 59.901833, Longitude: 10.7179} |

    Scenario: Handling incoming HDT events
        Given a handler of type NMEALineHandler
        When the following events of type NMEASentenceReceived is produced
            | sentence          |
            | $HEHDT,144.8,T*26 |

        Then the following events of type EventParsed is produced
            | Talker | Tag         | Value |
            | HEHDT  | HeadingTrue | 144.8 |

    Scenario: Handling incoming ROT events
        Given a handler of type NMEALineHandler
        When the following events of type NMEASentenceReceived is produced
            | sentence          |
            | $HEROT,000.0,A*2B |

        Then the following events of type EventParsed is produced
            | Talker | Tag        | Value |
            | HEROT  | RateOfTurn | 0.00  |

    Scenario: Handling incoming MWV  events
        Given a handler of type NMEALineHandler
        When the following events of type NMEASentenceReceived is produced
            | sentence                   |
            | $WIMWV,249.0,R,01.5,M,A*1B |
            | $IIMWV,16.6,T,14.7,N,A*38  |
            | $IIMWV,16.6,T,14.7,K,A*3D  |


        Then the following events of type EventParsed is produced
            | Talker | Tag               | Value    |
            | WIMWV  | WindAngleRelative | 249.0    |
            | WIMWV  | WindSpeedRelative | 1.5      |
            | IIMWV  | WindAngleTrue     | 16.6     |
            | IIMWV  | WindSpeedTrue     | 7.562333 |
            | IIMWV  | WindAngleTrue     | 16.6     |
            | IIMWV  | WindSpeedTrue     | 4.083333 |



