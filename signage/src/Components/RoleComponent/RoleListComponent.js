import React, { useEffect, useState } from "react";
import { RoleGetAll } from "../../Helper/RoleHelper";
import { Link } from "react-router-dom";
const RoleListComponent = () => {
  const [data, setData] = useState([]);
  useEffect(() => {
    RoleList();
  }, []);

  const RoleList = async () => {
    const res = await RoleGetAll();
    setData(res);
    console.log(res);
    debugger;
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
              Role List
            </li>
          </ol>
        </nav>

        <div class="accordions">
          <div class="row">
            <div class="col-lg-12 mb-4">
              <div class="card card_border">
                <div class="card-header chart-grid__header">Role List</div>
                <div class="card-body">
                  <div class="accordion" id="accordionExample">
                    <table className="table">
                      <thead>
                        <tr>
                          <th> id</th>
                          <th>roleName</th>
                          <th>Action</th>
                        </tr>
                      </thead>
                      <tbody>
                        {data.map((role) => (
                          <tr key={role.id}>
                            <td>{role.id}</td>
                            <td>{role.roleName}</td>

                            <td>
                              <button className="btn btn-warning">
                                {" "}
                                <i
                                  className="fa fa-trash fa-lg"
                                  aria-hidden="true"
                                ></i>
                              </button>
                              &nbsp;{" "}
                              <Link
                                to={"/EditRole/" + role.id}
                                className="btn btn-danger"
                              >
                                {" "}
                                <i
                                  className="fa fa-edit fa-lg"
                                  aria-hidden="true"
                                ></i>
                              </Link>
                              {/* &nbsp; <Link  to={`/Design/${layout.layoutId}&${layout.name}`}  className="btn btn-primary"> <i className="fa fa-desktop fa-lg" aria-hidden="true"></i></Link> */}
                              {/* &nbsp;{" "} */}
                              <Link
                              // to={`/Design/${layout.layoutId}?name=${layout.name}`}
                              // className="btn btn-primary"
                              >
                                {/* {" "}
                                  <i
                                    className="fa fa-desktop fa-lg"
                                    aria-hidden="true"
                                  ></i> */}
                              </Link>
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
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

export default RoleListComponent;
