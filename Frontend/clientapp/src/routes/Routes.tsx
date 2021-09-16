import { lazy, Suspense } from 'react'
import { Switch, Route, Redirect } from 'react-router'
import PageLoader from '../layout/PageLoader'
import MachineMonitoringPage from '../pages/machinemonitoring/MachineMonitoringPage'

export default function Routes() {
    return (
        <Suspense fallback={<PageLoader />}>
            <Switch>
                <Route path='/home' component={() => <div>Home</div>} />
                <Route exact path='/'>
                    <Redirect to='/home' />
                </Route>
                <Route path='/monitoring' component={() => <MachineMonitoringPage />} />
            </Switch>
        </Suspense>
    )
}