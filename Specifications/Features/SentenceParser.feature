Feature: SentenceParser

    Scenario: Parse unsupported sentences
        When sentence "$AAAAA,5.2,0.0,10.0*63" is parsed
        Then the exception "UnsupportedSentenceException" is thrown

    Scenario: Parse invalid sentence missing $ at start
        When sentence "AAAAA,5.2,0.0,10.0*63" is parsed
        Then the exception "InvalidSentenceException" is thrown
    
    Scenario: Parse valid sentence with invalid checksum
        When sentence "$SDDPT,5.2,0.0,10.0*64" is parsed
        Then the sentence should be valid
        And the exception "InvalidSentenceChecksumException" is thrown
    