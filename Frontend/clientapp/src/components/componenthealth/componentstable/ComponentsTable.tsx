import { TextField } from "@material-ui/core";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { Component } from "../../../globalTypes";
import "../HistoryTable/HistoryTable.scss";
import "./ComponentsTable.scss";

interface ITableProps {
    components: Component[];
    selectedComponentId: number;
    SetComponent: (component: Component) => void;
}

export default function ComponentsTable(props: ITableProps) {
    const [search, setSearch] = useState("");
    const { t } = useTranslation();

    function getSearchedComponents() {
        return props.components.filter((c) => c.description.toLowerCase().includes(search.toLowerCase()));
    }

    return (
        <div className="Components-Table">
            <main>
                <div className="row header">
                    <p>
                        {t("name.label")}
                        <div id="chealth-spacer" />
                        <TextField
                            value={search}
                            onChange={(e) => setSearch(e.target.value)}
                            label="Search"
                            variant="outlined"
                            size="small"
                        />
                    </p>
                    <p>{t("totalactions.label")}</p>
                </div>
                {search
                    ? getSearchedComponents().map((component: Component, index: number) => {
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
                    : props.components.map((component: Component, index: number) => {
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
            </main>
        </div>
    );
}
