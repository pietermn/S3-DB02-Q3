import { useTranslation } from "react-i18next";
import { GrStatusGoodSmall as StatusDot } from "react-icons/gr";
import { Component } from "../../../globalTypes";
import "./ComponentsTableStyle.scss";

interface IComponentsTable {
  components: Component[];
  setSelectedComponet: (component: Component) => void;
}

export default function ComponentsTable(props: IComponentsTable) {
  const { t } = useTranslation();

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

  return (
    <div className="lifespan-table">
      <div className="row">
        <p>{t("status.label")}</p>
        <p>{t("name.label")}</p>
        <p>{t("currentactions.label")}</p>
        <p>{t("max.label")} %</p>
      </div>
      {props.components &&
        props.components
          .sort((a, b) => b.percentageMaintenance - a.percentageMaintenance)
          .map((component, index) => {
            return (
              <div onClick={() => props.setSelectedComponet(component)} className="row">
                <p>
                  <StatusDot className={GetStatusColor(component.percentageMaintenance)} />
                </p>
                <p>{component.description}</p>
                <p>{component.currentActions}</p>
                <p>{component.percentageMaintenance}%</p>
              </div>
            );
          })}
    </div>
  );
}
