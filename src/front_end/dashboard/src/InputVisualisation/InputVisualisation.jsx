import React from 'react';
import './InputVisualisation.css'

export default function InputVisualisation(props) {

    function Button(props) {

        return (
            <div id={"inputButton"} style={{backgroundColor: props.active ? "green" : "red"}}>
                <span>{props.letter}</span>
            </div>
        )
    }

    return (
        <div id={"inputContainer"}>
            <div id={"letterCluster"}>
                <div>
                    <Button letter={"Q"} active={props.pressed} />
                    <Button letter={"W"} active={false} />
                    <Button letter={"E"} active={false} />
                </div>
                <div>
                    <Button letter={"A"} active={false} />
                    <Button letter={"S"} active={false} />
                    <Button letter={"D"} active={false} />
                </div>
            </div>
            <div id={"opCluster"}>
                <Button letter={"O"} active={false} />
                <Button letter={"P"} active={false} />
            </div>
            <div id={"arrowCluster"}>
                <Button letter={String.fromCharCode(8593)} active={false} />
                <Button letter={String.fromCharCode(8595)} active={false} />
            </div>
            <div id={"emergencyButton"}>
                {/*Emergency Stop Button icon by Icons8*/}
                {props.pressed ?
                    <img src={"/public/icons8-emergency-stop-button-96_STOP.png"} alt={"STOP"}/> :
                    <img src={"/public/icons8-emergency-stop-button-96_START.png"} alt={"START"}/>}

            </div>
        </div>
    )
}