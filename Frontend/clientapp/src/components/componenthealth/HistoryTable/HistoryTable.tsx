import { useTranslation } from "react-i18next";
import { ProductLineHistory } from "../../../globalTypes";
import { DataGrid, GridColDef, GridSortModel } from "@mui/x-data-grid";
import "./HistoryTable.scss";
import { useEffect, useState } from "react";

interface IHistoryTableProps {
    HistoryMachines: ProductLineHistory[];
}

export default function HistoryTable(props: IHistoryTableProps) {
    const [sortModel, setSortModel] = useState<GridSortModel>([
        {
            field: "endDate",
            sort: "desc",
        },
    ]);
    const { t } = useTranslation();
    const [innerWidth, setInnerWidth] = useState(window.innerWidth);
    let dgWidth = innerWidth / 3;

    useEffect(() => {
        window.addEventListener("resize", () => {
            setInnerWidth(window.innerWidth);
        });
    }, []);

    const cols: GridColDef[] = [
        {
            field: "productionLine",
            width: dgWidth * 0.3,
            renderHeader: () => {
                return <b>{t("productionline.label")}</b>;
            },
            disableColumnMenu: true,
            sortable: false,
            renderCell: (params) => {
                return params.row.productionLine.name;
            },
        },
        {
            field: "startDate",
            width: dgWidth * 0.35,
            renderHeader: () => {
                return <b>{t("begin.label")}</b>;
            },
            disableColumnMenu: true,
            sortable: false,
            renderCell: (params) => {
                return new Date(params.row.startDate).toLocaleString();
            },
        },
        {
            field: "endDate",
            width: dgWidth * 0.35,
            headerClassName: "History-Table-Last-Header",
            renderHeader: () => {
                return <b>{t("end.label")}</b>;
            },
            disableColumnMenu: true,
            sortable: false,
            renderCell: (params) => {
                return params.row.endDate.toLocaleString() !== "0001-01-01 00:00:00" ? (
                    <p>{params.row.endDate.toLocaleString()}</p>
                ) : (
                    <p>{t("componentstatus.label")}</p>
                );
            },
        },
    ];

    return (
        <DataGrid
            disableColumnSelector
            className="History-Table"
            columns={cols}
            rows={props.HistoryMachines}
            rowsPerPageOptions={[]}
            pageSize={100}
            hideFooter
            sortModel={sortModel}
            onSortModelChange={setSortModel}
        />
        // <div className="History-Table">
        //     <div className="thead">
        //         <div className="row">
        //             <p>{t("productionline.label")}</p>
        //             <p>{t("begin.label")}</p>
        //             <p>{t("end.label")}</p>
        //         </div>
        //     </div>
        //     <main>
        //         {props.HistoryMachines.sort(
        //             (a, b) => new Date(b.startDate).getTime() - new Date(a.startDate).getTime()
        //         ).map((historyMachine: ProductLineHistory, index: number) => {
        //             return (
        //                 <div key={index} className="row">
        //                     <p>{historyMachine.productionLine.name}</p>
        //                     <p>{historyMachine.startDate.toLocaleString()}</p>
        //                     {historyMachine.endDate.toLocaleString() !== "0001-01-01 00:00:00" ? (
        //                         <p>{historyMachine.endDate.toLocaleString()}</p>
        //                     ) : (
        //                         <p>{t("componentstatus.label")}</p>
        //                     )}
        //                 </div>
        //             );
        //         })}
        //     </main>
        // </div>
    );
}
