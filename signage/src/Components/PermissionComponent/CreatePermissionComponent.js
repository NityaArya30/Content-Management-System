import React, { useState, useEffect } from "react";
import Select from "react-select";
import { toast } from "react-toastify";
import {
    CreateUserPermission,
    CreatePermissionHooks,
    PermissionGetbyIdhooks,
} from "../../Helper/PermissionHelper";

function CreatePermissionComponent(props) {
    const [users, setUsers] = useState([]);
    const [selectedUser, setSelectedUser] = useState(null);
    const [selectedModule, setSelectedModule] = useState("");
    const [create, setCreate] = useState(false);
    const [view, setView] = useState(false);
    const [edit, setEdit] = useState(false);
    const [deleteModule, setDelete] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    // Manage permission data for form
    const [formData, setFormData] = useState({
        userId: null,
        module: "",
        canCreate: false,
        canView: false,
        canEdit: false,
        canDelete: false,
    });

    // Fetch users for the Select dropdown
    async function fetchUser() {
        const data = await CreateUserPermission();
        if (data !== undefined) {
            setUsers(data.items);
        }
    }

    // Fetch permission data if we are editing an existing one
    async function getData() {
        setIsLoading(true);
        const data = await PermissionGetbyIdhooks(props.id);
        if (data !== undefined) {
            setFormData({
                userId: data.userId,
                module: data.module,
                canCreate: data.canCreate,
                canView: data.canView,
                canEdit: data.canEdit,
                canDelete: data.canDelete,
            });
        }
        setIsLoading(false);
    }

    useEffect(() => {
        fetchUser();
        if (props.id !== undefined) {
            getData();
        }
    }, []);

    const userOptions = users.map((user) => ({
        value: user.userId,
        label: `${user.firstName} ${user.lastName} (${user.roleName})`,
    }));

    const handleFormSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true);

        const permissionData = {
            userId: selectedUser.value,
            module: selectedModule,
            canCreate: create,
            canView: view,
            canEdit: edit,
            canDelete: deleteModule,
        };

        const res = await CreatePermissionHooks(permissionData);

        if (res !== undefined) {
            toast.success(
                props.id === undefined
                    ? "Permission Created Successfully"
                    : "Permission Edited Successfully",
                {
                    position: "top-right",
                    autoClose: 3000,
                }
            );

            if (props.id === undefined) {
                setFormData({
                    userId: null,
                    module: "",
                    canCreate: false,
                    canView: false,
                    canEdit: false,
                    canDelete: false,
                });
                setSelectedUser(null);
                setSelectedModule("");
            }
        } else {
            toast.error("Failed to update Permission. Please try again.", {
                position: "top-right",
                autoClose: 3000,
            });
        }

        setIsLoading(false);
    };

    return (
        <div>
            <div className="container-fluid content-top-gap">
                <nav aria-label="breadcrumb" className="mb-4">
                    <ol className="breadcrumb my-breadcrumb">
                        <li className="breadcrumb-item">
                            <a href="index.html">Home</a>
                        </li>
                        <li className="breadcrumb-item active" aria-current="page">
                            {props.id !== undefined ? "Edit Permission" : "Add Permission"}
                        </li>
                    </ol>
                </nav>

                <div className="accordions">
                    <div className="row">
                        <div className="col-lg-12 mb-4">
                            <div className="card card_border">
                                <div className="card-header chart-grid__header">
                                    {props.id !== undefined
                                        ? "Edit Permission"
                                        : "Add Permission"}
                                </div>
                                <div className="card-body">
                                    <form onSubmit={handleFormSubmit}>
                                        <div className="form-row">
                                            <div className="form-group col-md-12">
                                                <label htmlFor="User" className="input__label">
                                                    Select User
                                                </label>
                                                <Select
                                                    id="User"
                                                    name="user"
                                                    options={userOptions}
                                                    class="form-control input-style"
                                                    classNamePrefix="react-select"
                                                    placeholder="Select User"
                                                    isSearchable
                                                    required
                                                    onChange={(selectedOption) =>
                                                        setSelectedUser(selectedOption)
                                                    } 
                                                />
                                            </div>
                                        </div>

                                        <div className="form-row">
                                            <div className="form-group col-md-12">
                                                <label htmlFor="Status" className="input__label">
                                                    Select Module
                                                </label>
                                                <select
                                                    id="Status"
                                                    name="module"
                                                    className="form-control input-style"
                                                    required
                                                    value={selectedModule}
                                                    onChange={(e) => setSelectedModule(e.target.value)}
                                                >
                                                    <option value="">Select Module</option>
                                                    <option value="User">User</option>
                                                    <option value="Schedule">Schedule</option>
                                                    <option value="Layout">Layout</option>
                                                    <option value="Campaign">Campaign</option>
                                                    <option value="Screen">Screen</option>
                                                    <option value="Library">Library</option>
                                                </select>
                                            </div>
                                        </div>

                                        <div className="form-row">
                                            <div className="form-group col-md-12">
                                                <label htmlFor="Action" className="input__label">
                                                    Actions
                                                </label>
                                                <br />
                                                <div className="form-check form-check-inline">
                                                    <input
                                                        className="form-check-input"
                                                        type="checkbox"
                                                        id="createCheckbox"
                                                        checked={create}
                                                        onChange={() => setCreate(!create)}
                                                    />
                                                    <label
                                                        className="form-check-label"
                                                        htmlFor="createCheckbox"
                                                    >
                                                        Create
                                                    </label>
                                                </div>
                                                <div className="form-check form-check-inline">
                                                    <input
                                                        className="form-check-input"
                                                        type="checkbox"
                                                        id="viewCheckbox"
                                                        checked={view}
                                                        onChange={() => setView(!view)}
                                                    />
                                                    <label
                                                        className="form-check-label"
                                                        htmlFor="viewCheckbox"
                                                    >
                                                        View
                                                    </label>
                                                </div>
                                                <div className="form-check form-check-inline">
                                                    <input
                                                        className="form-check-input"
                                                        type="checkbox"
                                                        id="editCheckbox"
                                                        checked={edit}
                                                        onChange={() => setEdit(!edit)}
                                                    />
                                                    <label
                                                        className="form-check-label"
                                                        htmlFor="editCheckbox"
                                                    >
                                                        Edit
                                                    </label>
                                                </div>
                                                <div className="form-check form-check-inline">
                                                    <input
                                                        className="form-check-input"
                                                        type="checkbox"
                                                        id="deleteCheckbox"
                                                        checked={deleteModule}
                                                        onChange={() => setDelete(!deleteModule)}
                                                    />
                                                    <label
                                                        className="form-check-label"
                                                        htmlFor="deleteCheckbox"
                                                    >
                                                        Delete
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                        <button
                                            type="submit"
                                            className="btn btn-primary btn-style mt-4"
                                            disabled={isLoading}
                                        >
                                            {props.id !== undefined
                                                ? "Edit Permission"
                                                : "Add Permission"}
                                        </button>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default CreatePermissionComponent;
