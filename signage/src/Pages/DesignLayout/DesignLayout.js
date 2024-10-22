import React from "react";
import { useNavigate, useParams, useLocation } from "react-router-dom";
import DesignComponent from "../../Components/Layout/DesignComponent";

function DesignLayout(props) {
  debugger;
  let navigate = useNavigate();
  const params = useParams();
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const name = queryParams.get("name");

  console.log(params);

  return (
    <div>
      <DesignComponent
        {...props}
        navigate={navigate}
        id={params.id}
        name={name}
      />
    </div>
  );
}

export default DesignLayout;
