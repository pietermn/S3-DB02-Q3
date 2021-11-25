import { TextField } from "@material-ui/core";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { Component } from "../../../globalTypes";
import { IoMdArrowDropdown as ArrowDownIcon, IoMdArrowDropup as ArrowUpIcon } from "react-icons/io";
import "../HistoryTable/HistoryTable.scss";
import "./ComponentsTable.scss";
import { DataGrid, GridColDef } from "@mui/x-data-grid";

interface ITableProps {
    components: Component[];
    selectedComponentId: number;
    SetComponent: (component: Component) => void;
}

export default function ComponentsTable(props: ITableProps) {
    const [search, setSearch] = useState("");
    const [orderByName, setOrderByName] = useState(true);
    const [orderDown, setOrderDown] = useState(true);
    const [components, setComponents] = useState(props.components);
    const { t } = useTranslation();
    const searchLabel = t("searchtag.label");

    function getSearchedComponents() {
        return props.components.filter((c) => c.description.toLowerCase().includes(search.toLowerCase()));
    }

    function searchHandler(e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) {
        // if (e.target.value) {
        //     setComponents(getSearchedComponents());
        // } else {
        //     setComponents(props.components);
        // }
        setSearch(e.target.value);
    }

    function orderHandler(name: boolean) {
        if (name) {
            if (orderByName) {
                setOrderDown(!orderDown);
            } else {
                setOrderByName(true);
                setOrderDown(true);
            }
        } else {
            if (!orderByName) {
                setOrderDown(!orderDown);
            } else {
                setOrderByName(false);
                setOrderDown(true);
            }
        }
    }

    function order(components: Component[]) {
        let c = components;
        if (orderByName) {
            c = components
                .sort((a, b) => {
                    return a.description.toLowerCase().localeCompare(b.description.toLowerCase());
                })
                .reverse();
        } else {
            c = components.sort((a, b) => {
                if (a.totalActions < b.totalActions) return -1;
                if (a.totalActions > b.totalActions) return 1;
                return 0;
            });
        }
        if (orderDown) {
            return c.reverse();
        } else {
            return c;
        }
    }

    const cols: GridColDef[] = [
        { field: "description", headerName: "Name" },
        { field: "totalActions", headerName: "Total Actions" },
    ];

    return (
        <div className="Components-Table">
            <DataGrid
                className="ChDataGrid"
                columns={cols}
                rows={props.components}
                rowsPerPageOptions={[]}
                pageSize={100}
            />

            {/* <main>
                <div className="row header">
                    <div id="chealth-search">
                        <p onClick={() => orderHandler(true)}>
                            {t("name.label")}
                            {orderByName && (orderDown ? <ArrowDownIcon /> : <ArrowUpIcon />)}
                        </p>
                        <TextField
                            value={search}
                            onChange={(e) => searchHandler(e)}
                            label={searchLabel}
                            variant="outlined"
                            size="small"
                        />
                    </div>
                    <p onClick={() => orderHandler(false)}>
                        {t("totalactions.label")}
                        {!orderByName && (orderDown ? <ArrowDownIcon /> : <ArrowUpIcon />)}
                    </p>
                </div>
                {search
                    ? order(getSearchedComponents()).map((component: Component, index: number) => {
                          return (
                              <div
                                  key={index}
                                  onClick={() => props.SetComponent(component)}
                                  className={props.selectedComponentId == component.id ? "row selected" : "row"}
                              >
                                  <p>{component.description}</p>
                                  <p>{component.totalActions}</p>
                              </div>
                          );
                      })
                    : order(props.components).map((component: Component, index: number) => {
                          return (
                              <div
                                  key={index}
                                  onClick={() => props.SetComponent(component)}
                                  className={props.selectedComponentId == component.id ? "row selected" : "row"}
                              >
                                  <p>{component.description}</p>
                                  <p>{component.totalActions}</p>
                              </div>
                          );
                      })}
            </main> */}
        </div>
    );
}
