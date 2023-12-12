import React from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'


export default function CameraRig({ position: [x, y, z], lookAt}) {
    useFrame((state) => {
        state.camera.position.lerp({x, y, z}, 0.1)
        state.camera.lookAt(lookAt[0], lookAt[1], lookAt[2])
    })
}