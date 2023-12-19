import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function BoomFront(props) {
    const meshRef = useRef()
    const groupRef = useRef()

    useFrame((state, delta) => {
        groupRef.current.position.z += (props.MovementZ / 60)
        groupRef.current.rotation.z += (props.boomRotation/60)
    })
    const armLength = 2
    const endLength = 10.8
    return (
        <group ref={groupRef}>
            <mesh
                position={[props.position[0], props.position[1], props.position[2]-armLength/2+props.dimensions[2]/2]}
                ref={meshRef}
            >
                <boxGeometry args={[props.dimensions[0], props.dimensions[1], armLength]} />
                <meshStandardMaterial color={'purple'} />
            </mesh>
            <mesh
                position={[props.position[0], props.position[1], props.position[2]+armLength/2-props.dimensions[2]/2]}
                ref={meshRef}
            >
                <boxGeometry args={[props.dimensions[0], props.dimensions[1], armLength]} />
                <meshStandardMaterial color={'purple'} />
            </mesh>
            <mesh
                position={[props.position[0]+props.dimensions[0]/2-endLength/2, props.position[1], props.position[2]]}
                ref={meshRef}
            >
                <boxGeometry args={[endLength, props.dimensions[1], props.dimensions[2]]} />
                <meshStandardMaterial color={'purple'} />
            </mesh>
        </group>
    )
}