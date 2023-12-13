import React, {useEffect, useRef, useState} from 'react';
import './CraneVisualisation.css'
import { Canvas, useFrame } from '@react-three/fiber'
import {Box, CameraControls, Line, Plane} from '@react-three/drei'
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
    const [railsSize, setRailsSize] = useState([31, 1, 100])
    const [gantrySize, setGantrySize] = useState([30.5, 76.1, 20.2])
    const [shipContainerSize, setShipContainerSize] = useState([2.44, 2.59, 13.71])
    const [boomSizeFront, setBoomSizeFront] = useState([76.8, 2, 8])
    const [boomSizeBack, setBoomSizeBack] = useState([62.6, 2, 8])
    const [trolleySize, setTrolleySize] = useState([4, 2, 5.6])
    const [hoistSize, setHoistSize] = useState([2.4, 1, 14])

    const [posRails, setPosRails] = useState([-(railsSize[0]/2)+0.25, -(gantrySize[1]/2)+railsSize[1]/2, 0])
    const [posGantry, setPosGantry] = useState([-(gantrySize[0]/2), 0, 0])
    const [posBoomFront, setPosBoomFront] = useState([(boomSizeFront[0]/2), 0, posGantry[2]])
    const [posBoomBack, setPosBoomBack] = useState([-boomSizeBack[0]/2, 0, posGantry[2]])
    const [posTrolley, setPosTrolley] = useState([-47.5, posBoomFront[1]-(boomSizeFront[1]/2)-(trolleySize[1]/2), 0])
    const [posHoist, setPosHoist] = useState([posTrolley[0], -10, 0])
    const [posShipContainer, setPosShipContainer] = useState([63, -((gantrySize[1]/2)-(shipContainerSize[1])/2), 0])

    const [wireLength, setWireLength] = useState(posTrolley[1]-posHoist[1])

    const [movementX, setMovementX] = useState(props.speed)
    const [movementY, setMovementY] = useState(props.speed)
    const [movementZ, setMovementZ] = useState(props.speed)
    const [rotationBoom, setRotationBoom] = useState()

    const [freeCamera, setFreeCamera] = useState(false)
    const [cameraPosition, setCameraPosition] = useState([80, 30, 80])
    const [cameraLookAt, setCameraLookAt] = useState([20, 0, 0])

    useEffect(() => {
        setMovementX(props.speed)
        setMovementY(props.speed)
        setMovementZ(props.speed)
    }, [props.speed, props.craneInfo])

    useEffect(() => {
        setWireLength(posTrolley[1]-posHoist[1])
    }, [posHoist])

    useEffect(() => {
        setCameraPosition(cameraPosition)
    }, [cameraPosition])

    return (
        <div id={"container3d"}>

            <div id={"cameraButtons"}>
                <button onClick={() => {
                    setCameraPosition([70, 30, 80])
                    setCameraLookAt([20, 0, 0])
                    setFreeCamera(false)
                }}>Standard</button>
                <button onClick={() => {
                    setCameraPosition([20, 100, 0])
                    setCameraLookAt([20, 0, 0])
                    setFreeCamera(false)
                }}>Top</button>
                <button onClick={() => {
                    setCameraPosition([0, 0, 100])
                    setCameraLookAt([0, 0, 0])
                    setFreeCamera(false)
                }}>Side</button>
                <button onClick={() => {
                    setFreeCamera(true)
                }}>Free</button>
            </div>

            <Canvas>
                {freeCamera ?
                    <CameraControls /> :
                    <CameraRig lookAt={cameraLookAt} position={cameraPosition} />
                }
                <ambientLight />
                <pointLight position={[100, 100, 100]} intensity={100000} />
                <pointLight position={[-100, 100, -100]} intensity={50000} />

                <group>
                    <Rails
                        position={posRails}
                        dimensions={railsSize}
                    />
                    <Gantry
                        position={posGantry}
                        dimensions={gantrySize}
                        MovementZ={movementZ}
                    />
                    <BoomFront
                        position={posBoomFront}
                        dimensions={boomSizeFront}
                        boomRotarion={0}
                        MovementZ={movementZ}
                    />
                    <BoomBack
                        position={posBoomBack}
                        dimensions={boomSizeBack}
                        MovementZ={movementZ}
                    />
                    <Trolley
                        position={posTrolley}
                        dimensions={trolleySize}
                        MovementZ={movementZ}
                        MovementX={movementX}
                    />
                    <Wire
                        position={[posTrolley[0],posTrolley[1]-(wireLength/2), posTrolley[2]]}
                        wireLength={wireLength}
                        MovementZ={movementZ}
                        MovementX={movementX}
                        MovementY={movementY}
                    />
                    <Hoist
                        position={posHoist}
                        dimension={hoistSize}
                        MovementZ={movementZ}
                        MovementX={movementX}
                        MovementY={movementY}
                    />
                    <ShipContainer
                        position={posShipContainer}
                        dimensions={shipContainerSize}
                        MovementZ={movementZ}
                        MovementX={movementX}
                        MovementY={movementY}
                        isConnected={props.isConnected}
                    />
                </group>
                <Box args={[200, 1, 100]} position={[0, -(76.1/2), 0]} material-color={"gray"} />
            </Canvas>
        </div>
    )
}