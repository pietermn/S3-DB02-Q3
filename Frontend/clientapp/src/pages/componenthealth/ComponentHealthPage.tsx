import ComponentsTable from "../../components/componenthealth/componentstable/ComponentsTable";
import HistoryTable from "../../components/componenthealth/HistoryTable/HistoryTable";
import "./ComponentHealthPage.scss";

export default function ComponentHealthPage() {
    return (
        <div className="Components-Full-Page">
            <section className="Component-Overview">
                <div className="center-table">
                    <h1>Components</h1>
                    <ComponentsTable components={[]} />
                </div>
            </section>

            <section className="Component-History-Overview">
                <h1>History <i>Component</i></h1>
                <HistoryTable />
            </section>

            <section className="Component-Graph">

            </section>
        </div>
    )
}