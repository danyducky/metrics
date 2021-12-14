import React, {ChangeEvent, useEffect, useState} from "react";
import "../styles/content.scss";
import {IUser} from "../models/IUser";
import moment from "moment";
import basket from "../imgs/Basket.png";
import uuid from "uuid";
import {Bar, BarChart, Legend, Tooltip, XAxis, YAxis} from "recharts";

const Content = () => {
    return (
        <div className="wrapper">
            <div className="container">
                <div className="title">rolling retention</div>
                <Table/>
            </div>
        </div>
    )
}

const Table = () => {
    const [users, setUsers] = useState<IUser[]>([]);
    const [addedUsers, setAddedUsers] = useState<IUser[]>([]);
    const [retention, setRetention] = useState(null);

    useEffect(() => {
        fetch(window.location.href + 'api/user')
            .then(res => res.json())
            .then(json => setUsers(json));
    }, [])

    const onCalculateClick = () => {
        fetch(window.location.href + 'api/metrics/retention/7')
            .then(res => res.json())
            .then(json => setRetention(json.toFixed(2)))
    }

    const onAddRowClick = () => {
        setAddedUsers(prevState => [...prevState, {id: uuid.v4()} as IUser])
    }

    const onSaveClick = () => {
        const invalids = addedUsers.filter(
            x => (!x.lastActivityDate || !x.registrationDate) || (x.registrationDate > x.lastActivityDate)
        );

        if (invalids.length > 0) return;

        fetch(window.location.href + 'api/user', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({
                dates: addedUsers
            })
        })
            .then(res => res.json())
            .then(json => {
                setAddedUsers([])
                setUsers(prevState => [...prevState, ...json])
            })
    }

    return (
        <>
            <table className="table mt-25">
                <thead>
                <tr>
                    <th>Id</th>
                    <th>Registration date</th>
                    <th>Last Activity</th>
                    <th>Action</th>
                </tr>
                </thead>
                <tbody>
                <TableUserRows users={users} setUsers={setUsers}/>
                <TableAddedUserRows users={addedUsers} setUsers={setAddedUsers}/>
                </tbody>
            </table>

            <div className="btn-wrapper mt-25">
                <input type="button" className="btn pointer" value="Calculate" onClick={onCalculateClick}/>
                <div className="btn-wrapper">
                    {
                        addedUsers.length > 0
                            ? <input type="button" className="btn btn-success pointer" value="Save"
                                     onClick={onSaveClick}/>
                            : null
                    }
                    <input type="button" className="btn pointer ml-25" value="Add new row" onClick={onAddRowClick}/>
                </div>
            </div>
            {
                retention !== null
                    ? <Report users={users} retention={retention}/>
                    : null
            }
        </>
    )
}

interface ITableRowProps {
    users: IUser[];
    setUsers: Function;
}

const TableUserRows = ({users, setUsers}: ITableRowProps) => {
    const onBasketClick = (user: IUser) => {
        fetch(window.location.href + `api/user/${user.id}`,
            {
                method: 'DELETE'
            })
            .then(_ => setUsers(users.filter(x => x.id !== user.id)))
    }

    return (
        <>
            {
                users.map((user, index) => {
                    return (
                        <tr key={user.id}>
                            <td>{index + 1}</td>
                            <td width={500}>{moment(user.registrationDate).format("YYYY-MM-DD")}</td>
                            <td width={300}>{moment(user.lastActivityDate).format("YYYY-MM-DD")}</td>
                            <td>
                                <img src={basket} alt="del" className="pointer" onClick={() => {
                                    onBasketClick(user)
                                }}/>
                            </td>
                        </tr>
                    )
                })
            }
        </>
    )
}

const TableAddedUserRows = ({users, setUsers}: ITableRowProps) => {
    const onRegisterDateChange = (e: ChangeEvent<HTMLInputElement>, user: IUser) => {
        user.registrationDate = new Date(e.target.value);
    }

    const onActivityDateChange = (e: ChangeEvent<HTMLInputElement>, user: IUser) => {
        user.lastActivityDate = new Date(e.target.value);
    }

    const onBasketClick = (user: IUser) => {
        setUsers(users.filter(x => x.id !== user.id))
    }

    return (
        <>
            {
                users.map((user) => {
                    return (
                        <tr key={user.id}>
                            <td>#</td>
                            <td width={500}>
                                <input type="date"
                                       className="user-date"
                                       required={true}
                                       onChange={(e) => onRegisterDateChange(e, user)}
                                />
                            </td>
                            <td width={300}>
                                <input type="date"
                                       className="user-date"
                                       required={true}
                                       onChange={(e) => onActivityDateChange(e, user)}
                                />
                            </td>
                            <td>
                                <img src={basket} alt="del" className="pointer" onClick={() => {
                                    onBasketClick(user)
                                }}/>
                            </td>
                        </tr>
                    )
                })
            }
        </>
    )
}

interface IReportProps {
    users: IUser[];
    retention: number;
}

interface IReportUser {
    id: number;
    lifetime: number;
}

const Report = ({users, retention}: IReportProps) => {
    const data = users.map((user, index) => {
        const registration = moment(user.registrationDate);
        const lastActivity = moment(user.lastActivityDate);
        const days = lastActivity.diff(registration, 'day')

        return {id: index + 1, lifetime: days} as IReportUser
    })

    return (
        <>
            <div className="title mt-25">Rolling retention 7 day is {retention}%</div>
            <BarChart className="mt-50" width={1280} height={250} data={data}>
                <XAxis dataKey="id" dy={5} />
                <YAxis label="days" />
                <Tooltip/>
                <Legend/>
                <Bar dataKey="lifetime" fill="#4A9DFF"/>
            </BarChart>
        </>
    )
}

export default Content;