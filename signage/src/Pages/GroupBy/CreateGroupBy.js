import React from 'react'
import CreateGroupByComponent from '../../Components/GroupByComponent/CreateGroupByComponent';

import {useNavigate,useParams} from 'react-router-dom';

function CreateGroupBy() {
    let navigate=useNavigate();
    let params=useParams();

  return (
    <div>
    <CreateGroupByComponent navigate={navigate} id={params.id}></CreateGroupByComponent>
    </div>
  )
}

export default CreateGroupBy;