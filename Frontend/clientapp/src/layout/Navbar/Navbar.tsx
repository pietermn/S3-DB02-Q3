import { useLocation, useHistory } from "react-router";
import { VscAccount as AccountIcon } from "react-icons/vsc";
import { FiLogOut as LogOutIcon } from "react-icons/fi";
import { FaBell as BellIcon, FaWrench as WrenchIcon } from "react-icons/fa";
import Q3Logo from "../../assets/LOGO_Q3_White.png";
import "./NavbarStyles.scss";
import { NotificationContext } from "../../context/NotificationContext";
import { ReactNode, useContext } from "react";
import { MaintenanceContext } from "../../context/MaintenanceContext";
import { MaintenanceNotification } from "../../globalTypes";

export default function Navbar() {
  return (
    <nav>
      <NavbarRedirects />
      <NavbarUserSection />
    </nav>
  );
}

function NavbarRedirects() {
  const { pathname } = useLocation();
  const history = useHistory();

  return (
    <section>
      <img alt="Q3" src={Q3Logo} />
      <p
        className={pathname === "/monitoring" ? "bold-text" : ""}
        onClick={() => history.push("monitoring")}
      >
        Machine Monitoring
      </p>
      <p
        className={pathname === "/chealth" ? "bold-text" : ""}
        onClick={() => history.push("chealth")}
      >
        Component Health
      </p>
      <p
        className={pathname === "/lifespan" ? "bold-text" : ""}
        onClick={() => history.push("lifespan")}
      >
        Lifespan
      </p>
    </section>
  );
}

interface INotifictionDropdown {
  title: string;
  icon: ReactNode;
  notifications: MaintenanceNotification[];
}

function NotificationDropdown({
  title,
  icon,
  notifications,
}: INotifictionDropdown) {
  return (
    <div className="Notification-Dropdown">
      {icon}
      <div className="Notification-Dropdown-Content">
        <h3>{title}</h3>
        {notifications.length ? (
          notifications.map((n, i) => {
            return (
              <div>
                {n.component}
                <br />
              </div>
            );
          })
        ) : (
          <i>Everything looks good</i>
        )}
      </div>
    </div>
  );
}

function NavbarUserSection() {
  const { notifications } = useContext(NotificationContext);
  const { maintenance } = useContext(MaintenanceContext);

  console.log(notifications);

  return (
    <section>
      <div className="Name-Container">
        <p>First name</p>
        <p>Last name</p>
      </div>
      <AccountIcon />
      <NotificationDropdown
        title="Notifications"
        icon={<BellIcon />}
        notifications={notifications}
      />
      <NotificationDropdown
        title="Maintenance"
        icon={<WrenchIcon />}
        notifications={maintenance}
      />
      <LogOutIcon />
    </section>
  );
}
