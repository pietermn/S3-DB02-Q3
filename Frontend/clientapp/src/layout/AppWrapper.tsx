import React, { ReactChild, ReactChildren, FC } from 'react'

interface AppWrapper_Children {
    children: React.ReactNode
}

export default function AppWrapper(props: AppWrapper_Children) {
    return (
        <React.Fragment>
            {props.children}
        </React.Fragment>
    )
}
