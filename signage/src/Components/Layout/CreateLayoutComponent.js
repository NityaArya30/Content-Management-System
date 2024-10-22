// src/components/CreateLayout.js

import React, { useState, useEffect } from 'react';
import { CreateLayouthooks } from '../../Helper/LayoutHelper';
import { toast } from 'react-toastify';
import { LayoutGetbyIdhooks } from '../../Helper/LayoutHelper';
import { ResolutionListhooks } from '../../Helper/ResolutionHelper';


const CreateLayoutComponent = (props) => {

    // State variables for form data and success/error messages
    const [layoutData, setLayoutData] = useState({
        "layoutId": props.id,
        "name": '',
        "xmlDesign": "",
        "resolutionType":"",
        "createdBy": null,
        "resolutionId":null,
    });
    const [message, setMessage] = useState('');
    const [error, setError] = useState('');
    const [resolutions, setResolutions] = useState([]);

    useEffect(() => {
        GetResolution();
        if(props.id !== undefined){
            getData();
        }
    }, []);

    async function getData() {
        const data = await LayoutGetbyIdhooks(props.id);
        if (data !== undefined) {
            setLayoutData((prevData) => ({
                ...prevData,
                ...data,
                resolutionId: data.resolutionId || '' // Ensure resolutionId is updated
            }));
        }
    }

    async function GetResolution() {
        try {
            const data = await ResolutionListhooks();
            console.log('Resolutions fetched:', data); // Debug: log the data fetched
            if (Array.isArray(data.items)) {
                setResolutions(data.items); // Ensure data is an array
            } else {
                console.error('Resolution data is not an array:', data);
                setResolutions([]); // Set to an empty array if data is not an array
            }
        } catch (err) {
            console.error('Error fetching resolutions:', err);
            setResolutions([]); // Handle error by setting resolutions to an empty array
        }
    }

    // Handle form input changes
    const handleChange = (e) => {
        const { name, value } = e.target;
        setLayoutData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    // Handle resolution change
    const handleResolutionChange = (e) => {
        const { value } = e.target;
        setLayoutData((prevData) => ({
            ...prevData,
            resolutionId: value, // Update resolutionId in layout data
        }));
    };

    // Handle form submission
    const handleSubmit = async (e) => {
        debugger;
        e.preventDefault();
        setMessage('');
        setError('');
        var res = await CreateLayouthooks(layoutData);
        if (res !== undefined) {
            toast.success('Layout created successfully!', {
                position: "top-right",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
            });
            if(props.id === undefined){
                setLayoutData({
                    name: '',
                    resolutionId:null
                });
            }
            
        } else {    
            toast.error('Failed to create layout. Please try again!', {
                position: "top-right",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
            });
        }
    };

    return (
        <div>

            <div class="container-fluid content-top-gap">
                <nav aria-label="breadcrumb" class="mb-4">
                    <ol class="breadcrumb my-breadcrumb">
                        <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                        <li class="breadcrumb-item active" aria-current="page">
                            {
                                props.id !== undefined ? 'Edit Layout' : 'Create Layout'
                            }
                        </li>
                    </ol>
                </nav>

                <div class="accordions">
                    <div class="row">

                        <div class="col-lg-12 mb-4">
                            <div class="card card_border">
                                <div class="card-header chart-grid__header">
                                    {
                                        props.id !== undefined ? 'Edit Layout' : 'Create Layout'
                                    }

                                </div>
                                <div class="card-body">
                                    <div class="accordion" id="accordionExample">
                                        <div class="card">
                                            <div class="card-header " id="headingOne">

                                                {message && <p style={{ color: 'green' }}>{message}</p>}
                                                {error && <p style={{ color: 'red' }}>{error}</p>}
                                                <form onSubmit={handleSubmit}>
                                                    <div className='form-group'>
                                                        <div className='row'>
                                                            <div className='col-sm-2'>
                                                                <label>Name:</label>
                                                            </div>
                                                            <div className='col-sm-10'>
                                                                <input
                                                                    type="text"
                                                                    id="name"
                                                                    className='form-control input-style'
                                                                    name="name"
                                                                    value={layoutData.name}
                                                                    onChange={handleChange}
                                                                    required
                                                                />
                                                            </div>
                                                        </div>


                                                    </div>

                                                    <div className='form-group'>
                                                        <div className='row'>
                                                            <div className='col-sm-2'>
                                                                <label>Resolution:</label>
                                                            </div>
                                                            <div className='col-sm-10'>
                                                                <select
                                                                    className='form-control input-style'
                                                                    name="resolutionId"
                                                                    value={layoutData.resolutionId}
                                                                    onChange={handleResolutionChange}
                                                                    required
                                                                >
                                                                    <option value="">Select Resolution</option>
                                                                    
                                                                    {resolutions.map(res => (
                                                                        <option key={res.id} value={res.resolutionId}>{res.resolutionType}</option>
                                                                    ))}
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <button type="submit" className='btn btn-success'>Save</button>
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
};

export default CreateLayoutComponent;
