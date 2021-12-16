import { Maintenance } from "../../../globalTypes";
import "./MaintenanceTableStyle.scss";
import { FaCheck as CheckmarkIcon } from "react-icons/fa";
import { DataGrid, GridColDef, GridSortModel } from "@mui/x-data-grid";
import { useTranslation } from "react-i18next";
import { useEffect, useState } from "react";

interface IMaintenanceTable {
    maintenance: Maintenance[];
    finishMaintenance: (id: number) => void;
}

export default function MaintenanceTable({ maintenance, finishMaintenance }: IMaintenanceTable) {
    const { t } = useTranslation();
    const [sortModel, setSortModel] = useState<GridSortModel>([
        {
            field: "timeDone",
            sort: "desc",
        },
    ]);
    const [innerWidth, setInnerWidth] = useState(window.innerWidth);
    let dgWidth = (innerWidth / 3) * 0.8;

    useEffect(() => {
        window.addEventListener("resize", () => {
            setInnerWidth(window.innerWidth);
        });
    }, []);

    const cols: GridColDef[] = [
        {
            field: "description",
            renderHeader: () => {
                return <b>{t("maintenance.label")}</b>;
            },
            sortable: false,
            width: dgWidth * 0.45,
            disableColumnMenu: true,
        },
        {
            field: "timeDone",
            headerClassName: "Maintenance-Table-Last-Header",
            renderHeader: () => {
                return <b>{t("performed.label")}</b>;
            },
            sortable: false,
            width: dgWidth * 0.45,
            disableColumnMenu: true,
            renderCell: (params) => {
                return (
                    <>
                        {params.row.timeDone.toLocaleString() === "0001-01-01 00:00:00" ? (
                            <p>
                                {t("finish.label")}
                                <CheckmarkIcon
                                    onClick={() => {
                                        finishMaintenance(params.row.id);
                                    }}
                                />
                            </p>
                        ) : (
                            <p>{params.row.timeDone.toLocaleString()}</p>
                        )}
                    </>
                );
            },
        },
    ];

    return (
        <DataGrid
            disableColumnSelector
            disableSelectionOnClick
            className="Maintenance-Table"
            columns={cols}
            rows={maintenance}
            rowsPerPageOptions={[]}
            pageSize={100}
            hideFooter
            sortModel={sortModel}
            onSortModelChange={setSortModel}
        />
    );
}
