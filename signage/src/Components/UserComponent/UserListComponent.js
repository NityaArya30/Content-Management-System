import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { UserListHooks } from '../../Helper/UsersHelper';

function UserListComponent() {
    const [lists, setLists] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [sortBy, setSortBy] = useState('Name');
    const [sortDescending, setSortDescending] = useState(false);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(1);
    const [totalItems, setTotalItems] = useState(0);
    const [loading, setLoading] = useState(false);

    async function getData(searchTerm, sortBy, sortDescending, pageNumber, pageSize) {
        setLoading(true);
        const data = await UserListHooks(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
        if (data) {
            setLists(data.items);
            setTotalPages(data.totalPages);
            setTotalItems(data.totalItems);
        }
        setLoading(false);
    }

    const handleSortChange = (event) => {
        const [field, order] = event.target.value.split('_');
        setSortBy(field);
        setSortDescending(order === 'desc');
    };

    const handlePageChange = (newPage) => {
        if (newPage >= 1 && newPage <= totalPages) {
            setPageNumber(newPage);
        }
    };

    useEffect(() => {
        getData(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
    }, [searchTerm, sortBy, sortDescending, pageNumber, pageSize]);

    return (
        <div>
            <div className="container-fluid content-top-gap">
                <nav aria-label="breadcrumb" className="mb-4">
                    <ol className="breadcrumb my-breadcrumb">
                        <li className="breadcrumb-item"><a href="index.html">Home</a></li>
                        <li className="breadcrumb-item active" aria-current="page">User List</li>
                    </ol>
                </nav>

                <div className="accordions">
                    <div className="row">
                        <div className="col-lg-12 mb-4">
                            <div className="card card_border">
                                <div className="card-header chart-grid__header">
                                    User List
                                </div>
                                <div className="card-body">
                                    <div className="accordion" id="accordionExample">
                                        <div className="card">
                                            <div className="card-header " id="headingOne">
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
                                        {loading ? (
                                            <div className="text-center">
                                                <div className="spinner-border" role="status">
                                                    <span className="sr-only">Loading...</span>
                                                </div>
                                            </div>
                                        ) : (
                                            <table className='table'>
                                                <thead>
                                                    <tr>
                                                        <th>SNo.</th>
                                                        {/* <th>User Id</th> */}
                                                        <th>Email</th>
                                                        <th>First Name</th>
                                                        <th>Last Name</th>
                                                        <th>Username</th>
                                                        <th>Role Name</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {lists.map((list, index) => (
                                                        <tr key={list.userId}>
                                                            <td>{index + 1}</td>
                                                            {/* <td>{list.userId}</td> */}
                                                            <td>{list.email}</td>
                                                            <td>{list.firstName}</td>
                                                            <td>{list.lastName}</td>
                                                            <td>{list.username}</td>
                                                            <td>{list.roleName}</td>
                                                            <td>
                                                                <button type="button" className="btn btn-sm btn-warning">
                                                                    <i className="fa fa-trash fa-lg" aria-hidden="true"></i>
                                                                </button>
                                                                <Link to={'/EditUser/' + list.userId} className="btn btn-danger btn-sm"
                                                                data-toggle="tooltip" title="Edit"
                                                                >
                                                                    <i className="fa fa-edit fa-lg" aria-hidden="true"></i>
                                                                </Link>
                                                            </td>
                                                        </tr>
                                                    ))}
                                                </tbody>
                                            </table>
                                        )}
                                        <div className='text-center'>
                                            <button className='btn btn-sm btn-primary' onClick={() => handlePageChange(pageNumber - 1)} disabled={pageNumber === 1}>
                                                Previous
                                            </button>
                                            &nbsp;
                                            <span>Page {pageNumber} of {totalPages}</span>
                                            &nbsp;
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
}

export default UserListComponent;