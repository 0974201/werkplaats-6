import React, {useEffect, useState} from 'react';
import './InputVisualisation.css'

export default function InputVisualisation(props) {
    const [controllerConnected, setControllerConnected] = useState(false)
    const [commands, setCommands] = useState(props)

    useEffect(() => {
        setCommands(props)
    }, [props])

    console.log(commands)

    function Button(props) {
        if (props.active) {
            console.log(props.active)
        }

        return (
            <div id={"inputButton"} style={{backgroundColor: props.active ? "green" : "red"}}>
                <span className={"inputLetter"}>{props.letter}</span>
            </div>
        )
    }

    function Joystick(props) {
        const posY = () => {
            switch (props.Y) {
                case 1:
                    return {
                        alignItems: 'flex-start'
                    }
                case 0:
                    return {
                        alignItems: 'center'
                    }
                case -1:
                    return {
                        alignItems: 'flex-end'
                    }
                default:
                    return {
                        alignItems: 'center'
                    }
            }
        }
        const posX = () => {
            switch (props.X) {
                case 1:
                    return {
                        justifyContent: 'flex-start'
                    }
                case 0:
                    return {
                        justifyContent: 'center'
                    }
                case -1:
                    return {
                        justifyContent: 'flex-end'
                    }
                default:
                    return {
                        justifyContent: 'center'
                    }
            }
        }
        return (
            <div id={"joystick"} style={{...posY(), ...posX()} }>
                <div id={'shadow'}></div>
                <div id={'stick'}>
                    <span className={"inputLetter"}>{props.letter}</span>
                </div>
            </div>
        )
    }

    const EmergencyButton = (props) => (
        <div id={"emergencyButton"}>
            {/*Emergency Stop Button icon by Icons8*/}
            {props.pressed ?
                <img src={"/icons8-emergency-stop-button-96_START.png"} alt={"START"}/> :
                <img src={"/icons8-emergency-stop-button-96_STOP.png"} alt={"STOP"}/>
            }
        </div>
    )

    const SpreaderButton = (props) => (
        <div id={"spreaderButton"} style={{backgroundColor: props.active ? "green" : "red"}}>
            <span>Connected</span>
        </div>
    )

    const Controller = () => (
        <div id={"controllerInput"}>
            <Joystick letter={"L"} Y={0} X={0} />
            <Joystick letter={"R"} Y={0} X={0} />
            <ABXYButtons />
            <Button letter={"RB"} active={false} />
        </div>
    )



    const Keyboard = () => (
        <div id={"keyboardInput"}>
            <div id={"letterCluster"}>
                <div>
                    <Button letter={"Q"} active={commands.boomCommand === "1"} />
                    <Button letter={"W"} active={commands.trolleyCommand === "1"} />
                    <Button letter={"E"} active={commands.boomCommand === "-1"} />
                    <Button letter={String.fromCharCode(8593)} active={commands.hoistCommand === "1"} />
                </div>
                <div>
                    <Button letter={"A"} active={commands.gantryCommand === "-1"} />
                    <Button letter={"S"} active={commands.trolleyCommand === "-1"} />
                    <Button letter={"D"} active={commands.gantryCommand === "1"} />
                    <Button letter={String.fromCharCode(8595)} active={commands.hoistCommand === "-1"} />
                </div>
            </div>
        </div>
    )

    const ABXYButtons = () => (
        <div id={"ABXYContainer"}>
            <div id={"ABXYRowSide"}>
                <Button letter={"X"} active={false} />
            </div>
            <div id={"ABXYRowMiddle"}>
                <Button letter={"Y"} active={false} />
                <Button letter={"A"} active={false} />
            </div>
            <div id={"ABXYRowSide"}>
                <Button letter={"B"} active={false} />
            </div>
        </div>
    )


    return (
        <div id={"inputContainer"}>
            {controllerConnected ?
                <Controller />
            :
                <Keyboard />
            }
            <div id={"onOffButton"}>
                <EmergencyButton pressed={commands.emergencyCommand === "true"} />
                <SpreaderButton active={commands.spreaderCommand === "true"} />
            </div>

        </div>
    )
}
