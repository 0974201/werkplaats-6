import React, {useEffect, useRef} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'
import {CameraControls} from "@react-three/drei";


export default function CameraRig(props) {
    const cameraControlsRef = useRef()
    // cameraControlsRef.current?.truck(0, 10, true)
    useEffect(() => {
        // cameraControlsRef.current?.truck(0, 0, false)
        // cameraControlsRef.current?.dolly(600, true)
        cameraControlsRef.current?.setPosition(80, 0, -400, false)
        cameraControlsRef.current?.setTarget(props.posGantry[0], props.posGantry[1], props.posGantry[2], false)
    }, [])

    useFrame((state, delta) => {

        cameraControlsRef.current?.truck(-props.MovementZ/85, 0, true)
        cameraControlsRef.current?.dolly(-props.MovementZ/90, true)
        // cameraControlsRef.current?.setPosition(80, 30, -400, true)
        // cameraControlsRef.current.position.z += props.MovementZ
    })
    return (
        <CameraControls
            ref={cameraControlsRef}
            infinityDolly={true}
        />
    )
}
