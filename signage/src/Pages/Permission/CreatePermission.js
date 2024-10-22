import React from 'react'
import CreatePermissionComponent from '../../Components/PermissionComponent/CreatePermissionComponent';
import {useNavigate,useParams} from 'react-router-dom';

function CreatePermission() {
    let navigate=useNavigate();
    let params=useParams();

  return (
    <div>
    <CreatePermissionComponent navigate={navigate} id={params.id}></CreatePermissionComponent>
    </div>
  )
}
export default CreatePermission;