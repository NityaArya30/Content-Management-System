import React from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import RoleListComponent from '../../../Components/RoleComponent/RoleListComponent';

function RoleList(props) {
    let navigate = useNavigate();
    const params = useParams();

    return (
        <div>
            <RoleListComponent {...props} navigate={navigate} id={params.id} />
        </div>
    );

}

export default RoleList