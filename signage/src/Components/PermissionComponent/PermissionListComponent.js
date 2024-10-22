import React, { useEffect, useState } from "react";
import Service from "../../Services/service";
import { Link } from "react-router-dom";
import { CreateUserPermission, getPermission, PermissionListHooks } from "../../Helper/PermissionHelper";


function PermissionListComponent() {
    const [permissions, setPermission] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [sortBy, setSortBy] = useState("Name");
    const [sortDescending, setSortDescending] = useState(false);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(1);
    const [totalitems, setTotalitems] = useState(0);
    const [loading, setLoading] = useState(false);
    useEffect(() => {
        getData(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
    }, [searchTerm, sortBy, sortDescending, pageNumber, pageSize]);

    async function getData(
        searchTerm,
        sortBy,
        sortDescending,
        pageNumber,
        pageSize
    ) {
        setLoading(true);
        const data = await PermissionListHooks(
            searchTerm,
            sortBy,
            sortDescending,
            pageNumber,
            pageSize
        );
        if (data) {
            //console.log(data.items);
            setPermission(data.items);
            setTotalPages(data.totalPages);
            setTotalitems(data.totalItems);
            setLoading(false);
        }
    }

    const handleSortChange = (event) => {
        const [field, order] = event.target.value.split("_");
        setSortBy(field);
        setSortDescending(order === "desc");
    };

    const handlePageChange = (newPage) => {
        if (newPage >= 1 && newPage <= totalPages) {
            setPageNumber(newPage);
        }
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
                            Permission List
                        </li>
                    </ol>
                </nav>
                <div class="accordions">
                    <div class="row">
                        <div class="col-lg-12 mb-4">
                            <div class="card card_border">
                                <div class="card-header chart-grid__header">Permission List</div>
                                <div class="card-body">
                                    <div class="accordion" id="accordionExample">
                                        <div class="card">
                                            <div class="card-header " id="headingOne">
                                                <div className="row">
                                                    <div className="col-sm-6">
                                                        <input
                                                            type="text"
                                                            className="form-control input-style"
                                                            placeholder="Search by name..."
                                                            value={searchTerm}
                                                            onChange={(e) => setSearchTerm(e.target.value)}
                                                        />{" "}
                                                    </div>
                                                    <div className="col-sm-6">
                                                        <select
                                                            className="form-control input-style"
                                                            onChange={handleSortChange}
                                                            value={`${sortBy}_${sortDescending ? "desc" : "asc"
                                                                }`}
                                                        >
                                                            <option value="Name_asc">Name (A-Z)</option>
                                                            <option value="Name_desc">Name (Z-A)</option>
                                                            <option value="CreatedAt_asc">
                                                                Created At (Oldest)
                                                            </option>
                                                            <option value="CreatedAt_desc">
                                                                Created At (Newest)
                                                            </option>
                                                            <option value="UpdatedAt_asc">
                                                                Updated At (Oldest)
                                                            </option>
                                                            <option value="UpdatedAt_desc">
                                                                Updated At (Newest)
                                                            </option>
                                                            <option value="CreatedBy_asc">
                                                                Created By (A-Z)
                                                            </option>
                                                            <option value="CreatedBy_desc">
                                                                Created By (Z-A)
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        {loading ? (
                                            <div className="text-center">
                                                <div className="spinner-border" role="status">
                                                    <span className="sr-only">Loading...</span>
                                                </div>
                                            </div>
                                        ) : (
                                            <table className="table">
                                                <thead>
                                                    <tr>
                                                        <th>S No.</th>
                                                        <th>UserId</th>
                                                        <th>Module Id</th>
                                                        <th>ViewModule</th>
                                                        <th>CreateModule</th>
                                                        <th>EditModule</th>
                                                        <th>DeleteModule</th>
                                                        <th>Actions</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {permissions.map((permission, index) => (
                                                        <tr key={permission.permissionId}>
                                                            <td>{index + 1}</td>
                                                            <td>{permission.userId}</td>
                                                            <td>{permission.moduleId}</td>
                                                            <td>{permission.viewModule}</td>
                                                            <td>{permission.createModule}</td>
                                                            <td>{permission.editModule}</td>
                                                            <td>{permission.deleteModule}</td>
                                                            <td>
                                                                <button
                                                                    type="button"
                                                                    className="btn btn-sm btn-warning"
                                                                >
                                                                    <i
                                                                        className="fa fa-trash fa-lg"
                                                                        aria-hidden="true"
                                                                    ></i>
                                                                </button>{" "}
                                                                <Link
                                                                    to={"/EditSchedule/" + permission.permissionId}
                                                                    className="btn btn-sm btn-danger"
                                                                >
                                                                    <i
                                                                        className="fa fa-edit fa-lg"
                                                                        data-toggle="tooltip"
                                                                        title="Edit"
                                                                        aria-hidden="true"
                                                                    ></i>
                                                                </Link>
                                                            </td>
                                                        </tr>
                                                    ))}
                                                </tbody>
                                            </table>
                                        )}
                                        <div className="text-center">
                                            <button
                                                className="btn btn-primary"
                                                onClick={() => handlePageChange(pageNumber - 1)}
                                                disabled={pageNumber === 1}
                                            >
                                                Previous
                                            </button>{" "}
                                            <span>
                                                Page {pageNumber} of {totalPages}{" "}
                                            </span>
                                            <button
                                                className="btn btn-primary"
                                                onClick={() => handlePageChange(pageNumber + 1)}
                                                disabled={pageNumber === totalPages}
                                            >
                                                Next
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default PermissionListComponent;
