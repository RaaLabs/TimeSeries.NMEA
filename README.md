# Connectors.NMEA
[![.NET 5.0](https://github.com/RaaLabs/Connectors.NMEA/actions/workflows/dotnet-core.yml/badge.svg)](https://github.com/RaaLabs/Connectors.NMEA/actions/workflows/dotnet-core.yml)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=RaaLabs_Connectors.NMEA&metric=sqale_rating&token=8897be6a6a24c254c635617801e86f08e68dd19f)](https://sonarcloud.io/dashboard?id=RaaLabs_Connectors.NMEA)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=RaaLabs_Connectors.NMEA&metric=coverage)](https://sonarcloud.io/dashboard?id=RaaLabs_Connectors.NMEA)

This document describes the Connectors.NMEA module for RaaLabs Edge. For information regarding the specifics of NMEA check out [NMEA Revealed](https://gpsd.gitlab.io/gpsd/NMEA.html)

## What does it do?

NMEA receives NMEA sentences over TCP or UDP streams. Sentences are parsed using the NMEA identifier, the supported NMEA identifiers can be found [here](https://github.com/RaaLabs/Connectors.NMEA/tree/master/Source/SentenceFormats).

If a specific tag, e.g. SpeedOverGround, are produced from multiple NMEA talkers and identifiers it will be prioritized using the configuration [prioritized.json](https://github.com/RaaLabs/Connectors.NMEA/blob/master/Source/data/prioritized.json)

The connector are producing events of type [OutputName("output")] and should be routed to [IdentityMapper](https://github.com/RaaLabs/IdentityMapper).

## Configuration

The module is configured using two json files. `connector.json` represents the connection to the TCP or UDP stream using IP and port.

```json
{
    "ip": "127.0.0.1",
    "port": 8888,
    "protocol": 1
}
```

`prioritized.json` holds the priority for each NMEA tag concerning the  NMEA talker and identifier where the priority is indicated by the position in the list.

```json
{
    "Latitude": {
        "priority": ["GPGGA", "GPGLL", "GPGNS", "GPRMC", "GPRMA"],
        "threshold": 60000
    },
    "Longitude": {
        "priority": ["GPGGA", "GPGLL", "GPGNS", "GPRMC", "GPRMA"],
        "threshold": 60000
    },
    "RateOfTurn": {
        "priority": ["HEROT", "GPROT"],
        "threshold": 60000
    },
    "SpeedThroughWater": {
        "priority": ["VDVHW", "VDVBW", "GPVHW", "GPVBW", "VWVHW", "VWVBW"],
        "threshold":00
    },
    "HeadingTrue": {
        "priority": ["HEHDT", "HEVHW", "GPHDT", "GPVHW", "SDHDT", "SDVHW"],
        "threshold": 60000
    }
}
```

## IoT Edge Deployment

### $edgeAgent

In your `deployment.json` file, you will need to add the module. For more details on modules in IoT Edge, go [here](https://docs.microsoft.com/en-us/azure/iot-edge/module-composition).

The module has persistent state and it is assuming that this is in the `data` folder relative to where the binary is running.
Since this is running in a containerized environment, the state is not persistent between runs. To get this state persistent, you'll
need to configure the deployment to mount a folder on the host into the data folder.

In your `deployment.json` file where you added the module, inside the `HostConfig` property, you should add the
volume binding.

```json
"Binds": [
    "<mount-path>:/app/data"
]
```

```json
{
    "modulesContent": {
        "$edgeAgent": {
            "properties.desired.modules.NMEA": {
                "settings": {
                    "image": "<repo-name>/connectors-nmea:<tag>",
                    "createOptions": "{\"HostConfig\":{\"Binds\":[\"<mount-path>:/app/data\"]}}"
                },
                "type": "docker",
                "version": "1.0",
                "status": "running",
                "restartPolicy": "always"
            }
        }
    }
}
```

For production setup, the bind mount can be replaced by a docker volume.

### $edgeHub

The routes in edgeHub can be specified like the example below.

```json
{
    "$edgeHub": {
        "properties.desired.routes.NMEAToIdentityMapper": "FROM /messages/modules/NMEA/outputs/output INTO BrokeredEndpoint(\"/modules/IdentityMapper/inputs/events\")",
    }
}
```
