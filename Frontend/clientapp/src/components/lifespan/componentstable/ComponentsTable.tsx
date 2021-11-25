import { IconButton, TextField, Tooltip } from "@material-ui/core";
import { useTranslation } from "react-i18next";
import { GrStatusGoodSmall as StatusDot } from "react-icons/gr";
import { Component } from "../../../globalTypes";
import "./ComponentsTableStyle.scss";
import { FaInfoCircle as InfoIcon } from "react-icons/fa";
import { useState } from "react";

interface IComponentsTable {
    components: Component[];
    setSelectedComponet: (component: Component) => void;
}

export default function ComponentsTable(props: IComponentsTable) {
    const [search, setSearch] = useState("");
    const { t } = useTranslation();
    const maxTooltip = t("maxtooltip.label");
    const searchLabel = t("searchtag.label");

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

    function getSearchedComponents() {
        return props.components.filter((c) => c.description.toLowerCase().includes(search.toLowerCase()));
    }

    return (
        <div className="lifespan-table">
            <div className="row">
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
                      })}
        </div>
    );
}
