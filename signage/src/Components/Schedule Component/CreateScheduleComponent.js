import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Audio } from 'react-loader-spinner';
import { toast } from "react-toastify";
import {
    CreateScheduleHooks,
    getLayout,
    ScheduleGetByIdHooks,
} from "../../Helper/ScheduleHelper";
function CreateScheduleComponent(props) {
    const navigate = useNavigate();
    const [layouts, setLayouts] = useState([]);
    const [isEditing, setIsEditing] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const [formData, setFormData] = useState({
        scheduleId: props.id,
        layoutId: "",
        startTime: "",
        endTime: "",
        recurrence: "",
        priority: "",
    });

    async function fetchLayout() {
        setIsLoading(true);
        const data = await getLayout();
        if (data !== undefined) {

            setLayouts(data.items);
        }
        setIsLoading(false);
    }

    async function getData() {
        setIsLoading(true);
        const data = await ScheduleGetByIdHooks(props.id);
        if (data !== undefined) {
            setFormData((prevData) => ({
                ...prevData,
                ...data,
                layoutId: data.layoutId || '',
            }));
            setIsEditing(true);
        }
        setIsLoading(false);
    }

    useEffect(() => {
        fetchLayout();
        if (props.id !== undefined) {
            getData();
        }
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleLayout = (e) => {
        setFormData({ ...formData, layoutId: e.target.value });
    }

    const convertTimeToISO = (datetime) => {
            const date = new Date(datetime);
            // IST is UTC +5:30
            const ISTOffset = 5.5 * 60 * 60 * 1000; 
            const dateInIST = new Date(date.getTime() + ISTOffset);
            return dateInIST.toISOString().slice(0, 19); // Removing the 'Z' to avoid UTC notation
        };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true); // Set loading to true
        const updatedFormData = {
            ...formData,
            startTime: convertTimeToISO(formData.startTime),
            endTime: convertTimeToISO(formData.endTime)
        };
        var res;
        res = await CreateScheduleHooks(updatedFormData);
        if (res !== undefined) {
            toast.success(
                props.id === undefined ?
                    "Schedule Created Successfully" :
                    "Schedule Edited Successfully"
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
                    scheduleId: 0,
                    layoutId: "",
                    startTime: "",
                    endTime: "",
                    recurrence: "",
                    priority: ""
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
            navigate("/schedule/ScheduleList");
        }, 4000);
        setIsLoading(false); // Set loading to false
    };


    return (
        <div>
            {isLoading ? (
                <div
                    style={{
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "center",
                        height: "100vh",
                    }}
                >
                    <Audio height="100" width="100" color="grey" ariaLabel="loading" />
                </div>
            ) : (
                <div className="container-fluid content-top-gap">
                    <nav aria-label="breadcrumb" className="mb-4">
                        <ol className="breadcrumb my-breadcrumb">
                            <li className="breadcrumb-item">
                                <a href="index.html">Home</a>
                            </li>
                            <li className="breadcrumb-item active" aria-current="page">
                                {props.id !== undefined ? "Edit Schedule" : "Add Schedule"}
                            </li>
                        </ol>
                    </nav>

                    <div className="accordions">
                        <div className="row">
                            <div className="col-lg-12 mb-4">
                                <div className="card card_border">
                                    <div className="card-header chart-grid__header">
                                        {props.id !== undefined ? "Edit Schedule" : "Add Schedule"}
                                    </div>
                                    <div className="card-body">
                                        <div className="accordion" id="accordionExample">
                                            <div className="card">
                                                <div className="card-header " id="headingOne">
                                                    <form onSubmit={handleSubmit}>
                                                        <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <label for="Priority" class="input__label">
                                                                    Priority
                                                                </label>
                                                                <select
                                                                    id="Priority"
                                                                    name="priority"
                                                                    class="form-control input-style"
                                                                    required
                                                                    onChange={handleChange}
                                                                    value={formData.priority}
                                                                >
                                                                    <option>Select Priority</option>
                                                                    <option value="1">Low</option>
                                                                    <option value="2">Medium</option>
                                                                    <option value="3">High</option>
                                                                </select>
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <label for="Layout" class="input__label">
                                                                    Layout
                                                                </label>
                                                                <select
                                                                    id="Layout"
                                                                    name="layout"
                                                                    class="form-control input-style"
                                                                    required
                                                                    onChange={handleLayout}
                                                                    value={formData.layoutId}
                                                                >
                                                                    <option value="">Select a layout..</option>
                                                                    {layouts.map((layout) => {
                                                                        return (
                                                                            <option
                                                                                key={layout.layoutId}
                                                                                value={layout.layoutId}
                                                                            >
                                                                                {layout.name}
                                                                            </option>
                                                                        );
                                                                    })}
                                                                </select>
                                                            </div>
                                                        </div>
                                                        <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <label for="time" class="input__label">
                                                                    Start Time
                                                                </label>
                                                                <input
                                                                    type="datetime-local"
                                                                    class="form-control input-style"
                                                                    id="time"
                                                                    placeholder="Start Time"
                                                                    onChange={handleChange}
                                                                    value={formData.startTime}
                                                                    name="startTime"
                                                                    required
                                                                />
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <label for="Etime" class="input__label">
                                                                    End Time
                                                                </label>
                                                                <input
                                                                    type="datetime-local"
                                                                    class="form-control input-style"
                                                                    id="Etime"
                                                                    placeholder="End Time"
                                                                    onChange={handleChange}
                                                                    value={formData.endTime}
                                                                    name="endTime"
                                                                    required
                                                                />
                                                            </div>
                                                        </div>
                                                        <div className="form-row">
                                                            <div class="form-group col-md-6">
                                                                <label for="recurrence" class="input__label">
                                                                    Reccurence
                                                                </label>
                                                                <select
                                                                    id="recurrence"
                                                                    name="recurrence"
                                                                    class="form-control input-style"
                                                                    required
                                                                    onChange={handleChange}
                                                                    value={formData.recurrence}
                                                                >
                                                                    {/* new */}
                                                                    <option>Select Reccurence</option>
                                                                    <option value="">None</option>
                                                                    <option value="Daily">Daily</option>
                                                                    <option value="Weekly">Weekly</option>
                                                                    <option value="Monthly">Monthly</option>
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <button
                                                            type="submit"
                                                            class="btn btn-primary btn-style mt-4"
                                                        >
                                                            {/* new */}
                                                            {props.id !== undefined ? "Edit Schedule" : "Add Schedule"}
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

export default CreateScheduleComponent;
