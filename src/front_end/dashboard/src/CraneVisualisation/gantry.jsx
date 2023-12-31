import React, {useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'
import Boom from "./boomFront.jsx";

export default function Gantry(props) {
    const meshRef = useRef()
    const gantryRef = useRef()
    useFrame((state, delta) => (gantryRef.current.position.z += parseInt(props.MovementZ)*(3/60)))

    const legWidth = 2

    return (
        <>
            <group ref={gantryRef}>
                <mesh
                    position={[props.position[0]+legWidth/2-props.dimensions[0]/2, props.position[1], props.position[2]+legWidth/2-props.dimensions[2]/2]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[legWidth, props.dimensions[1], legWidth]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>

                <mesh
                    position={[props.position[0]+legWidth/2-props.dimensions[0]/2, props.position[1], props.position[2]-legWidth/2+props.dimensions[2]/2]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[legWidth, props.dimensions[1], legWidth]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>

                <mesh
                    position={[props.position[0]-legWidth/2+props.dimensions[0]/2, props.position[1], props.position[2]+legWidth/2-props.dimensions[2]/2]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[legWidth, props.dimensions[1], legWidth]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>

                <mesh
                    position={[props.position[0]-legWidth/2+props.dimensions[0]/2, props.position[1], props.position[2]-legWidth/2+props.dimensions[2]/2]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[legWidth, props.dimensions[1], legWidth]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>

                <mesh
                    position={[props.position[0], props.position[1]+legWidth, props.position[2]-legWidth/2+props.dimensions[2]/2]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[props.dimensions[0], legWidth, legWidth]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>
                <mesh
                    position={[props.position[0], props.position[1]+legWidth, props.position[2]+legWidth/2-props.dimensions[2]/2]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[props.dimensions[0], legWidth, legWidth]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>
                <mesh
                    position={[props.position[0], props.position[1]-(48.7-17.5), props.position[2]-legWidth/2+props.dimensions[2]/2]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[props.dimensions[0], legWidth, legWidth]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>
                <mesh
                    position={[props.position[0], props.position[1]-(48.7-17.5), props.position[2]+legWidth/2-props.dimensions[2]/2]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[props.dimensions[0], legWidth, legWidth]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>
                <mesh
                    position={[-legWidth/2, props.position[1]+legWidth, props.position[2]]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[legWidth, legWidth, props.dimensions[2]]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>
                <mesh
                    position={[-(props.dimensions[0]-legWidth/2), props.position[1]+legWidth, props.position[2]]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[legWidth, legWidth, props.dimensions[2]]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>
                <mesh
                    position={[-props.dimensions[0]/2, props.position[1]+props.dimensions[1]/2, props.position[2]]}
                    ref={meshRef}
                    scale={1}
                >
                    <boxGeometry args={[props.dimensions[0], legWidth, props.dimensions[2]]} />
                    <meshStandardMaterial color={'purple'} />
                </mesh>
            </group>



        </>

    )
}
