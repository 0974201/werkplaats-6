import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function Rails(props) {
    const meshRef = useRef()

    return (
            <mesh
                {...props}
                ref={meshRef}
                scale={1}
            >
                <boxGeometry args={props.dimensions} />
                <meshStandardMaterial color={'yellow'} />
            </mesh>
    )
}