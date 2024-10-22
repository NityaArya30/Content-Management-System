import React from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import CreateLayoutComponent from '../../Components/Layout/CreateLayoutComponent';

function CreateLayout(props) {
    let navigate = useNavigate();
    const params = useParams();

    return (
        <div>
            <CreateLayoutComponent {...props} navigate={navigate} id={params.id} name={params.name} />
        </div>
    );

}

export default CreateLayout