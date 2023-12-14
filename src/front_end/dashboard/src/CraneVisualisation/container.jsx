import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function ShipContainer(props) {
    const meshRef = useRef()

    if (props.isConnected === true) {
        useFrame((state, delta) => (meshRef.current.position.z += (props.MovementZ/60)))
        useFrame((state, delta) => (meshRef.current.position.x += (props.MovementX/60)))
        useFrame((state, delta) => (meshRef.current.position.y -= (props.MovementY/60)))
    }


    return (
        <mesh
            {...props}
            ref={meshRef}
            scale={1}
        >
            <boxGeometry args={props.dimensions} />
            <meshStandardMaterial color={'red'} />
        </mesh>
    )
}