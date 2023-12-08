import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function Wire(props) {
    const meshRef = useRef()

    return (
        <mesh
            {...props}
            ref={meshRef}
            scale={1}
        >
            <cylinderGeometry args={[0.1, 0.1, props.wireLength, 8]} />
            <meshStandardMaterial color={'gray'} />
        </mesh>
    )
}