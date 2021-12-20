import { useTranslation } from "react-i18next";
import { ProductLineHistory } from "../../../globalTypes";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import "./HistoryTable.scss";
import { useEffect, useState } from "react";
import i18n from "i18next";

interface IHistoryTableProps {
    HistoryMachines: ProductLineHistory[];
}

export default function HistoryTable(props: IHistoryTableProps) {
    const [innerWidth, setInnerWidth] = useState(window.innerWidth);
    let dgWidth = innerWidth / 3;
    const { t } = useTranslation();

    const cols: GridColDef[] = [
        {
            field: "productionLine",
            width: dgWidth * 0.3,
            renderHeader: () => {
                return <b key={i18n.language}>{t("productionline.label")}</b>;
            },
            disableColumnMenu: true,
            sortable: false,
            renderCell: (params) => {
                return params.row.productionLine.name;
            },
        },
        {
            field: "startDate",
            width: dgWidth * 0.3,
            renderHeader: () => {
                return <b key={i18n.language}>{t("begin.label")}</b>;
            },
            disableColumnMenu: true,
            sortable: false,
            renderCell: (params) => {
                return new Date(params.row.startDate).toLocaleString();
            },
        },
        {
            field: "endDate",
            width: dgWidth * 0.3,
            headerClassName: "History-Table-Last-Header",
            renderHeader: () => {
                return <b key={i18n.language}>{t("end.label")}</b>;
            },
            disableColumnMenu: true,
            sortable: false,
            renderCell: (params) => {
                return new Date(params.row.endDate).getFullYear() !== 1 ? (
                    <p>{new Date(params.row.endDate).toLocaleString()}</p>
                ) : (
                    <p>{t("componentstatus.label")}</p>
                );
            },
        },
    ];

    return (
        <DataGrid
            disableColumnSelector
            disableSelectionOnClick
            className="History-Table"
            columns={cols}
            rows={[...props.HistoryMachines].sort(
                (a, b) => new Date(b.startDate).getTime() - new Date(a.startDate).getTime()
            )}
            rowsPerPageOptions={[]}
            pageSize={100}
            hideFooter
            onResize={() => {
                setInnerWidth(window.innerWidth);
            }}
        />
    );
}
