import { useTranslation } from "react-i18next";
import { Component } from "../../../globalTypes";
import "../HistoryTable/HistoryTable.scss";
import "./ComponentsTable.scss";
import { DataGrid, GridColDef, GridSortModel } from "@mui/x-data-grid";
import { useState } from "react";
import { TextField } from "@material-ui/core";

interface ITableProps {
    components: Component[];
    selectedComponentId: number;
    SetComponent: (component: Component) => void;
}

export default function ComponentsTable(props: ITableProps) {
    const [searchInput, setSearchInput] = useState("");
    const [sortModel, setSortModel] = useState<GridSortModel>([
        {
            field: "totalActions",
            sort: "desc",
        },
    ]);
    const { t } = useTranslation();
    const [innerWidth, setInnerWidth] = useState(window.innerWidth);
    let dgWidth = (innerWidth / 3 - 96) * 0.8;

    const cols: GridColDef[] = [
        {
            field: "description",
            headerName: t("name.label"),
            width: dgWidth * 0.7,
            sortable: false,
            disableColumnMenu: true,
            renderHeader: () => {
                return (
                    <div className="LS-Header-Description">
                        <b>{t("name.label")}</b>
                        <TextField
                            value={searchInput}
                            onChange={(e) => setSearchInput(e.target.value)}
                            label={t("searchtag.label")}
                            variant="standard"
                            size="small"
                        />
                    </div>
                );
            },
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
                rows={
                    searchInput
                        ? props.components.filter((c) =>
                              c.description.toLowerCase().includes(searchInput.toLowerCase())
                          )
                        : props.components
                }
                rowsPerPageOptions={[]}
                pageSize={100}
                hideFooter
                sortModel={sortModel}
                onSortModelChange={setSortModel}
                onRowClick={(data) => {
                    props.SetComponent(data.row as Component);
                }}
                selectionModel={props.selectedComponentId}
                onResize={() => {
                    setInnerWidth(window.innerWidth);
                }}
            />
        </div>
    );
}
