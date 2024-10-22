import React from 'react'
import PermissionListComponent from '../../Components/PermissionComponent/PermissionListComponent'
import { useNavigate, useParams } from 'react-router-dom'

function PermissionList() {
    let navigate = useNavigate()
    let params=useParams()
  return (
    
    <div>
      <PermissionListComponent navigate={navigate} id={params.id}></PermissionListComponent>
    </div>
  )
}

export default PermissionList;
