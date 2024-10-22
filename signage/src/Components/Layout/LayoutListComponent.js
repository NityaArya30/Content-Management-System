// src/components/LayoutList.js

import React, { useState, useEffect } from 'react';
import { LayoutListhooks } from '../../Helper/LayoutHelper';
import { Link } from 'react-router-dom';

const LayoutListComponent = () => {
    // State to hold the layout data, filters, and pagination
    const [layouts, setLayouts] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [sortBy, setSortBy] = useState('Name');
    const [sortDescending, setSortDescending] = useState(false);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(1);
    const [totalitems, setTotalitems] = useState(0);



    // Fetch data when the component mounts or any dependencies change
    useEffect(() => {
        getData(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
    }, [searchTerm, sortBy, sortDescending, pageNumber, pageSize]);

    async function getData(searchTerm, sortBy, sortDescending, pageNumber, pageSize) {      
        
        const data = await LayoutListhooks(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
        if (data) {
            console.log(data.items);
            setLayouts(data.items);
            setTotalPages(data.totalPages);
            setTotalitems(data.totalItems)
        }
    }



    // Handle the change of sorting options
    const handleSortChange = (event) => {
        const [field, order] = event.target.value.split('_');
        setSortBy(field);
        setSortDescending(order === 'desc');
    };

    // Handle page change
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
                        <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                        <li class="breadcrumb-item active" aria-current="page">Layout List</li>
                    </ol>
                </nav>

                <div class="accordions">
                    <div class="row">

                        <div class="col-lg-12 mb-4">
                            <div class="card card_border">
                                <div class="card-header chart-grid__header">
                                    Layout List
                                </div>
                                <div class="card-body">
                                    <div class="accordion" id="accordionExample">
                                        <div class="card">
                                            <div class="card-header " id="headingOne">
                                                <div className='row'>
                                                    <div className='col-sm-6'>
                                                        <input type="text" className='form-control input-style' placeholder="Search by name..." value={searchTerm} onChange={(e) => setSearchTerm(e.target.value)} />
                                                    </div>
                                                    <div className='col-sm-6'>
                                                        <select className='form-control input-style' onChange={handleSortChange} value={`${sortBy}_${sortDescending ? 'desc' : 'asc'}`}>
                                                            <option value="Name_asc">Name (A-Z)</option>
                                                            <option value="Name_desc">Name (Z-A)</option>
                                                            <option value="CreatedAt_asc">Created At (Oldest)</option>
                                                            <option value="CreatedAt_desc">Created At (Newest)</option>
                                                            <option value="UpdatedAt_asc">Updated At (Oldest)</option>
                                                            <option value="UpdatedAt_desc">Updated At (Newest)</option>
                                                            <option value="CreatedBy_asc">Created By (A-Z)</option>
                                                            <option value="CreatedBy_desc">Created By (Z-A)</option>
                                                        </select>
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                        <table className='table'>
                                            <thead>
                                                <tr>
                                                    <th>Layout ID</th>
                                                    <th>Name</th>
                                                    <th>Resolution</th>
                                                    <th>Actions</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {layouts.map((layout) => (
                                                    <tr key={layout.layoutId}>
                                                        <td>{layout.layoutId}</td>
                                                        <td>{layout.name}</td>
                                                        <td>{layout.resolutionType}</td>
                                                        <td>
                                                            <button className="btn btn-warning"> <i className="fa fa-trash fa-lg" aria-hidden="true"></i></button>
                                                            &nbsp; <Link  to={'/EditLayout/'+layout.layoutId} className="btn btn-danger"> <i className="fa fa-edit fa-lg" aria-hidden="true"></i></Link> 
                                                            {/* &nbsp; <Link  to={`/Design/${layout.layoutId}&${layout.name}`}  className="btn btn-primary"> <i className="fa fa-desktop fa-lg" aria-hidden="true"></i></Link> */}
                                                            &nbsp; <Link  to={`/Design/${layout.layoutId}?name=${layout.name}`}  className="btn btn-primary"> <i className="fa fa-desktop fa-lg" aria-hidden="true"></i></Link> 
                                                            &nbsp; <Link to={'/Demo'} className='bnt btn-primary'>Demo</Link> 
                                                            
                                                        </td>
                                                    </tr>
                                                ))}
                                            </tbody>
                                        </table>

                                        <div className='text-center'>
                                            <button className='btn btn-primary' onClick={() => handlePageChange(pageNumber - 1)} disabled={pageNumber === 1}>
                                                Previous
                                            </button>
                                            <span>Page {pageNumber} of {totalPages}</span>
                                            <button className='btn btn-primary' onClick={() => handlePageChange(pageNumber + 1)} disabled={pageNumber === totalPages}>
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
};

export default LayoutListComponent;
