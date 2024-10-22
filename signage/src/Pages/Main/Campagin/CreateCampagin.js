import React from "react";
import { useNavigate, useParams } from "react-router-dom";
import CreateCampaginComponent from "../../../Components/CampaginComponent/CreateCampaignComponent";

function CreateCampaign() {
  let navigate = useNavigate();
  let params = useParams();

  return (
    <div>
      <CreateCampaginComponent
        navigate={navigate}
        id={params}
      ></CreateCampaginComponent>
    </div>
  );
}
export default CreateCampaign;
