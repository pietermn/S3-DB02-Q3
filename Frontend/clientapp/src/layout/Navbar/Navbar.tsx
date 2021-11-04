import { useLocation, useHistory } from "react-router";
import { VscAccount as AccountIcon } from "react-icons/vsc";
import { FiLogOut as LogOutIcon } from "react-icons/fi";
import { FaBell as BellIcon, FaWrench as WrenchIcon } from "react-icons/fa";
import Q3Logo from "../../assets/LOGO_Q3_White.png";
import "./NavbarStyles.scss";
import { NotificationContext } from "../../context/NotificationContext";
import { ReactNode, Suspense, useContext, useEffect, useState } from "react";
import { MaintenanceContext } from "../../context/MaintenanceContext";
import { MaintenanceNotification } from "../../globalTypes";
import { useTranslation } from "react-i18next";
import PageLoader from "../PageLoader";
//@ts-ignore
import ReactCountryFlag from "react-country-flag";
import i18n from "../../i18n";
import ReactPlayer from "react-player";

export default function Navbar() {
  return (
    <Suspense fallback={<PageLoader />}>
      <nav>
        <NavbarRedirects />
        <NavbarUserSection />
      </nav>
    </Suspense>
  );
}

function NavbarRedirects() {
  const { pathname } = useLocation();
  const history = useHistory();
  const { t } = useTranslation();

  return (
    <section>
      <img alt="Q3" src={Q3Logo} />
      <p className={pathname === "/monitoring" ? "bold-text" : ""} onClick={() => history.push("monitoring")}>
        {t("mm.label")}
      </p>
      <p className={pathname === "/chealth" ? "bold-text" : ""} onClick={() => history.push("chealth")}>
        {t("chealth.label")}
      </p>
      <p className={pathname === "/lifespan" ? "bold-text" : ""} onClick={() => history.push("lifespan")}>
        {t("lifespan.label")}
      </p>
    </section>
  );
}

interface INotifictionDropdown {
  title: string;
  icon: ReactNode;
  notifications: MaintenanceNotification[];
}

function NotificationDropdown({ title, icon, notifications }: INotifictionDropdown) {
  const history = useHistory();
  const { t } = useTranslation();

  return (
    <div className="Notification-Dropdown">
      {icon}
      <div className="Notification-Dropdown-Content">
        <h3>{title}</h3>
        {notifications.length ? (
          notifications.map((n, i) => {
            return (
              <div
                onClick={() => {
                  history.push({
                    pathname: "/lifespan",
                  });
                  history.replace({ state: { componentId: n.componentId } });
                }}
              >
                {n.component}
                <br />
              </div>
            );
          })
        ) : (
          <i>{t("everythinglooksgood.label")}</i>
        )}
      </div>
    </div>
  );
}

function NavbarUserSection() {
  const { notifications } = useContext(NotificationContext);
  const { maintenance } = useContext(MaintenanceContext);
  const { t } = useTranslation();
  const [openFlags, setOpenFlags] = useState(false);
  const [currentLang, setCurrentLang] = useState("en");
  const [germanCounter, setGermanCounter] = useState(0);
  console.log(germanCounter);

  useEffect(() => {
    i18n.changeLanguage(currentLang);
  }, [currentLang]);

  return (
    <section>
      <div className="Name-Container">
        <p>
          {t("first.label")}
          {t("name.label").toLowerCase()}
        </p>
        <p>
          {t("last.label")}
          {t("name.label").toLowerCase()}
        </p>
      </div>
      <AccountIcon />
      <ReactPlayer url="https://youtu.be/D-cZVjSLhSE?t=10" playing={germanCounter >= 10} style={{ display: "none" }} />
      <NotificationDropdown title={t("notifications.label")} icon={<BellIcon />} notifications={notifications} />
      <NotificationDropdown title={t("maintenance.label")} icon={<WrenchIcon />} notifications={maintenance} />
      <div className="Flags" onClick={() => setOpenFlags(!openFlags)}>
        <ReactCountryFlag
          onClick={() => {
            if (currentLang == "de") setGermanCounter(germanCounter + 1);
          }}
          countryCode={currentLang == "en" ? "GB" : currentLang.toUpperCase()}
          svg
          id="Flag__Icon"
        />
        {openFlags && (
          <div className="Flags__Dropdown">
            <ReactCountryFlag countryCode="NL" svg id="Flag__Icon" onClick={() => setCurrentLang("nl")} />
            <ReactCountryFlag countryCode="GB" svg id="Flag__Icon" onClick={() => setCurrentLang("en")} />
            <ReactCountryFlag countryCode="DE" svg id="Flag__Icon" onClick={() => setCurrentLang("de")} />
          </div>
        )}
      </div>
      <LogOutIcon />
    </section>
  );
}
