import React from "react";
import Navbar from "./components/Navbar";
import Sidebar from "./components/Sidebar";
import Content from "./components/Content";
import styles from "./styles/shared.module.scss";

function App() {
    return (
        <>
            <Navbar/>
            <Wrapper>
                <Sidebar/>
                <Content/>
            </Wrapper>
        </>
    );
}

interface IWrapperProps {
    children: React.ReactNode
}

const Wrapper = ({children} : IWrapperProps) => {
    return <div className={styles.wrapper}>{children}</div>
}

export default App;
