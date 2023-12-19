import React, {useState} from 'react';
import './InputVisualisation.css'

export default function InputVisualisation(props) {
    const [controllerConnected, setControllerConnected] = useState(false)


    function Button(props) {
        return (
            <div id={"inputButton"} style={{backgroundColor: props.active ? "green" : "red"}}>
                <span className={"inputLetter"}>{props.letter}</span>
            </div>
        )
    }

    function Joystick(props) {
        const posY = () => {
            switch (props.Y) {
                case 'top':
                    return {
                        alignItems: 'flex-start'
                    }
                case 'center':
                    return {
                        alignItems: 'center'
                    }
                case 'bottom':
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
                case 'left':
                    return {
                        justifyContent: 'flex-start'
                    }
                case 'center':
                    return {
                        justifyContent: 'center'
                    }
                case 'right':
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

    const Controller = () => (
        <div id={"controllerInput"}>
            <Joystick letter={"L"} Y={'top'} X={'left'} />
            <Joystick letter={"R"} Y={'center'} X={'right'} />
        </div>
    )

    const Keyboard = () => (
        <div id={"keyboardInput"}>
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
        </div>
    )

    const EmergencyButton = () => (
        <div id={"emergencyButton"}>
            {/*Emergency Stop Button icon by Icons8*/}
            {props.pressed ?
                <img src={"/public/icons8-emergency-stop-button-96_STOP.png"} alt={"STOP"}/> :
                <img src={"/public/icons8-emergency-stop-button-96_START.png"} alt={"START"}/>}
        </div>
    )


    return (
        <div id={"inputContainer"}>
            {controllerConnected ?
                <Controller />
            :
                <Keyboard />
            }
            <EmergencyButton />
        </div>
    )
}
