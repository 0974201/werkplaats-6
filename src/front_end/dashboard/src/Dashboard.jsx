// eslint-disable-next-line no-unused-vars
import React from 'react';
import mqtt from 'mqtt';
import './Dashboard.css'
import CraneVisualisation from "./CraneVisualisation/CraneVisualisation.jsx";
import DataTable from "./DataTable/DataTable.jsx";
import InputVisualisation from "./InputVisualisation/InputVisualisation.jsx";
import {useEffect, useState} from "react";

export default function Dashboard() {
    const [craneInfo, setCraneInfo] = useState({
        "meta": {
            "topic":"crane/state",
            "isActive":"bool"
        },
        "absolutePosition": {
            "x": 1,
            "y": 1,
            "z": 1
        },
        "components": [
            {
                "component": "hoist",
                "isActive": true,
                "isConnected": true,
                "absolutePosition": {
                    "x": 1,
                    "y": 1,
                    "z": 1
                },
                "speed": {
                    "activeAcceleration": {
                        "y": false
                    },
                    "acceleration": {
                        "y": 1
                    },
                    "speed": {
                        "y": 1
                    }
                }
            },
            {
                "component": "trolley",
                "isActive": true,
                "absolutePosition": {
                    "x": 1,
                    "y": 1,
                    "z": 1
                },
                "speed": {
                    "activeAcceleration": {
                        "x": false
                    },
                    "acceleration": {
                        "x": 1
                    },
                    "speed": {
                        "x": 1
                    }
                }
            },
            {
                "component": "boom",
                "isActive": true,
                "absolutePosition": {
                    "x": 1,
                    "y": 1,
                    "z": 1
                },
                "speed": {
                    "activeAcceleration": {
                        "x": false,
                        "y": false
                    },
                    "acceleration": {
                        "x": 1,
                        "y": 1
                    },
                    "speed": {
                        "X": 1,
                        "y": 0
                    }
                }
            },
            {
                "component": "gantry",
                "isActive": false,
                "absolutePosition": {
                    "x": 1,
                    "y": 1,
                    "z": 1
                },
                "speed": {
                    "activeAcceleration": {
                        "z": false
                    },
                    "acceleration": {
                        "z": 1
                    },
                    "speed": {
                        "z": 2
                    }
                }
            }
        ],
        "container": {
            "id":13,
            "isConnected":false,
            "absolutePosition":{
                "x":1,
                "y":1,
                "z":1
            },
            "speed":{
                "speed":{
                    "x":1,
                    "y":1,
                    "z":1
                }
            }
        },
        "commands": [
            {
                "target": "str",
                "command": 0
            }
        ]
    })

    const [trolleyCommand, setTrolleyCommand] = useState(0)
    const [gantryCommand, setGantryCommand] = useState(0)
    const [hoistCommand, setHoistCommand] = useState(0)
    const [boomCommand, setBoomCommand] = useState(0)
    const [emergencyCommand, setEmergencyCommand] = useState(false)
    const [spreaderCommand, setSpreaderCommand] = useState("false")

    // gebruikte bron: https://stackoverflow.com/questions/75312551/how-to-connect-hivemqtt-to-react-app-using-mqtt-package
    useEffect(() => {
        const options = {
            protocol: "wss",
            username: "Dashboardmqtt",
            password: "Dashboard1234",
        };

        const client = mqtt.connect("wss://c0bbe3829ad14fe3b24e5c51247f57c1.s2.eu.hivemq.cloud:8884/mqtt", options);
        client.on("connect", function () {
            console.log("Connected");
        });
        client.on("error", function (error) {
            console.log("ERROR", error);
        });
        client.on("message", (topic,message)=>{
            switch (topic) {
                case "crane/state":
                    setCraneInfo(JSON.parse(message.toString()))
                    break
                case "crane/components/trolley/command":
                    setTrolleyCommand(JSON.parse(message.toString()).msg.command)
                    break
                case "crane/components/gantry/command":
                    setGantryCommand(JSON.parse(message.toString()).msg.command)
                    break
                case "crane/components/hoist/command":
                    setHoistCommand(JSON.parse(message.toString()).msg.command)
                    break
                case "crane/components/boom/command":
                    setBoomCommand(JSON.parse(message.toString()).msg.command)
                    break
                case "meta/emergency_button":
                    setEmergencyCommand(JSON.parse(message.toString()).msg.isPressed)
                    break
                case "crane/connectrequest":
                    setSpreaderCommand(JSON.parse(message.toString()).msg.isconnecting)
                    break
                default:
                    console.log(JSON.parse(message.toString()))
            }
            console.log("RECEIVE", JSON.parse(message.toString()))

        });
        client.subscribe('crane/components/trolley/command');
        client.subscribe('crane/components/gantry/command');
        client.subscribe('crane/components/hoist/command');
        client.subscribe('crane/components/boom/command');
        client.subscribe('meta/emergency_button');
        client.subscribe('crane/connectrequest');
    }, []);

    return (
        <div id={"container"}>
            <div id={"visualisation"}>
                <div id={"threeD"}>
                    <CraneVisualisation
                        trolleyCommand={trolleyCommand}
                        gantryCommand={gantryCommand}
                        hoistCommand={hoistCommand}
                        boomCommand={boomCommand}
                        spreaderCommand={spreaderCommand}
                        craneInfo={craneInfo}
                    />
                </div>
                <div id={"input"}>
                    <InputVisualisation
                        trolleyCommand={trolleyCommand}
                        gantryCommand={gantryCommand}
                        hoistCommand={hoistCommand}
                        boomCommand={boomCommand}
                        emergencyCommand={emergencyCommand}
                        spreaderCommand={spreaderCommand}
                    />
                </div>
            </div>
            <div id={"dataTable"}>
                <DataTable craneInfo={craneInfo} />
            </div>
        </div>
    )
}
