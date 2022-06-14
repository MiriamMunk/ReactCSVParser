import React, { useState, useEffect } from 'react';
import PersonRow from '../Components/PersonRow';
import axios from 'axios';

const HomePage = () => {

    const [people, setPeople] = useState([]);

    useEffect(() => {
        const getPeople = async() => {
            const { data } = await axios.get('/api/csv/getPeople');
            setPeople(data);
        }

        getPeople();
    }, [])

    const onDeleteClick = async () => {
        await axios.get('/api/csv/delete');
        setPeople([]);
    }

    return (<div className="container">
        <div className="col-md-6 offset-md-3">
            <button className="btn btn-danger btn-lg btn-block" onClick={onDeleteClick} > Delete All</button>
        </div>
        <table className="table table-striped table-hover table-bordered mt-4">
            <thead className="table-dark">
                <tr>
                    <th>Id</th>
                    <th>First Name</th>
                    <th>LastName</th>
                    <th>Age</th>
                    <th>Address</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                {people && people.map(p => <PersonRow person={p} key={p.id} />)}
            </tbody>
        </table>
    </div>)
}
export default HomePage;