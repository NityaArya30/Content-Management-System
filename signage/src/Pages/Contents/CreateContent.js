import React from "react";
import CreateContentComponent from "../../Components/ContentComponent/CreateContentComponent";
import { useNavigate,useParams } from "react-router-dom";
function CreateContent() {
    let navigate=useNavigate();
    let params=useParams();
    return(
        <CreateContentComponent navigate={navigate} id={params}></CreateContentComponent>
    )
}

export default CreateContent;
