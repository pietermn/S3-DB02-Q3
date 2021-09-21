import { combineReducers } from 'redux'
import { connectRouter } from 'connected-react-router'

const history = () => 
    combineReducers({
        router: connectRouter(history)
    })

export default history;