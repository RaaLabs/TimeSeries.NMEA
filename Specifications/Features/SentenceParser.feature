Feature: SentenceParser

    Scenario: Parse unsupported sentences
        When sentence "$AAAAA,5.2,0.0,10.0*63" is parsed
        Then the exception "UnsupportedSentenceException" is thrown with message "Talker 'AA' with identifier 'AAA' is not supported in sentence '$AAAAA,5.2,0.0,10.0*63'"

    Scenario: Parse invalid sentence missing $ at start
        When sentence "AAAAA,5.2,0.0,10.0*63" is parsed
        Then the exception "InvalidSentenceException" is thrown with message "Sentence 'AAAAA,5.2,0.0,10.0*63' is invalid. Please refer to the standard for NMEA."
    
    Scenario: Parse valid sentence with invalid checksum
        When sentence "$SDDPT,5.2,0.0,10.0*64" is parsed
        Then the sentence should be valid
        And the exception "InvalidSentenceChecksumException" is thrown
    