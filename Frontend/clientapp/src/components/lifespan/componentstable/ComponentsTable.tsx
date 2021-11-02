import { useEffect, useState } from "react";
import { GrStatusGoodSmall as StatusDot } from 'react-icons/gr';
import { GetComponents } from '../../../api/requests/components';
import { Component } from "../../../globalTypes";
import "./ComponentsTableStyle.scss";

interface IComponentsTable {
    setSelectedComponet: (component: Component) => void
}

export default function ComponentsTable(props: IComponentsTable) {
    const [components, setComponents] = useState<Component[]>([])
    useEffect(() => {
        async function GetAsyncComponents() {
            setComponents(await GetComponents());
        }

        GetAsyncComponents();
    }, [])

    function GetStatusColor(percentage: number): string {
        if (percentage >= 95 && percentage < 100) {
            return "orange"
        }

        if (percentage >= 100) {
            return "red"
        } else {
            return "green";
        }
    }

    return (
        <div className="lifespan-table">
            <div className="row">
                <p>Status</p>
                <p>Name</p>
                <p>Current actions</p>
                <p>Max %</p>
            </div>
            {
                components && components.sort((a, b) => b.percentageMaintenance - a.percentageMaintenance).map((component, index) => {
                    return (
                        <div onClick={() => props.setSelectedComponet(component)} className="row">
                            <p><StatusDot className={GetStatusColor(component.percentageMaintenance)} /></p>
                            <p>{component.description}</p>
                            <p>{component.currentActions}</p>
                            <p>{component.percentageMaintenance}%</p>
                        </div>
                    )
                })
            }
        </div>
    )
}
