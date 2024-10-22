import React from 'react'
import ScheduleListComponent from '../../Components/Schedule Component/ScheduleListComponent'
import { useNavigate, useParams } from 'react-router-dom'

function ScheduleList() {
    let navigate = useNavigate()
    let params=useParams()
  return (
    
    <div>
      <ScheduleListComponent navigate={navigate} id={params.id}></ScheduleListComponent>
    </div>
  )
}

export default ScheduleList
