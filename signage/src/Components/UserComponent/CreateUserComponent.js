import React, { useState, useEffect } from "react";
import Service from "../../Services/service";
import { toast } from "react-toastify";
import { CreateUserHooks, CreateUserRole, isEmailRegistered, UserGetbyIdhooks } from "../../Helper/UsersHelper";
import { useNavigate } from "react-router-dom";
import { Audio } from 'react-loader-spinner';

function CreateUserCompnent(props) {
    const navigate = useNavigate();
    const [roles, setRoles] = useState([]);
    const [isEditing, setIsEditing] = useState(false);
    const [isLoading, setIsLoading] = useState(false); // State to manage loading
    const [formData, setFormData] = useState({
        userId: props.id,
        firstName: "",
        lastName: "",
        email: "",
        passwordHash: "",
        username: "",
        roleId: 0,
    });

    async function fetchRole() {
        setIsLoading(true); // Set loading to true
        const data = await CreateUserRole();
        if (data !== undefined) {
            setRoles(data);
        }
        setIsLoading(false); // Set loading to false
    }

    // Edit User
    async function getData() {
        setIsLoading(true); // Set loading to true
        const data = await UserGetbyIdhooks(props.id);
        if (data !== undefined) {
            setFormData((prevData) => ({
                ...prevData,
                ...data,
                roleId: data.roleId || ''
            }));
            setIsEditing(true);
        }
        setIsLoading(false); // Set loading to false
    }

    const userEmail = async (email) => {
        const data = await isEmailRegistered(email);
        if (data !== undefined) {
            if (data.id !== 0) {
                toast.error("Email already registered", {
                    position: "top-right",
                    autoClose: 4000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                });
            }
        }
    }

    useEffect(() => {
        fetchRole();
        if (props.id !== undefined) {
            getData();
        }
    }, []);

    const handleRole = (e) => {
        setFormData({ ...formData, roleId: e.target.value });
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true); // Set loading to true
        var res;
        res = await CreateUserHooks(formData);
        if (res !== undefined) {
            toast.success(
                props.id === undefined ?
                    "User Created Successfully" :
                    "User Edited Successfully"
                , {
                    position: "top-right",
                    autoClose: 3000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                });

            if (props.id === undefined) {
                setFormData({
                    userId: 0,
                    firstName: "",
                    lastName: "",
                    email: "",
                    passwordHash: "",
                    username: "",
                    roleId: 0,
                });
            }
        } else {
            toast.error("Failed to create User. Please try again!", {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
            });
        }
        setTimeout(() => {
            navigate("/UserList");
        }, 4000);
        setIsLoading(false); // Set loading to false
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
        if (name === "email") {
            const emailRegex = /^[^\s@]+@[^\s@]+\.(com|in)$/i;
            if (emailRegex.test(value)) {
                userEmail(value);
            }
        }
    };

    return (
        <div>
            {isLoading ? (
                <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                    <Audio height="100" width="100" color='grey' ariaLabel='loading' />
                </div>
            ) : (
                <div className="container-fluid content-top-gap">
                    <nav aria-label="breadcrumb" className="mb-4">
                        <ol className="breadcrumb my-breadcrumb">
                            <li className="breadcrumb-item">
                                <a href="index.html">Home</a>
                            </li>
                            <li className="breadcrumb-item active" aria-current="page">
                                {props.id !== undefined ? "Edit User" : "Add User"}
                            </li>
                        </ol>
                    </nav>

                    <div className="accordions">
                        <div className="row">
                            <div className="col-lg-12 mb-4">
                                <div className="card card_border">
                                    <div className="card-header chart-grid__header">
                                        {props.id !== undefined ? "Edit User" : "Add User"}
                                    </div>
                                    <div className="card-body">
                                        <div className="accordion" id="accordionExample">
                                            <div className="card">
                                                <div className="card-header " id="headingOne">
                                                    <form onSubmit={handleSubmit}>
                                                        <div className="form-row">
                                                            <div className="form-group col-md-6">
                                                                <label htmlFor="firstName" className="input__label">
                                                                    First Name
                                                                </label>
                                                                <input
                                                                    type="text"
                                                                    className="form-control input-style"
                                                                    id="firstName"
                                                                    name="firstName"
                                                                    placeholder="First Name"
                                                                    onChange={handleChange}
                                                                    value={formData.firstName}
                                                                    required
                                                                />
                                                            </div>
                                                            <div className="form-group col-md-6">
                                                                <label htmlFor="lastName" className="input__label">
                                                                    Last Name
                                                                </label>
                                                                <input
                                                                    type="text"
                                                                    className="form-control input-style"
                                                                    id="lastName"
                                                                    name="lastName"
                                                                    placeholder="Last Name"
                                                                    onChange={handleChange}
                                                                    value={formData.lastName}
                                                                    required
                                                                />
                                                            </div>
                                                        </div>
                                                        <div className="form-row">
                                                            <div className="form-group col-md-6">
                                                                <label htmlFor="inputEmail4" className="input__label">
                                                                    Email
                                                                </label>
                                                                <input
                                                                    type="email"
                                                                    className="form-control input-style"
                                                                    id="inputEmail4"
                                                                    placeholder="Email"
                                                                    onChange={handleChange}
                                                                    name="email"
                                                                    value={formData.email}
                                                                    required
                                                                />
                                                            </div>
                                                            {props.id === undefined && (
                                                                <div className="form-group col-md-6">
                                                                    <label htmlFor="inputPassword4" className="input__label">
                                                                        Password
                                                                    </label>
                                                                    <input
                                                                        type="password"
                                                                        className="form-control input-style"
                                                                        id="inputPassword4"
                                                                        name="passwordHash"
                                                                        placeholder="Password"
                                                                        value={formData.passwordHash}
                                                                        onChange={handleChange}
                                                                        required
                                                                    />
                                                                </div>
                                                            )}
                                                        </div>
                                                        <div className="form-group">
                                                            <label htmlFor="username" className="input__label">
                                                                Username
                                                            </label>
                                                            <input
                                                                type="text"
                                                                className="form-control input-style"
                                                                id="username"
                                                                name="username"
                                                                placeholder="Username"
                                                                onChange={handleChange}
                                                                value={formData.username}
                                                                required
                                                            />
                                                        </div>
                                                        <div className="form-row">
                                                            <div className="form-group col-md-6">
                                                                <label htmlFor="inputRole" className="input__label">
                                                                    Role
                                                                </label>
                                                                <select
                                                                    id="inputRole"
                                                                    className="form-control input-style"
                                                                    value={formData.roleId}
                                                                    onChange={handleRole}
                                                                >
                                                                    <option value="">Choose Your Role</option>
                                                                    {roles.map((role) => (
                                                                        <option key={role.id} value={role.id}>
                                                                            {role.roleName}
                                                                        </option>
                                                                    ))}
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <button type="submit" className="btn btn-primary btn-style mt-4">
                                                            {props.id !== undefined ? "Edit User" : "Add User"}
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
                </div>
            )}
        </div>
    );
}

export default CreateUserCompnent;