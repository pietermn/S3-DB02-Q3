import { useLocation, useHistory } from 'react-router';
import { VscAccount } from 'react-icons/vsc';
import { FiLogOut } from 'react-icons/fi';
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
            <img alt="Q3" src={require("../../assets/LOGO_Q3.png").default} />
            <p className={pathname === "/monitoring" ? "bold-text" : ""} onClick={() => history.push("monitoring")}>Machine Monitoring</p>
            <p className={pathname === "/chealth" ? "bold-text" : ""} onClick={() => history.push("chealth")}>Component Health</p>
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
            <VscAccount id="Profile-Icon" />
            <FiLogOut id="Logout-Icon" />
        </section>
    )
}