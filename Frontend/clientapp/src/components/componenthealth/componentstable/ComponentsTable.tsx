import { useTranslation } from "react-i18next";
import { Component } from "../../../globalTypes";
import "../HistoryTable/HistoryTable.scss";
import "./ComponentsTable.scss";
import { DataGrid, GridColDef, GridSortModel } from "@mui/x-data-grid";
import { useEffect, useState } from "react";

interface ITableProps {
    components: Component[];
    selectedComponentId: number;
    SetComponent: (component: Component) => void;
}

export default function ComponentsTable(props: ITableProps) {
    const [sortModel, setSortModel] = useState<GridSortModel>([
        {
            field: "totalActions",
            sort: "desc",
        },
    ]);
    const { t } = useTranslation();
    const [innerWidth, setInnerWidth] = useState(window.innerWidth);
    let dgWidth = (innerWidth / 3 - 96) * 0.8;

    useEffect(() => {
        window.addEventListener("resize", () => {
            setInnerWidth(window.innerWidth);
        });
    }, []);

    const cols: GridColDef[] = [
        {
            field: "description",
            headerName: t("name.label"),
            width: dgWidth * 0.7,
        },
        {
            field: "totalActions",
            headerName: t("totalactions.label"),
            width: dgWidth * 0.3,
            align: "right",
            headerAlign: "right",
        },
    ];

    return (
        <div className="Components-Table">
            <DataGrid
                disableColumnSelector
                className="ChDataGrid"
                columns={cols}
                rows={props.components}
                rowsPerPageOptions={[]}
                pageSize={100}
                hideFooter
                sortModel={sortModel}
                onSortModelChange={setSortModel}
                onRowClick={(data) => {
                    props.SetComponent(data.row as Component);
                }}
            />
        </div>
    );
}
