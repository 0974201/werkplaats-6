import React, {useEffect, useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function Wire(props) {
    const meshRef = useRef()

    const [length, setLength] = useState(props.wireLength)

    useFrame((state, delta) => (meshRef.current.position.z += (props.MovementZ/60)))
    useFrame((state, delta) => (meshRef.current.position.x += (props.MovementX/60)))
    // useFrame((state, delta) => (meshRef.current.wireLength += 200))

    useEffect(() => {
        setLength(props.wireLength)
    },[props.wireLength])

    console.log(meshRef.current)
    return (
        <mesh
            {...props}
            ref={meshRef}
            scale={1}
        >
            <cylinderGeometry args={[0.1, 0.1, length, 8]} />
            <meshStandardMaterial color={'gray'} />
        </mesh>
    )
}