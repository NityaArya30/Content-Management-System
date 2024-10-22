import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { ContentListhooks } from '../../Helper/ContentHelper';

function ContentListComponent() {
    // State to hold the content data, filters, and pagination
    const [contentData, setContent] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [sortBy, setSortBy] = useState('Name');
    const [sortDescending, setSortDescending] = useState(false);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(1);
    const [totalitems, setTotalitems] = useState(0);
debugger;
    // Fetch data when the component mounts or any dependencies change
    useEffect(() => {
        getData(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
    }, [searchTerm, sortBy, sortDescending, pageNumber, pageSize]);

    async function getData(searchTerm, sortBy, sortDescending, pageNumber, pageSize) {      
        
        const data = await ContentListhooks(searchTerm, sortBy, sortDescending, pageNumber, pageSize);
debugger;
if (data) {
    console.log(data.items);
    setContent(data.items);
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
    <div class="container-fluid content-top-gap">
    <h2 className='mb-3'>Content List</h2>
        <input class="form-control" id="myInput" type="text" placeholder="Search.." />
        <div class="table-responsive">
            <div class="table-wrapper">
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th>S No.</th>
                            <th>Content ID</th>
                            <th>Title</th>
                            <th>File Type</th>
                            <th>File Path</th>
                            <th>URL</th>
                            <th>Created By</th>
                            <th>Created On</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            
                        {contentData.map((content) => (
                                                    <tr key={content.contentid}>
                                                        <td>{content.contentid}</td>
                                                        <td>{content.title}</td>
                                                        <td>{content.type}</td>
                                                        <td>{content.filePath}</td>
                                                        <td>{content.url}</td>
                                                        <td>{content.createdBy}</td>
                                                        <td>{content.createdAt}</td>
                                                        <td>
                                                            <button className="btn btn-warning"> <i className="fa fa-trash fa-lg" aria-hidden="true"></i></button>&nbsp; 
                                                            <Link to={'/EditContent/'+content.contentid} className="btn btn-danger"> <i className="fa fa-edit fa-lg" aria-hidden="true"></i></Link>  &nbsp; 
                                                            <Link  to={`/Design/${content.contentid}?name=${content.title}`}  className="btn btn-primary"> 
                                                            <i className="fa fa-desktop fa-lg" aria-hidden="true"></i></Link>  
                                                            
                                                        </td>
                                                    </tr>
                                                ))}
                            </tr> 
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
  )
}

export default ContentListComponent