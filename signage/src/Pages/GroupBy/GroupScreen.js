import React from 'react'
import CreateGroupScreen from '../../Components/GroupByComponent/GroupScreenComponent';
import { useNavigate, useParams } from 'react-router-dom';

function GroupScreen() {
    let navigate = useNavigate();
    let params = useParams();

    return (
        <div>
            <CreateGroupScreen navigate={navigate} id={params.id}></CreateGroupScreen>
        </div>
    )
}

export default GroupScreen;