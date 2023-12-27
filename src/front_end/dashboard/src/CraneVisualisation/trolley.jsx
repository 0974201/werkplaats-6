import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function Trolley(props) {
    const meshRef = useRef()

    useFrame((state, delta) => {
        meshRef.current.position.z += (parseInt(props.MovementZ)*(3/60))
        meshRef.current.position.x += (parseInt(props.MovementX)*(3/60))
    })

    return (
        <mesh
            {...props}
            ref={meshRef}
            scale={1}
        >
            <boxGeometry args={props.dimensions} />
            <meshStandardMaterial color={'orange'} />
        </mesh>
    )
}
