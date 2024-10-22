import React, { useState, useEffect } from 'react'
import Select from "react-select";
import { GroupProps, ScheduleIdd } from '../../Helper/GroupHelper';

function CreateGroupSchedule(props) {
    const [scheduleid, setScheduleid] = useState([]);
    const [id, setId] = useState([]);
    const [selectedUser, setSelectedUser] = useState(null);
    const [selectedModule, setSelectedModule] = useState("");

    async function fetchUser() {
        const data = await GroupProps();
        if (data !== undefined) {
            setId(data.items);
        }
    }

    async function fetchSchedule() {
        const data = await ScheduleIdd();
        if (data !== undefined) {
            setScheduleid(data.items);
        }
    }

    useEffect(() => {
        fetchUser();
        fetchSchedule();
    }, []);

    const idOptions = id.map(group => ({
        value: group.groupId,
        label: `${group.groupId}`
    }));
    
    const scheduleOptions = scheduleid.map(schedule => ({
        value: schedule.scheduleId,
        label: `${schedule.layoutName}`
    }));

    const handleFormSubmit = (event) => {
        event.preventDefault();
        const formData = {
            selectedUser,
            selectedModule
        };
        console.log(formData);
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
                            {props.id !== undefined ? "Edit Group" : "Add Group Schedule"}
                        </li>
                    </ol>
                </nav>
                <div className="accordions">
                    <div className="row">
                        <div className="col-lg-12 mb-4">
                            <div className="card card_border">
                                <div className="card-header chart-grid__header">
                                    {props.id !== undefined
                                        ? "Edit Group"
                                        : "Add Group Schedule"}
                                </div>
                                <div className="card-body">
                                    <div className="accordion" id="accordionExample">
                                        <div className="card">
                                            <div className="card-header " id="headingOne">
                                                <form onSubmit={handleFormSubmit}>
                                                    <div className="form-row">
                                                        <div className="form-group col-md-12">
                                                            <label htmlFor="User" className="input__label">
                                                                Select GroupId
                                                            </label>
                                                            <Select
                                                                id="User"
                                                                name="user"
                                                                options={idOptions}
                                                                class="form-control input-style"
                                                                classNamePrefix="react-select"
                                                                placeholder="Select Id"
                                                                isSearchable
                                                                required
                                                                onChange={(selectedOption) => setSelectedUser(selectedOption)} // Set selected user
                                                            />
                                                        </div>                                                       
                                                    </div>
                                                    
                                                    <div className="form-row">
                                                        <div className="form-group col-md-12">
                                                            <label htmlFor="Schedule" className="input__label">
                                                                Select ScheduleId
                                                            </label>
                                                            <Select
                                                                id="Schedule"
                                                                name="schedule"
                                                                options={scheduleOptions}
                                                                class="form-control input-style"
                                                                classNamePrefix="react-select"
                                                                placeholder="Select ScheduleId"
                                                                isSearchable
                                                                required
                                                                onChange={(selectedOption) => setSelectedUser(selectedOption)}
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
                                                                name="status"
                                                                className="form-control input-style"
                                                                required
                                                                value={selectedModule}
                                                                onChange={(e) => setSelectedModule(e.target.value)} // Set selected module
                                                            >
                                                                <option value="">Select Module</option>
                                                                <option value="User">User</option>
                                                                <option value="Schedule">Schedule</option>
                                                                <option value="Layout">Layout</option>
                                                                <option value="Campaign">Campaign</option>
                                                                <option value="Screen">Screen</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <button
                                                        type="submit"
                                                        className="btn btn-primary btn-style mt-4"
                                                    >
                                                        Add
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
        </div>
    );
}

export default CreateGroupSchedule;