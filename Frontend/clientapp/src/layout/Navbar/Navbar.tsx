import { useLocation, useHistory } from 'react-router';
import { VscAccount as AccountIcon } from 'react-icons/vsc';
import { FiLogOut as LogOutIcon } from 'react-icons/fi';
import { FaBell as BellIcon } from 'react-icons/fa'
import Q3Logo from "../../assets/LOGO_Q3_White.png";
import "./NavbarStyles.scss";

export default function Navbar() {

    return (
        <nav>
            <NavbarRedirects />
            <NavbarUserSection />
        </nav>
    )
}

function NavbarRedirects() {
    const { pathname } = useLocation();
    const history = useHistory();

    return (
        <section>
            <img alt="Q3" src={Q3Logo} />
            <p className={pathname === "/monitoring" ? "bold-text" : ""} onClick={() => history.push("monitoring")}>Machine Monitoring</p>
            <p className={pathname === "/chealth" ? "bold-text" : ""} onClick={() => history.push("chealth")}>Component Health</p>
            <p className={pathname === "/lifespan" ? "bold-text" : ""} onClick={() => history.push("lifespan")}>Lifespan</p>
        </section>
    )
}

function NavbarUserSection() {
    return (
        <section>
            <div className="Name-Container">
                <p>First name</p>
                <p>Last name</p>
            </div>
            <AccountIcon />
            <div className='Notification-Dropdown'>
                <BellIcon />
                <div className='Notification-Dropdown-Content'>
                    <b>Saladevork 4-tands</b><br />
                    Vervang o-ringen
                </div>
            </div>
            <LogOutIcon />
        </section>
    )
}