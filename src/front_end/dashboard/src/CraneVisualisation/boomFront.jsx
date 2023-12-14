import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function BoomFront(props) {
    const meshRef = useRef()
    const groupRef = useRef()

    useFrame((state, delta) => (meshRef.current.position.z += (props.MovementZ/60)))
    // useFrame((state, delta) => (groupRef.current.rotation.z += (1/60)))

    return (
        <group ref={groupRef}>
            <mesh
                {...props}
                ref={meshRef}
                scale={1}
            >
                <boxGeometry args={props.dimensions} />
                <meshStandardMaterial color={'purple'} />
            </mesh>
        </group>
    )
}