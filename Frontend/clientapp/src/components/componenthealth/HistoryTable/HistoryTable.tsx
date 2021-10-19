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
                    props.HistoryMachines.sort((a, b) => new Date(b.startDate).getTime() - new Date(a.startDate).getTime()).map((historyMachine: ProductLineHistory, index: number) => {
                        return (
                            <div key={index} className="row">
                                <p>{historyMachine.productionLine.name}</p>
                                <p>{historyMachine.startDate.toLocaleString()}</p>
                                {historyMachine.endDate.toLocaleString() !== "0001-01-01 00:00:00" ? <p>{historyMachine.endDate.toLocaleString()}</p> : <p>Currently attached!</p>}
                            </div>
                        )
                    })
                }
            </div>
        </div>
    )
}
