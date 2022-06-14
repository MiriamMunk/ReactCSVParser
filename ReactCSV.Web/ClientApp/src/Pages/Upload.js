import React, { useRef } from 'react';
import axios from 'axios';
import { useHistory } from "react-router-dom";

const UpoadPage = () => {

    const fileInputRef = useRef(null);
    const history = useHistory();

    const toBase64 = file => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
   
    const onUploadClick = async () => {
        const file = fileInputRef.current.files[0];
        const base64 = await toBase64(file);
        const name = file.name;
        await axios.post('/api/csv/upload', { base64File: base64, name });
        history.push('/');
    }

    return <div className="container col-md-6 offset-md-3 mt-5">
        <div className="row">
            <div className="col-md-8">
                <input ref={fileInputRef} type='file' className='form-control' />
            </div>
            <div className="col-md-2">
                <button className='btn btn-primary' onClick={onUploadClick}>Upload</button>
            </div>
          
        </div>
    </div>
   
}
export default UpoadPage;