import React from 'react'
import CreateUserComponent from '../../Components/UserComponent/CreateUserComponent';

import {useNavigate,useParams} from 'react-router-dom';

function CreateUser() {
    let navigate=useNavigate();
    let params=useParams();

  return (
    <div>
    <CreateUserComponent navigate={navigate} id={params.id}></CreateUserComponent>
    </div>
  )
}

export default CreateUser;