import { IconButton, TextField, Tooltip } from "@material-ui/core";
import { useTranslation } from "react-i18next";
import { GrStatusGoodSmall as StatusDot } from "react-icons/gr";
import { Component, MaintenanceNotification } from "../../../globalTypes";
import "./ComponentsTableStyle.scss";
import { FaInfoCircle as InfoIcon } from "react-icons/fa";
import { useState } from "react";
import { DataGrid, GridColDef, GridSortModel } from "@mui/x-data-grid";

interface IComponentsTable {
    components: Component[];
    setSelectedComponet: (component: Component) => void;
    getComponentNotifications: (componentId: number) => MaintenanceNotification[];
}

export default function ComponentsTable(props: IComponentsTable) {
    const { t } = useTranslation();
    const [sortModel, setSortModel] = useState<GridSortModel>([
        {
            field: "percentageMaintenance",
            sort: "desc",
        },
    ]);

    function GetStatusColor(percentage: number): string {
        if (percentage >= 95 && percentage < 100) {
            return "orange";
        }

        if (percentage >= 100) {
            return "red";
        } else {
            return "green";
        }
    }

    const cols: GridColDef[] = [
        {
            field: "status",
            headerName: t("status.label"),
            align: "center",
            headerAlign: "center",
            renderCell: (params) => {
                return <StatusDot className={GetStatusColor(params.row.percentageMaintenance)} />;
            },
            sortable: false,
            filterable: false,
            hideSortIcons: true,
            disableColumnMenu: true,
        },
        {
            field: "description",
            headerName: t("name.label"),
            width: 300,
        },
        {
            field: "maintenance",
            headerName: t("maintenance.label"),
            width: 300,
            sortable: false,
            filterable: false,
            hideSortIcons: true,
            disableColumnMenu: true,
            renderCell: (params) => {
                let m = props.getComponentNotifications(params.row.id);
                return m.length ? (
                    m.length === 1 ? (
                        // <Tooltip title={m[0].description}>
                        <div className="MuiDataGrid-cell MuiDataGrid-cell--textLeft">{m[0].description}</div>
                    ) : (
                        // </Tooltip>
                        // <Tooltip title={m[0].description}>
                        <div className="MuiDataGrid-cell MuiDataGrid-cell--textLeft">
                            <b>({m.length})</b> {m[0].description}
                        </div>
                        // </Tooltip>
                    )
                ) : (
                    <div></div>
                );
            },
        },
        {
            field: "totalActions",
            headerName: t("totalactions.label"),
            align: "right",
            headerAlign: "right",
            filterable: false,
            width: 150,
        },
        {
            field: "currentActions",
            headerName: t("currentactions.label"),
            align: "right",
            headerAlign: "right",
            filterable: false,
            width: 150,
        },
        {
            field: "percentageMaintenance",
            headerName: t("max.label"),
            align: "right",
            headerAlign: "right",
            filterable: false,
            width: 150,
            renderCell: (params) => {
                return <div>{params.row.percentageMaintenance}%</div>;
            },
        },
    ];

    return (
        <div className="lifespan-table">
            <DataGrid
                disableColumnSelector
                disableSelectionOnClick
                className="LsDataGrid"
                columns={cols}
                rows={props.components}
                rowsPerPageOptions={[]}
                pageSize={100}
                hideFooter
                sortModel={sortModel}
                onSortModelChange={setSortModel}
                onRowClick={(data) => {
                    props.setSelectedComponet(data.row as Component);
                }}
            />
            {/* <div className="row">
                <p>{t("status.label")}</p>
                <div id="Lifespan-Search">
                    {t("name.label")}
                    <div id="Lifespan-Search-Spacer" />
                    <TextField
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                        label={searchLabel}
                        variant="outlined"
                        size="small"
                    />
                </div>
                <p>{t("totalactions.label")}</p>
                <p>{t("currentactions.label")}</p>
                <div>
                    {t("max.label")} %
                    <Tooltip title={maxTooltip}>
                        <IconButton style={{ fontSize: "1rem" }} disableRipple>
                            <InfoIcon />
                        </IconButton>
                    </Tooltip>
                </div>
            </div>
            {props.components && search
                ? getSearchedComponents()
                      .sort((a, b) => b.percentageMaintenance - a.percentageMaintenance)
                      .map((component) => {
                          return (
                              <div
                                  key={component.id}
                                  onClick={() => props.setSelectedComponet(component)}
                                  className="row"
                              >
                                  <div>
                                      <StatusDot className={GetStatusColor(component.percentageMaintenance)} />
                                  </div>
                                  <p>{component.description}</p>
                                  <p>{component.totalActions}</p>
                                  <p>{component.currentActions}</p>
                                  <p>{component.percentageMaintenance}%</p>
                              </div>
                          );
                      })
                : props.components
                      .sort((a, b) => b.percentageMaintenance - a.percentageMaintenance)
                      .map((component) => {
                          return (
                              <div
                                  key={component.id}
                                  onClick={() => props.setSelectedComponet(component)}
                                  className="row"
                              >
                                  <div>
                                      <StatusDot className={GetStatusColor(component.percentageMaintenance)} />
                                  </div>
                                  <p>{component.description}</p>
                                  <p>{component.totalActions}</p>
                                  <p>{component.currentActions}</p>
                                  <p>{component.percentageMaintenance}%</p>
                              </div>
                          );
                      })}*/}
        </div>
    );
}
