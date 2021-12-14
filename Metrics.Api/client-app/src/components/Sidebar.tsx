import React from 'react';
import "../styles/sidebar.scss";
import arrow from "../imgs/Arrow.png";

const Sidebar = () => {
    return (
        <div className="side">
            <SideMenuElement text="Projects" hasSubElements={true}/>
            <SideMenuElement text="Account"/>
            <SideMenuElement text="Support"/>
        </div>
    )
}

interface ISideMenuElementProps {
    text: string;
    hasSubElements?: boolean;
}

const SideMenuElement = ({text, hasSubElements = false}: ISideMenuElementProps) => {
    return (
        <div className="side__element pointer">
            <span className="roboto-font">
                {hasSubElements ? <img src={arrow} alt="-"/> : null}
                {text}
            </span>
        </div>
    )
}

export default Sidebar;