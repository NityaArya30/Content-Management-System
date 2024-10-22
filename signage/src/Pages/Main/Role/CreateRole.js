import React from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import CreateRoleComponent from '../../../Components/RoleComponent/CreateRoleComponent';

function CreateRole(props) {
    let navigate = useNavigate();
    const params = useParams();

    return (
        <div>
            <CreateRoleComponent {...props} navigate={navigate} id={params.id} />
        </div>
    );

}

export default CreateRole