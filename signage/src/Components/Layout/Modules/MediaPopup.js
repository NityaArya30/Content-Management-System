// EditPopup.js
import React, { useState } from 'react';

const MediaTextPopup = ({ media, onSave, onClose }) => {
    const [editedMedia, setEditedMedia] = useState(media);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEditedMedia((prev) => ({ ...prev, [name]: value }));
    };

    const handleSave = () => {
        onSave(editedMedia);
    };

    return (
        <div className="popup-overlay">
            <div className="popup-content">
                <h3>Edit Media</h3>
                <div class="form-row">
                            <div class="form-group col-md-6">
                                <label for="Name" class="input__label">Name</label>
                                <input type="text" class="form-control input-style" name="name" value={editedMedia.name} onChange={handleChange} />
                                
                            </div>
                            <div class="form-group col-md-6">
                                <label for="Duration" class="input__label">Duration</label>
                                <input class="form-control input-style" type="number" name="duration" value={editedMedia.duration}
                        onChange={handleChange}
                    />
                  </div>
                
                <button className='btn btn-success' onClick={handleSave}>Save</button> &nbsp; 
                <button className='btn btn-primary' onClick={onClose}>Cancel</button>
            </div>
        </div>
        </div>
    );
};

export default MediaTextPopup;
