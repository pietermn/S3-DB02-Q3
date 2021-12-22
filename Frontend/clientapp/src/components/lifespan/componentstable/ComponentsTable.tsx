import { useTranslation } from "react-i18next";
import {
    FaExclamationTriangle as WarningIcon,
    FaExclamation as WarnIcon,
    FaTimes as ErrorIcon,
    FaWrench as MaintenanceIcon,
    FaCheck as GoodIcon,
    FaBan as BanIcon,
} from "react-icons/fa";
import { Component, ComponentPredictedMaintenance } from "../../../globalTypes";
import "./ComponentsTableStyle.scss";
import { memo, useContext, useEffect, useState } from "react";
import { DataGrid, GridColDef, GridSortModel } from "@mui/x-data-grid";
import { CircularProgress, IconButton, TextField } from "@mui/material";
import Tooltip, { TooltipProps, tooltipClasses } from "@mui/material/Tooltip";
import { styled } from "@mui/material/styles";
import axios from "axios";
import { MaintenanceContext } from "../../../context/MaintenanceContext";
import { FaBan } from "react-icons/fa";

interface IComponentsTable {
    components: Component[];
    setSelectedComponent: (component: Component) => void;
    predictedMaintenances: ComponentPredictedMaintenance[];
}

const WarningTooltip = styled(({ className, ...props }: TooltipProps) => (
    <Tooltip {...props} classes={{ popper: className }} />
))(({ theme }) => ({
    [`& .${tooltipClasses.tooltip}`]: {
        fontSize: 14,
    },
}));

interface IPredictMaintenance {
    component: Component;
    maintenance: Date;
}

function ComponentsTable(props: IComponentsTable) {
    const [cancelSource] = useState(axios.CancelToken.source());
    const { getComponentMaintenance } = useContext(MaintenanceContext);

    useEffect(() => {
        return () => {
            cancelSource.cancel();
        };
        // eslint-disable-next-line
    }, []);

    function PredictMaintenance(props: IPredictMaintenance) {
        const [loading, setLoading] = useState(true);

        useEffect(() => {
            if (new Date(props.maintenance).toLocaleString() !== new Date().toLocaleString()) {
                setLoading(false);
            }
        }, []);

        if (props.component.maxActions === 1) {
            return (
                <div className="Predict-Text">
                    <i>Cannot predict if no max actions is set</i>
                </div>
            );
        }

        if (new Date(props.maintenance).toLocaleDateString() === "01/01/1") {
            return (
                <div className="Predict-Text">
                    <i>Cannot predict this component</i>
                </div>
            );
        } else if (props.component.currentActions >= props.component.maxActions) {
            return (
                <div className="Predict-Text">
                    <i>Predicted: Has already hit its max actions</i>
                </div>
            );
        } else if (loading) {
            return <CircularProgress size={20} className="CircularProgress" />;
        } else {
            return (
                <div className="Predict-Text">
                    <i>Predicted: {new Date(props.maintenance).toLocaleDateString()}</i>
                </div>
            );
        }
    }

    const { t } = useTranslation();
    const maw = t("maxactionswarning.label");
    const [sortModel, setSortModel] = useState<GridSortModel>([
        {
            field: "percentageMaintenance",
            sort: "desc",
        },
    ]);
    const [searchInput, setSearchInput] = useState("");
    const [innerWidth, setInnerWidth] = useState(window.innerWidth);
    let dgWidth = innerWidth * 0.975;

    const cols: GridColDef[] = [
        {
            field: "status",
            headerName: "",
            align: "center",
            headerAlign: "center",
            renderCell: (params) => {
                switch (true) {
                    case params.row.maxActions === 1:
                        return <BanIcon className="grey" />;
                    case params.row.percentageMaintenance >= 100 && getComponentMaintenance(params.row.id).length > 0:
                        return <MaintenanceIcon className="red" />;
                    case params.row.percentageMaintenance >= 100:
                        return <ErrorIcon className="red" />;
                    case params.row.percentageMaintenance >= 95 &&
                        params.row.percentageMaintenance < 100 &&
                        getComponentMaintenance(params.row.id).length > 0:
                        return <MaintenanceIcon className="orange" />;
                    case params.row.percentageMaintenance >= 95 && params.row.percentageMaintenance < 100:
                        return <WarnIcon className="orange" />;
                    case getComponentMaintenance(params.row.id).length > 0:
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
            disableColumnMenu: true,
            sortable: false,
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
            field: "maintenance",
            headerName: t("maintenance.label"),
            width: dgWidth * 0.25,
            sortable: false,
            filterable: false,
            hideSortIcons: true,
            disableColumnMenu: true,
            renderCell: (params) => {
                let m = getComponentMaintenance(params.row.id);
                return m.length ? (
                    m.length === 1 ? (
                        <div className="MuiDataGrid-cell MuiDataGrid-cell--textLeft">{m[0].description}</div>
                    ) : (
                        <div className="MuiDataGrid-cell MuiDataGrid-cell--textLeft">
                            <b>({m.length})</b> {m[0].description}
                        </div>
                    )
                ) : params.row.percentageMaintenance > 70 && params.row.percentageMaintenance < 100 ? (
                    <PredictMaintenance
                        component={params.row}
                        maintenance={
                            props.predictedMaintenances.find((p) => p.componentId === params.row.id)?.maintenance ||
                            new Date()
                        }
                    />
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
            disableColumnMenu: true,
            width: dgWidth * 0.1,
        },
        {
            field: "currentActions",
            headerName: t("currentactions.label"),
            align: "right",
            headerAlign: "right",
            filterable: false,
            disableColumnMenu: true,
            width: dgWidth * 0.1,
        },
        {
            field: "percentageMaintenance",
            headerName: t("max.label"),
            align: "right",
            headerAlign: "right",
            filterable: false,
            disableColumnMenu: true,
            width: dgWidth * 0.1,
            renderCell: (params) => {
                if (params.row.maxActions === 1) {
                    return (
                        <WarningTooltip title={maw}>
                            <IconButton>
                                <WarningIcon className="orange" />
                            </IconButton>
                        </WarningTooltip>
                    );
                } else {
                    return <div>{params.row.percentageMaintenance}%</div>;
                }
            },
        },
    ];
    return (
        <div className="lifespan-table">
            <DataGrid
                rowBuffer={100}
                rowThreshold={100}
                disableColumnSelector
                disableSelectionOnClick
                className="LsDataGrid"
                columns={cols}
                rows={
                    searchInput
                        ? props.components.filter((c) =>
                              c.description.toLocaleLowerCase().includes(searchInput.toLocaleLowerCase())
                          )
                        : props.components
                }
                rowsPerPageOptions={[]}
                pageSize={100}
                hideFooter
                sortModel={sortModel}
                onSortModelChange={setSortModel}
                onRowClick={(data) => {
                    props.setSelectedComponent(data.row as Component);
                }}
                loading={props.components.length === 0}
                components={{
                    NoRowsOverlay: () => {
                        return null;
                    },
                }}
                onResize={() => {
                    setInnerWidth(window.innerWidth);
                }}
            />
        </div>
    );
}

function compareProps(prevProps: IComponentsTable, nextProps: IComponentsTable) {
    return (
        JSON.stringify(prevProps.components) === JSON.stringify(nextProps.components) &&
        JSON.stringify(prevProps.predictedMaintenances) === JSON.stringify(nextProps.predictedMaintenances)
    );
}

export default memo(ComponentsTable, compareProps);
