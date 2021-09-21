import React from 'react'
import Navbar from './Navbar/Navbar'
import "../_variables.scss";

interface AppWrapper_Children {
    children: React.ReactNode
}

export default function AppWrapper(props: AppWrapper_Children) {
    return (
        <React.Fragment>
            <div id="page-container">
                <Navbar />
                <div id="content-wrapper">
                    {props.children}
                </div>
            </div>
        </React.Fragment>
    )
}
