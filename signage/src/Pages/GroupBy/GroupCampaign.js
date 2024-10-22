import React from 'react'
import CreateGroupCampaign from '../../Components/GroupByComponent/GroupCampaignComponent';
import { useNavigate, useParams } from 'react-router-dom';

function GroupCampaign() {
    let navigate = useNavigate();
    let params = useParams();

    return (
        <div>
            <CreateGroupCampaign navigate={navigate} id={params.id}></CreateGroupCampaign>
        </div>
    )
}

export default GroupCampaign;