import { BrowserRouter as Router } from "react-router-dom";
import AppWrapper from "./layout/AppWrapper";
import Routes from "./routes/Routes";
import { NotificationProvider } from "./context/NotificationContext";
import { UpdaterProvider } from "./context/UpdaterContext";
import { MaintenanceProvider } from "./context/MaintenanceContext";
import { SocketProvider } from "./context/SocketContext";
import "./i18n";

function App() {
  return (
    <Router>
      <SocketProvider>
        <UpdaterProvider>
          <MaintenanceProvider>
            <NotificationProvider>
              <AppWrapper>
                <Routes />
              </AppWrapper>
            </NotificationProvider>
          </MaintenanceProvider>
        </UpdaterProvider>
      </SocketProvider>
    </Router>
  );
}

export default App;
