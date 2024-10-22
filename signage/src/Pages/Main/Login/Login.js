import React from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import LoginComponent from '../../../Components/LoginComponent/LoginComponent';

function Login(props) {
    let navigate = useNavigate();
    const params = useParams();

    return (
        <div>
            <LoginComponent {...props} navigate={navigate} id={params.id} />
        </div>
    );

}

export default Login