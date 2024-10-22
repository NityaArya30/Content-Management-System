import React, { useEffect, useState } from "react";
import { RoleCreateOrUpdate, RoleGetAll } from "../../Helper/RoleHelper";
import { toast } from "react-toastify";
import { RoleGetByIdHooks } from "../../Helper/RoleHelper";
import { useNavigate } from "react-router-dom";

const CreateRoleComponent = (props) => {
  const navigate = useNavigate();
  const [inputField, setInputField] = useState([
    {
      id: props.id,
      roleName: "",
      createdBy: 0,
      updatedBy: 0,
    },
  ]);
  const [dataRole, setDataRole] = useState([]);
  useEffect(() => {
    RoleList();
    if (props.id !== undefined) {
      getData();
    }
  }, []);

  async function getData() {
    const data = await RoleGetByIdHooks(props.id);
    if (data !== undefined) {
      setInputField((prevData) => ({
        ...data,
        ...prevData,
      }));
    }
  }
  const RoleList = async () => {
    const res = await RoleGetAll();
    setDataRole(res);
    console.log(res);
    debugger;
  };
  const handleInputRole = (evt) => {
    const { name, value } = evt.target;
    setInputField((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };
  const saveRole = async (evt) => {
    evt.preventDefault();
    console.log(inputField);
    debugger;
    const res = await RoleCreateOrUpdate(inputField);
    if (res !== undefined) {
      toast.success(
        props.id === undefined
          ? "Role Created Successfully"
          : "Role Edited Successfully",
        {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        }
      );
      //   window.location.reload();
    } else {
      toast.error("Failed to create role. Please try again!", {
        position: "top-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
      });
    }
    setTimeout(() => {
      navigate("/Rolelist");
    }, 4000);
  };
  return (
    <div>
      <div class="container-fluid content-top-gap">
        <nav aria-label="breadcrumb" class="mb-4">
          <ol class="breadcrumb my-breadcrumb">
            <li class="breadcrumb-item">
              <a href="index.html">Home</a>
            </li>
            <li class="breadcrumb-item active" aria-current="page">
              {props.id !== undefined ? "Edit Role" : "Add Role"}
            </li>
          </ol>
        </nav>

        <div class="accordions">
          <div class="row">
            <div class="col-lg-12 mb-4">
              <div class="card card_border">
                <div class="card-header chart-grid__header">
                  {props.id !== undefined ? "Edit Role" : "Add Role"}
                </div>
                <div class="card-body">
                  <div class="accordion" id="accordionExample">
                    <form
                      onSubmit={saveRole}
                      // className="border border-dark rounded p-3"
                    >
                      <div class="form-group">
                        <label for="exampleInputPassword1">Role Name</label>
                        <input
                          type="text"
                          class="form-control"
                          id="exampleInputPassword1"
                          name="roleName"
                          onChange={handleInputRole}
                          placeholder=" please enter Role Name"
                          value={inputField.roleName}
                          required
                        />
                      </div>
                      <button type="submit" class="btn btn-primary">
                        Submit
                      </button>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CreateRoleComponent;
