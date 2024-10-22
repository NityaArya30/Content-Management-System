import React from 'react'
import CreateScheduleComponent from '../../Components/Schedule Component/CreateScheduleComponent'
import {useNavigate,useParams} from 'react-router-dom';

function CreateSchedule() {
    let navigate=useNavigate();
    let params=useParams();
    
  return (
    <div>
    <CreateScheduleComponent navigate={navigate} id={params.id}></CreateScheduleComponent>
    </div>
  )
}
export default CreateSchedule
