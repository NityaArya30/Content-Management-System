import React from 'react'
import CreateGroupSchedule from '../../Components/GroupByComponent/GroupScheduleComponent';
import { useNavigate, useParams } from 'react-router-dom';

function GroupSchedule() {
    let navigate = useNavigate();
    let params = useParams();

    return (
        <div>
            <CreateGroupSchedule navigate={navigate} id={params.id}></CreateGroupSchedule>
        </div>
    )
}

export default GroupSchedule;