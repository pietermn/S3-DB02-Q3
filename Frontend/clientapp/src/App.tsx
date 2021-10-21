import { BrowserRouter as Router } from "react-router-dom";
import AppWrapper from "./layout/AppWrapper";
import Routes from "./routes/Routes";
import { NotificationProvider } from "./context/NotificationContext";
import { UpdaterProvider } from "./context/UpdaterContext";

function App() {
  return (
    <Router>
      <UpdaterProvider>
        <NotificationProvider>
          <AppWrapper>
            <Routes />
          </AppWrapper>
        </NotificationProvider>
      </UpdaterProvider>
    </Router>
  );
}

export default App;
