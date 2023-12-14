// eslint-disable-next-line no-unused-vars
import React from 'react';
import mqtt from 'mqtt';
import './Dashboard.css'
import CraneVisualisation from "./CraneVisualisation/CraneVisualisation.jsx";
import DataTable from "./DataTable/DataTable.jsx";
import AnimatedGraphs from "./AnimatedGraphs/AnimatedGraphs.jsx";
import InputVisualisation from "./InputVisualisation/InputVisualisation.jsx";
import {useEffect, useState} from "react";

export default function Dashboard() {
    const [craneInfo, setCraneInfo] = useState(null)
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
            <div>
                <div id={"threeD"}>
                    <CraneVisualisation speed={speed} craneInfo={craneInfo} />
                </div>
                <div id={"dataTable"}>
                    <DataTable craneInfo={craneInfo} />
                </div>
            </div>
            <div>
                <div id={"graph"}>
                    <AnimatedGraphs />
                </div>
                <div id={"input"}>
                    <InputVisualisation craneInfo={craneInfo} pressed={pressed} />
                </div>
            </div>

        </div>
    )
}
