import React from 'react'
import UserListComponent from '../../Components/UserComponent/UserListComponent'
import { useNavigate, useParams } from 'react-router-dom'


function UserList() {
  let navigate = useNavigate()
  let params=useParams()
return (

  <div>
    <UserListComponent navigate={navigate} id={params}></UserListComponent>
  </div>
)
}

export default UserList;
