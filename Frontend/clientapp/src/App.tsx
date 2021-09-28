import { ConnectedRouter } from "connected-react-router";
import { Provider } from "react-redux";
import configureStore, { history } from "./redux/store";
import AppWrapper from "./layout/AppWrapper";
import Routes from "./routes/Routes";

const store = configureStore();

function App() {
  return (
    <Provider store={store}>
      <ConnectedRouter history={history}>
        <AppWrapper>
          <Routes />
        </AppWrapper>
      </ConnectedRouter>
    </Provider>
  );
}

export default App;
