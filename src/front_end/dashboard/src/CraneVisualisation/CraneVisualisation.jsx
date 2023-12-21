import React, {useEffect, useRef, useState} from 'react';
import './CraneVisualisation.css'
import * as THREE from "three";
import {Canvas, useFrame, useThree} from '@react-three/fiber'
import {Box, CameraControls, PerspectiveCamera} from '@react-three/drei'
import ShipContainer from "./container.jsx";
import Gantry from "./gantry.jsx";
import Hoist from "./hoist.jsx";
import Trolley from "./trolley.jsx";
import Wire from "./wire.jsx";
import Rails from "./rails.jsx";
import BoomFront from "./boomFront.jsx";
import BoomBack from "./boomBack.jsx";
import CameraRig from "./camera.jsx";

export default function CraneVisualisation(props) {

    const [railsSize, setRailsSize] = useState([31, 1, 400])
    const [gantrySize, setGantrySize] = useState([30.5, 76.1, 20.2])
    const [shipContainerSize, setShipContainerSize] = useState([2.44, 2.59, 13.71])
    const [boomSizeFront, setBoomSizeFront] = useState([76.8, 2, 8])
    const [boomSizeBack, setBoomSizeBack] = useState([62.6, 2, 8])
    const [trolleySize, setTrolleySize] = useState([4, 2, 5.6])
    const [hoistSize, setHoistSize] = useState([2.4, 1, 14])

    const [posRails, setPosRails] = useState([-(railsSize[0]/2)+0.25, -(gantrySize[1]/2)+railsSize[1]/2, 0])
    const [posGantry, setPosGantry] = useState([-(gantrySize[0]/2), 0, -(railsSize[2]/2)+gantrySize[2]/2])
    const [posBoomFront, setPosBoomFront] = useState([(boomSizeFront[0]/2), 0, posGantry[2]])
    const [posBoomBack, setPosBoomBack] = useState([-boomSizeBack[0]/2, 0, posGantry[2]])
    const [posTrolley, setPosTrolley] = useState([-47.5, posBoomFront[1]-(boomSizeFront[1]/2)-(trolleySize[1]/2), posGantry[2]])
    const [posHoist, setPosHoist] = useState([posTrolley[0], -10, posGantry[2]])
    const [posShipContainer, setPosShipContainer] = useState([63, -((gantrySize[1]/2)-(shipContainerSize[1])/2), posGantry[2]])

    const [wireLength, setWireLength] = useState(posTrolley[1]-posHoist[1])

    const [trolleyCommand, setTrolleyCommand] = useState(props.trolleyCommand)
    const [gantryCommand, setGantryCommand] = useState(props.gantryCommand)
    const [hoistCommand, setHoistCommand] = useState(props.hoistCommand)
    const [boomCommand, setBoomCommand] = useState(props.boomCommand)
    const [spreaderCommand, setSpreaderCommand] = useState(props.spreaderCommand)

    console.log(props.spreaderCommand)

    useEffect(() => {
        setTrolleyCommand(props.trolleyCommand)
        setGantryCommand(props.gantryCommand)
        setHoistCommand(props.hoistCommand)
        setBoomCommand(props.boomCommand)
        setSpreaderCommand(props.spreaderCommand)
    }, [
        props.trolleyCommand,
        props.gantryCommand,
        props.hoistCommand,
        props.boomCommand,
        props.spreaderCommand
    ])

    return (
        <div id={"container3d"}>
            <Canvas>
                <ambientLight />
                <pointLight position={[100, 100, 100]} intensity={100000} />
                <pointLight position={[-100, 100, -100]} intensity={50000} />
                <CameraRig MovementZ={gantryCommand} posGantry={posGantry} />
                <group>
                    <Rails
                        position={posRails}
                        dimensions={railsSize}
                    />
                    <Gantry
                        position={posGantry}
                        dimensions={gantrySize}
                        MovementZ={gantryCommand}
                    />
                    <BoomFront
                        position={posBoomFront}
                        dimensions={boomSizeFront}
                        boomRotation={boomCommand}
                        MovementZ={gantryCommand}
                    />
                    <BoomBack
                        position={posBoomBack}
                        dimensions={boomSizeBack}
                        MovementZ={gantryCommand}
                    />
                    <Trolley
                        position={posTrolley}
                        dimensions={trolleySize}
                        MovementZ={gantryCommand}
                        MovementX={trolleyCommand}
                    />
                    <Wire
                        position={[posTrolley[0],posTrolley[1]-(wireLength/2), posTrolley[2]]}
                        wireLength={wireLength}
                        MovementZ={gantryCommand}
                        MovementX={trolleyCommand}
                        MovementY={hoistCommand}
                    />
                    <Hoist
                        position={posHoist}
                        dimension={hoistSize}
                        MovementZ={gantryCommand}
                        MovementX={trolleyCommand}
                        MovementY={hoistCommand}
                    />
                    <ShipContainer
                        position={posShipContainer}
                        dimensions={shipContainerSize}
                        MovementZ={gantryCommand}
                        MovementX={trolleyCommand}
                        MovementY={hoistCommand}
                        isConnected={spreaderCommand}
                    />
                </group>
                <Box args={[200, 1, 400]} position={[0, -(76.1/2), 0]} material-color={"gray"} />
            </Canvas>
        </div>
    )
}
