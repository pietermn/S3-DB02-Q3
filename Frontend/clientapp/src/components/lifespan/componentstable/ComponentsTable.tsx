import { useTranslation } from "react-i18next";
import {
    FaExclamationTriangle as WarningIcon,
    FaExclamation as WarnIcon,
    FaTimes as ErrorIcon,
    FaWrench as MaintenanceIcon,
    FaCheck as GoodIcon,
} from "react-icons/fa";
import { Component, MaintenanceNotification } from "../../../globalTypes";
import "./ComponentsTableStyle.scss";
import { useEffect, useState } from "react";
import { DataGrid, GridColDef, GridSortModel } from "@mui/x-data-grid";
import { Tooltip } from "@material-ui/core";
import { IconButton } from "@mui/material";

interface IComponentsTable {
    components: Component[];
    setSelectedComponet: (component: Component) => void;
    getComponentNotifications: (componentId: number) => MaintenanceNotification[];
}

export default function ComponentsTable(props: IComponentsTable) {
    const { t } = useTranslation();
    const maw = t("maxactionswarning.label");
    const [sortModel, setSortModel] = useState<GridSortModel>([
        {
            field: "percentageMaintenance",
            sort: "desc",
        },
    ]);
    const [innerWidth, setInnerWidth] = useState(window.innerWidth);
    let dgWidth = innerWidth * 0.8;

    useEffect(() => {
        window.addEventListener("resize", () => {
            setInnerWidth(window.innerWidth);
        });
    }, []);

    const cols: GridColDef[] = [
        {
            field: "status",
            headerName: t("status.label"),
            align: "center",
            headerAlign: "center",
            renderCell: (params) => {
                switch (true) {
                    case params.row.percentageMaintenance >= 100 &&
                        props.getComponentNotifications(params.row.id).length > 0:
                        return <MaintenanceIcon className="red" />;
                    case params.row.percentageMaintenance >= 100:
                        return <ErrorIcon className="red" />;
                    case params.row.percentageMaintenance >= 95 &&
                        params.row.percentageMaintenance < 100 &&
                        props.getComponentNotifications(params.row.id).length > 0:
                        return <MaintenanceIcon className="orange" />;
                    case params.row.percentageMaintenance >= 95 && params.row.percentageMaintenance < 100:
                        return <WarnIcon className="orange" />;
                    case props.getComponentNotifications(params.row.id).length > 0:
                        return <MaintenanceIcon className="green" />;
                    default:
                        return <GoodIcon className="green" />;
                }
            },
            sortable: false,
            filterable: false,
            hideSortIcons: true,
            disableColumnMenu: true,
            width: dgWidth * 0.05,
        },
        {
            field: "description",
            headerName: t("name.label"),
            width: dgWidth * 0.25,
        },
        {
            field: "maintenance",
            headerName: t("maintenance.label"),
            width: dgWidth * 0.25,
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
            width: dgWidth * 0.1,
        },
        {
            field: "currentActions",
            headerName: t("currentactions.label"),
            align: "right",
            headerAlign: "right",
            filterable: false,
            width: dgWidth * 0.1,
        },
        {
            field: "percentageMaintenance",
            headerName: t("max.label"),
            align: "right",
            headerAlign: "right",
            filterable: false,
            width: dgWidth * 0.1,
            renderCell: (params) => {
                return <div>{params.row.percentageMaintenance}%</div>;
            },
        },
        {
            field: "warning",
            headerName: "",
            sortable: false,
            filterable: false,
            disableColumnMenu: true,
            align: "center",
            headerAlign: "center",
            width: dgWidth * 0.05,
            renderCell: (params) => {
                if (params.row.maxActions === 1) {
                    return (
                        <Tooltip title={maw}>
                            <IconButton>
                                <WarningIcon className="orange" />
                            </IconButton>
                        </Tooltip>
                    );
                }
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
                loading={props.components.length === 0}
                components={{
                    NoRowsOverlay: () => {
                        return null;
                    },
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
