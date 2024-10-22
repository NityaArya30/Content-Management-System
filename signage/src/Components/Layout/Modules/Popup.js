import React, { useState } from 'react';
import './Popup.css'


function Popup({ images, onSelect, onClose }) {
    const [searchTerm, setSearchTerm] = useState('');

    const filteredImages = images.filter(image =>
        image.Title.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div className="popup-overlay">
            <div className="popup-content">
                <h4>Media Library</h4>
                <button className="close-btn" onClick={onClose}>X</button>
                <input
                    type="text"
                    placeholder="Search..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    className="search-box"
                />
                <div className="image-list">
                    {filteredImages.map(image => (
                        <div
                            key={image.ContentId}
                            className="image-item"
                            onClick={() => onSelect(image)}
                        >
                            <p>{image.Title}</p>
                            {/* Display image thumbnail if available */}
                            {/* <img src={image.FilePath} alt={image.Title} /> */}
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}

export default Popup;