import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'
import Boom from "./boom.jsx";

export default function Gantry(props) {
    const meshRef = useRef()

    useFrame((state, delta) => (meshRef.current.position.z += (props.MovementZ/60)))

    return (
        <>
            <mesh
                {...props}
                ref={meshRef}
                scale={1}
            >
                <boxGeometry args={props.dimensions} />
                <meshStandardMaterial color={'green'} />
            </mesh>
        </>

    )
}