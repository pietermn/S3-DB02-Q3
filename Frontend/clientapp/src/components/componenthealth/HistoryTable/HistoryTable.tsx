import { ProductLineHistory } from "../../../globalTypes";
import "./HistoryTable.scss";

interface IHistoryTableProps {
    HistoryMachines: ProductLineHistory[]
}

export default function HistoryTable(props: IHistoryTableProps) {
    return (
        <div className="History-Table">
            <div className="thead">
                <div className="row">
                    <p>Machine</p>
                    <p>Begin</p>
                    <p>End</p>
                </div>
            </div>
            <div className="tbody">
                {
                    props.HistoryMachines.map((historyMachine: ProductLineHistory, index: number) => {
                        return (
                            <div key={index} className="row">
                                <p>{historyMachine.productionLine.name}</p>
                                <p>{historyMachine.startDate.toLocaleString()}</p>
                                <p>{historyMachine.endDate.toLocaleString()}</p>
                            </div>
                        )
                    })
                }
            </div>
        </div>
    )
}
