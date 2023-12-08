import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'
import Boom from "./boom.jsx";

export default function Gantry(props) {
    const meshRef = useRef()
    const boomRef = useRef()

    useFrame((state, delta) => (meshRef.current.position.z += (props.speed/60)))
    useFrame((state, delta) => (boomRef.current.position.z += (props.speed/30)))

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
            <mesh
                {...props}
                ref={boomRef}
                scale={1}
            >
                <boxGeometry args={[1,1,100]} />
                <meshStandardMaterial color={'pink'} />
            </mesh>
        </>

    )
}