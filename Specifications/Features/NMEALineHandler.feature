Feature: NMEALineHandler

    Background: Given handler
        Given a handler of type NMEALineHandler

    Scenario: Handling incoming DPT events
        When the following events of type NMEASentenceReceived is produced
            | sentence                 |
            | $SDDPT,5.2,0.0,10.0*63   |
            | $SDDPT,0002.8,0010.8*54  |
            | $SDDPT,0022.9,-0010.8*7A |
        Then the following events of type EventParsed is produced
            | Talker | Tag            | Value |
            | SDDPT  | WaterDepth     | 5.2   |
            | SDDPT  | WaterDepth     | 13.6  |
            | SDDPT  | DepthBelowKeel | 12.1  |

    Scenario: Handling incoming DPT events with missing values
        When the following events of type NMEASentenceReceived is produced
            | sentence           |
            | $SDDPT,0022.9,*40  |
            | $SDDPT,,-0010.8*6D |
        Then the following events of type EventParsed is produced
            | Talker | Tag | Value |


    Scenario: Handling incoming GGA events
        When the following events of type NMEASentenceReceived is produced
            | sentence                                                              |
            | $GPGGA,114757.00,4041.482,N,07408.271,W,1,08,01.0,+0022,M,-034,M,,*47 |

        Then the following events of type EventParsed is produced
            | Talker | Tag           | Value                                    |
            | GPGGA  | GPSsatellites | 8.0                                      |
            | GPGGA  | HDOP          | 1.0                                      |
            | GPGGA  | Latitude      | 40.6913                                  |
            | GPGGA  | Longitude     | -74.1378                                 |
            | GPGGA  | Position      | {Latitude: 40.6913, Longitude: -74.1378} |

    Scenario: Handling incoming GNS events
        When the following events of type NMEASentenceReceived is produced
            | sentence                                                              |
            | $GPGNS,114757.00,4041.482,N,07408.271,W,1,08,01.0,+0022,M,-034,M,,*5C |

        Then the following events of type EventParsed is produced
            | Talker | Tag           | Value                                    |
            | GPGNS  | GPSsatellites | 8.0                                      |
            | GPGNS  | HDOP          | 1.0                                      |
            | GPGNS  | Latitude      | 40.6913                                  |
            | GPGNS  | Longitude     | -74.1378                                 |
            | GPGNS  | Position      | {Latitude: 40.6913, Longitude: -74.1378} |

    Scenario: Handling incoming GLL events
        When the following events of type NMEASentenceReceived is produced
            | sentence                                       |
            | $GPGLL,4041.482,N,07408.271,W,114757.00,A,A*7A |

        Then the following events of type EventParsed is produced
            | Talker | Tag       | Value                                    |
            | GPGLL  | Latitude  | 40.6913                                  |
            | GPGLL  | Longitude | -74.1378                                 |
            | GPRMC  | Position  | {Latitude: 40.6913, Longitude: -74.1378} |

    Scenario: Handling incoming RMC events
        When the following events of type NMEASentenceReceived is produced
            | sentence                                                                 |
            | $GPRMC,050318.004,A,5954.110,S,01043.074,E,038.9,241.4,090920,000.0,W*67 |

        Then the following events of type EventParsed is produced
            | Talker | Tag             | Value                                      |
            | GPRMC  | SpeedOverGround | 20.01189                                   |
            | GPRMC  | Latitude        | -59.901833                                 |
            | GPRMC  | Longitude       | 10.7179                                    |
            | GPRMC  | Position        | {Latitude: -59.901833, Longitude: 10.7179} |

    Scenario: Handling incoming HDT events
        When the following events of type NMEASentenceReceived is produced
            | sentence          |
            | $HEHDT,144.8,T*26 |

        Then the following events of type EventParsed is produced
            | Talker | Tag         | Value |
            | HEHDT  | HeadingTrue | 144.8 |

    Scenario: Handling incoming ROT events
        When the following events of type NMEASentenceReceived is produced
            | sentence          |
            | $HEROT,000.0,A*2B |

        Then the following events of type EventParsed is produced
            | Talker | Tag        | Value |
            | HEROT  | RateOfTurn | 0.00  |

    Scenario: Handling incoming MWV  events
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

    Scenario: Handling incoming VBW events
        When the following events of type NMEASentenceReceived is produced
            | sentence                                    |
            | $INVBW,00.1,00.0,A,00.0,00.0,A,00.0,V,,V*5B |

        Then the following events of type EventParsed is produced
            | Talker | Tag                           | Value     |
            | INVBW  | LongitudinalSpeedThroughWater | 0.0514444 |
            | INVBW  | TransverseSpeedThroughWater   | 0.0       |
            | INVBW  | LongitudinalSpeedOverGround   | 0.0       |
            | INVBW  | TransverseSpeedOverGround     | 0.0       |
            | INVBW  | SpeedThroughWater             | 0.0514444 |

    Scenario: Handling incoming VTG events
        When the following events of type NMEASentenceReceived is produced
            | sentence                          |
            | $GPVTG,138,T,,,00.0,N,00.0,K,A*54 |

        Then the following events of type EventParsed is produced
            | Talker | Tag                  | Value |
            | GPVTG  | CourseOverGroundTrue | 138.0 |
            | GPVTG  | SpeedOverGround      | 0.0   |

    Scenario: Handling incoming VHW events
        When the following events of type NMEASentenceReceived is produced
            | sentence                                 |
            | $VDVHW,119.9,T,120.1,M,7.76,N,14.37,K*72 |

        Then the following events of type EventParsed is produced
            | Talker | Tag               | Value   |
            | VDVHW  | HeadingTrue       | 119.9   |
            | VDVHW  | HeadingMagnetic   | 120.1   |
            | VDVHW  | SpeedThroughWater | 3.99208 |

    Scenario: Handling incoming GLL events with missing checksum
        When the following events of type NMEASentenceReceived is produced
            | sentence                                    |
            | $GPGLL,4041.482,S,07408.271,E,114757.00,A,A |

        Then the following events of type EventParsed is produced
            | Talker | Tag       | Value                                    |
            | GPGLL  | Latitude  | -40.6913                                 |
            | GPGLL  | Longitude | 74.1378                                  |
            | GPRMC  | Position  | {Latitude: -40.6913, Longitude: 74.1378} |

    Scenario: Handling incoming position events with missing Latitude
        When the following events of type NMEASentenceReceived is produced
            | sentence                           |
            | $GPGLL,,,07408.271,E,114757.00,A,A |
        Then the following events of type EventParsed is produced
            | Talker | Tag | Value |

    Scenario: Handling incoming position events with missing Longitude
        When the following events of type NMEASentenceReceived is produced
            | sentence                          |
            | $GPGLL,4041.482,S,,,114757.00,A,A |

        Then the following events of type EventParsed is produced
            | Talker | Tag | Value |

    Scenario: Handling incoming position events with missing Longitude. However it contains other valid values
        When the following events of type NMEASentenceReceived is produced
            | sentence                                                    |
            | $GPRMC,050318.004,A,5954.110,S,,,038.9,241.4,090920,000.0,W |

        Then the following events of type EventParsed is produced
            | Talker | Tag             | Value    |
            | GPRMC  | SpeedOverGround | 20.01189 |
