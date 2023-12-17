import React, {useEffect, useRef, useState} from 'react';
import './CraneVisualisation.css'
import { useFrame } from '@react-three/fiber'

export default function Wire(props) {
    const meshRef = useRef()

    const [length, setLength] = useState(props.wireLength)

    useFrame((state, delta) => {
        meshRef.current.position.z += (props.MovementZ/60)
        meshRef.current.position.x += (props.MovementX/60)
        meshRef.current.position.y -= (props.MovementY/120)
        meshRef.current.scale.y += (props.MovementY/480)})

    useEffect(() => {
        setLength(props.wireLength)
    },[props.wireLength])
    return (
        <mesh
            {...props}
            ref={meshRef}
        >
            <cylinderGeometry args={[0.1, 0.1, length, 8]} />
            <meshStandardMaterial color={'gray'} />
        </mesh>
    )
}