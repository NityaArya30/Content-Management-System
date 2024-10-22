import React from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import LayoutListComponent from '../../Components/Layout/LayoutListComponent';
function LayoutList(props) {
    let navigate = useNavigate();
    const params = useParams();
    return (
        <div>
            <LayoutListComponent {...props} navigate={navigate} id={params.id}  />
        </div>
    );

}

export default LayoutList