import React, { useState, useEffect } from 'react';
// import './TextMediaPopup.css'; // Add styles for the text media popup

const TextMediaPopup = ({ onClose, onSave, media  }) => {
    const [textContent, setTextContent] = useState('');
    const [name, setname] = useState('');
    const [textSize, setTextSize] = useState(16); // Default size
    const [textColor, setTextColor] = useState('#000000'); // Default color
    const [mediaId, setMediaId] = useState(null); 
    const [behave, setbehave] = useState('Center');

    useEffect(() => {
        if (media) {
            setTextContent(media.text);
            setTextSize(media.size);
            setTextColor(media.color);
            setMediaId(media.id);
            setname(media.name);
            setbehave(media.behave || 'Center'); // Default to 'Center'
        }
    }, [media]);

    const handleSave = () => {
        onSave(name,textContent, textSize, textColor,mediaId,behave);
    };

    return (
        <div className="popup-overlay">
            <div className="popup-content">
                <h2>{media ? 'Edit Text' : 'Add Text'}</h2>                
                <div className="form-group">
                    <label>Name:</label>
                    <input type="text" class="form-control input-style" value={name} onChange={(e) => setname(e.target.value)} />
                </div>
                <div className="form-group">
                    <label>Text Content:</label>
                    <textarea value={textContent} class="form-control input-style" onChange={(e) => setTextContent(e.target.value)} />
                </div>
                <div className="form-group">
                    <label>Text Size:</label>
                    <input type="number" class="form-control input-style" value={textSize} onChange={(e) => setTextSize(parseInt(e.target.value))} />
                </div>
                <div className="form-group">
                    <label>Behavior:</label>
                    <select className="form-control input-style" value={behave} onChange={(e) => setbehave(e.target.value)}>
                        <option value="Left Scroll">Left Scroll</option>
                        <option value="Right Scroll">Right Scroll</option>
                        <option value="Center">Center</option>
                    </select>
                </div>
                <div className="form-group">
                    <label>Text Color:</label>
                    <input type="color" class="form-control input-style" value={textColor} onChange={(e) => setTextColor(e.target.value)} />
                </div>
                <div className="form-buttons">
                    <button className='btn btn-success' onClick={handleSave}>Save</button> &nbsp;
                    <button className='btn btn-primary' onClick={onClose}>Cancel</button>
                </div>
            </div>
        </div>
    );
};

export default TextMediaPopup;
