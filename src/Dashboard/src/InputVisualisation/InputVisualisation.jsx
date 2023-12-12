import React from 'react';
import './InputVisualisation.css'
import EmergencyButton from "../EmergencyButton/EmergencyButton.jsx";

export default function InputVisualisation() {

    function Button(props) {

        return (
            <div id={"inputButton"} style={{backgroundColor: "red"}}>
                <span>{props.letter}</span>
            </div>
        )
    }

    return (
        <>
            <EmergencyButton />
            <Button letter={"W"} active={true} />
        </>
    )
}