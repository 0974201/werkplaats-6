import React, {useEffect, useRef} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'
import {CameraControls} from "@react-three/drei";


export default function CameraRig(props) {
    const cameraControlsRef = useRef()
    useEffect(() => {
        cameraControlsRef.current?.setPosition(100, 0, -100, false)
        cameraControlsRef.current?.setTarget(props.posGantry[0], props.posGantry[1], props.posGantry[2], false)
    }, [])

    useFrame((state, delta) => {

        cameraControlsRef.current?.truck(-parseInt(props.MovementZ)*(3/80), 0, true)
        cameraControlsRef.current?.dolly(-parseInt(props.MovementZ)*(3/95), true)
    })
    return (
        <CameraControls
            ref={cameraControlsRef}
            infinityDolly={true}
        />
    )
}
