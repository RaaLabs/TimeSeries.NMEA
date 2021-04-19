#!/bin/bash
docker build --no-cache -f ./Source/Dockerfile -t raaedge.azurecr.io/connectors-nmea:test .
docker push raaedge.azurecr.io/connectors-nmea:test