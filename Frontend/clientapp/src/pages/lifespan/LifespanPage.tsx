import './LifespanPage.scss'
import ComponentsTable from '../../components/componenthealth/componentstable/ComponentsTable'

export default function LifespanPage() {
    return (
        <section className="Component-Overview">
            <div className="center-table">
                <h1>Components <i>Sort by total actions</i></h1>
                <ComponentsTable components={[]} />
            </div>
        </section>
    )
}
