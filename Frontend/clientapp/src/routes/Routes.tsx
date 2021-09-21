import { Suspense } from 'react'
import { Switch, Route, Redirect } from 'react-router'
import PageLoader from '../layout/PageLoader'

export default function Routes() {
    return (
        <Suspense fallback={<PageLoader />}>
            <Switch>
                <Route path='/home' component={() => <div>Home</div>} />
                <Route exact path='/'>
                    <Redirect to='/home' />
                </Route>
            </Switch>
        </Suspense>
    )
}