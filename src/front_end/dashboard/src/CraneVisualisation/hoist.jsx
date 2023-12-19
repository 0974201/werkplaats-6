import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function Hoist(props) {
    const meshRef = useRef()

    useFrame((state, delta) => {
        meshRef.current.position.z += (props.MovementZ / 60)
        meshRef.current.position.x += (props.MovementX/60)
        meshRef.current.position.y -= (props.MovementY/60)
    })

    return (
        <mesh
            {...props}
            ref={meshRef}
            scale={1}
        >
            <boxGeometry args={props.dimension} />
            <meshStandardMaterial color={'blue'} />
        </mesh>
    )
}