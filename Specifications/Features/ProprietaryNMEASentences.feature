Feature: ProprietaryNMEASentences

    Background: Given handler
        Given a handler of type NMEALineHandler

    Scenario: Handling incoming _00CT events
        When the following events of type NMEASentenceReceived is produced
            | sentence                     |
            | $PV00CT,03,0.0,3500.0,2000.0 |
        Then the following events of type EventParsed is produced
            | Talker | Tag              | Value  |
            | PV00CT | FlowMainEngineIn | 0.0    |
            | PV00CT | FlowAuxEngineIn  | 3500.0 |
            | PV00CT | FlowBoilerIn     | 2000.0 |

    Scenario: Handling incoming TEMPAI events
        When the following events of type NMEASentenceReceived is produced
            | sentence                       |
            | $PVTEMPAI,03,0.0,3500.0,2000.0 |
        Then the following events of type EventParsed is produced
            | Talker   | Tag                  | Value  |
            | PVTEMPAI | FlowTempMainEngineIn | 0.0    |
            | PVTEMPAI | FlowTempAuxEngineIn  | 3500.0 |
            | PVTEMPAI | FlowTempBoilerIn     | 2000.0 |

    Scenario: Handling incoming _00AI events
        When the following events of type NMEASentenceReceived is produced
            | sentence                         |
            | $PV00AI,08,2000,12,-32,,,V,,S*02 |
        Then the following events of type EventParsed is produced
            | Talker | Tag         | Value |
            | PV00AI | ShaftThrust | 2000  |
            | PV00AI | ShaftRpm    | 12.0  |
            | PV00AI | ShaftTorque | -32.0 |



