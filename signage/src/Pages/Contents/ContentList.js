import React from 'react'
import ContentListComponent from '../../Components/ContentComponent/ContentListComponent'
import { useNavigate, useParams } from 'react-router-dom'

function ContentList() {
    let navigate = useNavigate()
    let params=useParams()
  return (

    <div>
      <ContentListComponent navigate={navigate} id={params}></ContentListComponent>
    </div>
  )
}

export default ContentList;
