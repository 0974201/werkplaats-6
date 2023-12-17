import React, {useEffect, useRef} from 'react';
import './CraneVisualisation.css'
import {useFrame, useThree} from '@react-three/fiber'


export default function CameraRig2(props) {

    console.log(props.camera)
    // useEffect(() => {
    //     props.camera.position.z =
    // })
    // useEffect(() => void set({ camera: ref.current }), []);
    useFrame((state) => {
        state.camera.position.z += props.MovementZ/60
        // state.camera.lookAt(lookAt[0], lookAt[1], lookAt[2])
    })
}