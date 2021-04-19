#!/bin/bash
docker build -f ./Source/Dockerfile -t raalabs/connectors-nmea . --build-arg CONFIGURATION="Debug"
iotedgehubdev start -d deployment.json -v