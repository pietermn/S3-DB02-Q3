import { Suspense } from 'react'
import { Switch, Route, Redirect } from 'react-router'
import PageLoader from '../layout/PageLoader'
import MachineMonitoringPage from '../pages/machinemonitoring/MachineMonitoringPage'
import ComponentHealthPage from '../pages/componenthealth/ComponentHealthPage';
import LifespanPage from '../pages/lifespan/LifespanPage';

export default function Routes() {
    return (
        <Suspense fallback={<PageLoader />}>
            <Switch>
                <Route exact path='/chealth' component={ComponentHealthPage} />
                <Route exact path='/monitoring' component={MachineMonitoringPage} />
                <Route exact path='/lifespan' component={LifespanPage} />
                <Route path='/'>
                    <Redirect to='/monitoring' />
                </Route>
            </Switch>
        </Suspense>
    )
}