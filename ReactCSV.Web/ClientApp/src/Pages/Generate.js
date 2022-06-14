import React, { useState } from 'react';

const Generate = () => {

    const [amount, setAmount] = useState('')

    const onButtonClick = async () => {
        window.location.href = await `/api/csv/generatePeople?amount=${amount}`;
    }

    return <div className="container col-md-6 offset-md-3 ">
        <div className="row">
            <div className="col-md-8">
                <input type="text" className="form-control" placeholder="Amount" onChange={e => setAmount(e.target.value)} />
            </div>
            <div className="col-md-2">
                <button className="btn btn-primary" onClick={onButtonClick}>Generate</button>
            </div>
        </div>
    </div>

}
export default Generate;