import { BrowserRouter as Router } from "react-router-dom";
import AppWrapper from "./layout/AppWrapper";
import Routes from "./routes/Routes";
import { NotificationProvider } from "./context/NotificationContext";

function App() {
  return (
    <Router>
      <NotificationProvider>
        <AppWrapper>
          <Routes />
        </AppWrapper>
      </NotificationProvider>
    </Router>
  );
}

export default App;
