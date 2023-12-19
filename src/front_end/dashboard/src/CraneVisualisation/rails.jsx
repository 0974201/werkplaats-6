import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function Rails(props) {
    const meshRef = useRef()
    const color = 'darkgray'

    return (
        <>
            <mesh
                position={[(props.position[0])-(props.dimensions[0]/2)+1.25, props.position[1], props.position[2]]}
                ref={meshRef}
                scale={1}
            >
                <boxGeometry args={[3, props.dimensions[1], props.dimensions[2]]} />
                <meshStandardMaterial color={color} />
            </mesh>
            <mesh
                position={[(props.position[0])+(props.dimensions[0]/2)-1.25, props.position[1], props.position[2]]}
                ref={meshRef}
                scale={1}
            >
                <boxGeometry args={[3, props.dimensions[1], props.dimensions[2]]} />
                <meshStandardMaterial color={color} />
            </mesh>
        </>
    )
}