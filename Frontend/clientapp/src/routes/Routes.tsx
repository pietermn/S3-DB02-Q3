import { Suspense } from 'react'
import { Switch, Route, Redirect } from 'react-router'
import PageLoader from '../layout/PageLoader'
import MachineMonitoringPage from '../pages/machinemonitoring/MachineMonitoringPage'
import ComponentHealthPage from '../pages/componenthealth/ComponentHealthPage';

export default function Routes() {
    return (
        <Suspense fallback={<PageLoader />}>
            <Switch>
                <Route exact path='/chealth' component={ComponentHealthPage} />
                <Route exact path='/monitoring' component={MachineMonitoringPage} />
                <Route path='/'>
                    <Redirect to='/monitoring' />
                </Route>
            </Switch>
        </Suspense>
    )
}