import React from 'react'
import Navbar from './Navbar/Navbar'

interface AppWrapper_Children {
    children: React.ReactNode
}

export default function AppWrapper(props: AppWrapper_Children) {
    return (
        <React.Fragment>
            <Navbar />
            {props.children}
        </React.Fragment>
    )
}
