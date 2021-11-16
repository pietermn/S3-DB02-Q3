import { useTranslation } from "react-i18next";
import { ProductLineHistory } from "../../../globalTypes";
import "./HistoryTable.scss";

interface IHistoryTableProps {
    HistoryMachines: ProductLineHistory[];
}

export default function HistoryTable(props: IHistoryTableProps) {
    const { t } = useTranslation();

    return (
        <div className="History-Table">
            <div className="thead">
                <div className="row">
                    <p>{t("machine.label")}</p>
                    <p>{t("begin.label")}</p>
                    <p>{t("end.label")}</p>
                </div>
            </div>
            <main>
                {props.HistoryMachines.sort(
                    (a, b) => new Date(b.startDate).getTime() - new Date(a.startDate).getTime()
                ).map((historyMachine: ProductLineHistory, index: number) => {
                    return (
                        <div key={index} className="row">
                            <p>{historyMachine.productionLine.name}</p>
                            <p>{historyMachine.startDate.toLocaleString()}</p>
                            {historyMachine.endDate.toLocaleString() !== "0001-01-01 00:00:00" ? (
                                <p>{historyMachine.endDate.toLocaleString()}</p>
                            ) : (
                                <p>{t("componentstatus.label")}</p>
                            )}
                        </div>
                    );
                })}
            </main>
        </div>
    );
}
