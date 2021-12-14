import React from 'react';
import "../styles/navbar.scss";
import account from "../imgs/Account.png";
import exit from "../imgs/Exit.png";

const Navbar = () => {
    return (
        <nav className="nav">
            <div className="nav__element">
                <span className="roboto-font roboto-font_mod nav__element_vertical-middle pointer">
                    {process.env.REACT_APP_COMPANY_NAME}
                </span>
                <input type="search" className="nav__element_search nav__element_search-icon ml-50"/>
            </div>
            <div className="nav__element">
                <img className="nav__element_logo-icon pointer" src={account} alt="acc" />
                <img className="nav__element_exit-icon pointer" src={exit} alt="exit"/>
            </div>
        </nav>
    )
}

export default Navbar;