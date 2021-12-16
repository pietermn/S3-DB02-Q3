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
import { useEffect, useRef, useState } from "react";
import { DataGrid, GridColDef, GridSortModel } from "@mui/x-data-grid";
import { IconButton, LinearProgress, TextField } from "@mui/material";
import Tooltip, { TooltipProps, tooltipClasses } from "@mui/material/Tooltip";
import { styled } from "@mui/material/styles";
import { PredictMaintenance as ApiPredictMaintenance } from "../../../api/requests/components";

interface IComponentsTable {
    components: Component[];
    setSelectedComponent: (component: Component) => void;
    getComponentNotifications: (componentId: number) => MaintenanceNotification[];
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
}

export default function ComponentsTable(props: IComponentsTable) {
    function PredictMaintenance(props: IPredictMaintenance) {
        const [date, setDate] = useState(new Date());

        async function asyncGetPrediction() {
            setDate(await ApiPredictMaintenance(props.component.id));
        }

        useEffect(() => {
            asyncGetPrediction();
        }, []);

        if (props.component.maxActions === 1) {
            return <div>Cannot predict if no max actions is set</div>;
        }

        if (new Date(date).toLocaleDateString() === "01/01/1") {
            return <div>Cannot predict this component</div>;
        } else if (props.component.currentActions >= props.component.maxActions) {
            return <div>Has already hit its max actions</div>;
        } else {
            return <div>{new Date(date).toLocaleDateString()}</div>;
        }
    }

    // function GetMaintenanceRow(component: Component) {
    //     let m = props.getComponentNotifications(component.id);
    //     const [predictedMaintenance, setPredictedMaintenance] = useState("loading...");
    //     const ref = useRef(false);
    //     if (component.percentageMaintenance > 95 && component.percentageMaintenance < 100 && ref.current === false) {
    //         console.log(component);

    //         ref.current = true;
    //         PredictMaintenance(component, setPredictedMaintenance);
    //     }
    //     return m.length ? (
    //         m.length === 1 ? (
    //             <div className="MuiDataGrid-cell MuiDataGrid-cell--textLeft">{m[0].description}</div>
    //         ) : (
    //             <div className="MuiDataGrid-cell MuiDataGrid-cell--textLeft">
    //                 <b>({m.length})</b> {m[0].description}
    //             </div>
    //         )
    //     ) : component.percentageMaintenance > 95 && component.percentageMaintenance < 100 ? (
    //         <div>
    //             {predictedMaintenance === "loading..." ? (
    //                 <LinearProgress color="primary" className="LS-LinearProgress" />
    //             ) : (
    //                 predictedMaintenance
    //             )}
    //         </div>
    //     ) : (
    //         <div></div>
    //     );
    // }

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
    let dgWidth = innerWidth * 0.8;

    useEffect(() => {
        window.addEventListener("resize", () => {
            setInnerWidth(window.innerWidth);
        });
    }, []);

    const cols: GridColDef[] = [
        {
            field: "status",
            headerName: "",
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
                let m = props.getComponentNotifications(params.row.id);
                return m.length ? (
                    m.length === 1 ? (
                        <div className="MuiDataGrid-cell MuiDataGrid-cell--textLeft">{m[0].description}</div>
                    ) : (
                        <div className="MuiDataGrid-cell MuiDataGrid-cell--textLeft">
                            <b>({m.length})</b> {m[0].description}
                        </div>
                    )
                ) : params.row.percentageMaintenance > 95 && params.row.percentageMaintenance < 100 ? (
                    <PredictMaintenance component={params.row} />
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
                        <WarningTooltip title={maw}>
                            <IconButton>
                                <WarningIcon className="orange" />
                            </IconButton>
                        </WarningTooltip>
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
            />
        </div>
    );
}
