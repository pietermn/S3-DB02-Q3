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
    const [innerWidth, setInnerWidth] = useState(window.innerWidth);
    let dgWidth = (innerWidth / 3) * 0.8;

    useEffect(() => {
        window.addEventListener("resize", () => {
            setInnerWidth(window.innerWidth);
        });
    }, []);

    function rows(): Maintenance[] {
        let rows = [...maintenance].sort((a, b) => new Date(b.timeDone).getTime() - new Date(a.timeDone).getTime());
        let first = rows.find((r) => new Date(r.timeDone).getFullYear() === 1);
        if (first) {
            rows.splice(rows.indexOf(first), 1);
            return [first, ...rows];
        }
        return rows;
    }

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
                        {new Date(params.row.timeDone).getFullYear() === 1 ? (
                            <p>
                                {t("finish.label")}
                                <CheckmarkIcon
                                    onClick={() => {
                                        finishMaintenance(params.row.id);
                                    }}
                                />
                            </p>
                        ) : (
                            <p>{new Date(params.row.timeDone).toLocaleString()}</p>
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
            rows={rows()}
            rowsPerPageOptions={[]}
            pageSize={100}
            hideFooter
        />
    );
}
