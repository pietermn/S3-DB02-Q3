import { createContext, useState } from "react";
import { Component } from "../globalTypes";

interface IComponentContext {
    component: Component;
    setComponent: (component: Component) => void;
}

const defaultState = {
    component: undefined,
    setComponent: (component: undefined) => {},
};

export const ComponentContext = createContext(defaultState);

interface IComponentProvider {
    children?: React.ReactNode;
}

export function ComponentProvider(props: IComponentProvider) {
    const [component, setComponent] = useState(defaultState.component);

    return <ComponentContext.Provider value={{ component, setComponent }}>{props.children}</ComponentContext.Provider>;
}
