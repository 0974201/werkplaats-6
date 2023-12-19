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
            "x": "float",
            "y": "float",
            "z": "float"
        },
        "components": [
            {
                "component": "hoist",
                "isActive": "bool",
                "isConnected": "bool",
                "absolutePosition": {
                    "x": "float",
                    "y": "float",
                    "z": "float"
                },
                "speed": {
                    "activeAcceleration": {
                        "y": "bool"
                    },
                    "acceleration": {
                        "y": "float"
                    },
                    "speed": {
                        "y": "float"
                    }
                }
            },
            {
                "component": "trolley",
                "isActive": "bool",
                "absolutePosition": {
                    "x": "float",
                    "y": "float",
                    "z": "float"
                },
                "speed": {
                    "activeAcceleration": {
                        "x": "bool"
                    },
                    "acceleration": {
                        "x": "float"
                    },
                    "speed": {
                        "x": "float"
                    }
                }
            },
            {
                "component": "boom",
                "isActive": "bool",
                "absolutePosition": {
                    "x": "float",
                    "y": "float",
                    "z": "float"
                },
                "speed": {
                    "activeAcceleration": {
                        "x": "bool",
                        "y": "bool"
                    },
                    "acceleration": {
                        "x": "float",
                        "y": "float"
                    },
                    "speed": {
                        "X": "float",
                        "y": "float"
                    }
                }
            },
            {
                "component": "gantry",
                "isActive": "bool",
                "absolutePosition": {
                    "x": "float",
                    "y": "float",
                    "z": "float"
                },
                "speed": {
                    "activeAcceleration": {
                        "z": "bool"
                    },
                    "acceleration": {
                        "z": "float"
                    },
                    "speed": {
                        "z": "float"
                    }
                }
            }
        ],
        "container": {
            "id":"int",
            "isConnected":"bool",
            "absolutePosition":{
                "x":"float",
                "y":"float",
                "z":"float"
            },
            "speed":{
                "speed":{
                    "x":"float",
                    "y":"float",
                    "z":"float"
                }
            }
        },
        "commands": [
            {
                "target": "str",
                "command": "int"
            }
        ]
    })
    const [speed, setSpeed] = useState(0)
    const [pressed, setPressed] = useState(true)


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
            console.log("RECEIVE", message.toString())
            setCraneInfo(message.toString())
        });
        client.subscribe('my/test/topic');
    }, []);

    return (
        <div id={"container"}>
            <button onClick={() => setSpeed(50)}>+</button>
            <button onClick={() => setSpeed(-50)}>-</button>
            <div id={"visualisation"}>
                <div id={"threeD"}>
                    <CraneVisualisation speed={speed} craneInfo={craneInfo} />
                </div>
                <div id={"input"}>
                    <InputVisualisation craneInfo={craneInfo} pressed={pressed} />
                </div>
            </div>
            <div id={"dataTable"}>
                <DataTable craneInfo={craneInfo} />
            </div>
        </div>
    )
}
