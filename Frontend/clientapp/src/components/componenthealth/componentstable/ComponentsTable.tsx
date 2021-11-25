import { useTranslation } from "react-i18next";
import { Component } from "../../../globalTypes";
import "../HistoryTable/HistoryTable.scss";
import "./ComponentsTable.scss";
import { DataGrid, GridColDef } from "@mui/x-data-grid";

interface ITableProps {
    components: Component[];
    selectedComponentId: number;
    SetComponent: (component: Component) => void;
}

export default function ComponentsTable(props: ITableProps) {
    const { t } = useTranslation();

    const cols: GridColDef[] = [
        {
            field: "description",
            headerName: t("name.label"),
            width: 250,
        },
        { field: "totalActions", headerName: t("totalactions.label"), width: 150, align: "right" },
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
                onRowClick={(data) => {
                    props.SetComponent(data.row as Component);
                }}
            />
        </div>
    );
}
