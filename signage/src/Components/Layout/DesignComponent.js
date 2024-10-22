import React, { useState, useEffect } from 'react';
import Draggable from 'react-draggable';
import { ResizableBox } from 'react-resizable';
import 'react-resizable/css/styles.css';
import './DesignComponent.css'; // Custom styles
import Popup from './Modules/Popup';
import MediaEditPopup from './Modules/MediaPopup';
import TextMediaPopup from './Modules/TextMediaPopup';
import { LayoutGetbyIdhooks } from '../../Helper/LayoutHelper';
import { CreateLayouthooks } from '../../Helper/LayoutHelper';
import { create } from 'xmlbuilder2';
import { toast } from 'react-toastify';
import { ResolutionGetByIdhooks } from '../../Helper/ResolutionHelper';
import { ResolutionListhooks } from '../../Helper/ResolutionHelper';

const data = {
    resolution_type: '1080p( landscape )',
    intent_width: '800',
    intent_height: '450',
    scale: '0.41666666666667',
    main_width: '1920',
    main_height: '1080'
};

function parseLayoutXML(xmlString) {
    const parser = new DOMParser();
    const xmlDoc = parser.parseFromString(xmlString, 'text/xml');

    // Extract the layout element
    const layoutElement = xmlDoc.querySelector('layout');
    const layout = {
        width: parseInt(layoutElement.getAttribute('width')),
        height: parseInt(layoutElement.getAttribute('height')),
        duration: parseInt(layoutElement.getAttribute('dur')),
    };

    // Extract regions
    const regionElements = xmlDoc.querySelectorAll('region');
    const regions = Array.from(regionElements).map(regionElement => {
        const region = {
            class: regionElement.getAttribute('class'),
            width: parseInt(regionElement.getAttribute('width')),
            height: parseInt(regionElement.getAttribute('height')),
            top: parseInt(regionElement.getAttribute('top')),
            left: parseInt(regionElement.getAttribute('left')),
            color: regionElement.getAttribute('color'),
            repeat: regionElement.getAttribute('repeat') === 'true',
            media: [],
        };

        // Extract media within each region
        const mediaElements = regionElement.querySelectorAll('media');
        region.media = Array.from(mediaElements).map(mediaElement => ({
            type: mediaElement.getAttribute('type'),
            name: mediaElement.getAttribute('name'),
            duration: parseInt(mediaElement.getAttribute('dur')),
            size: parseInt(mediaElement.getAttribute('size')),
            color: mediaElement.getAttribute('color'),
            behave: mediaElement.getAttribute('behave'),
            text: mediaElement.getAttribute('text'),
            id: mediaElement.getAttribute('id'),
        }));

        return region;
    });

    return { layout, regions };
}

var Images = [
    {
        "ContentId": 1,
        "Title": "Image 1",
        "Type": "Image",
        "FilePath": ""
    },
    {
        "ContentId": 2,
        "Title": "Image 2",
        "Type": "Image",
        "FilePath": ""
    },
    {
        "ContentId": 3,
        "Title": "Image 3",
        "Type": "Image",
        "FilePath": ""
    },
    {
        "ContentId": 4,
        "Title": "Image 4",
        "Type": "Image",
        "FilePath": ""
    },
]

var Videos = [
    {
        "ContentId": 1,
        "Title": "Video 1",
        "Type": "Video",
        "FilePath": ""
    },
    {
        "ContentId": 2,
        "Title": "Video 2",
        "Type": "Video",
        "FilePath": ""
    },
    {
        "ContentId": 3,
        "Title": "Video 3",
        "Type": "Video",
        "FilePath": ""
    },
    {
        "ContentId": 4,
        "Title": "Video 4",
        "Type": "Video",
        "FilePath": ""
    },
]

function DesignComponent(props) {
    const [resolution, setResolution] = useState('');
    const [parsedLayout, setParsedLayout] = useState(null);
    const [CurrentRegionMedia, SetCurrentRegionMedia] = useState([]);
    const [mediaimages, Setmediaimages] = useState(Images);
    const [mediavideos, Setmediavideos] = useState(Videos);
    const [currentpopupMedia, SetcurrentpopupMedia] = useState(null);
    const [isPopupVisible, setIsPopupVisible] = useState(false);
    const [isEditPopupVisible, setIsEditPopupVisible] = useState(false);
    const [RegionIndex, SetRegionIndex] = useState(null);
    const [editingMedia, setEditingMedia] = useState(null);
    const [isTextPopupVisible, setIsTextPopupVisible] = useState(false); // New state for text popup
    const [textMedia, setTextMedia] = useState(null); // New state for text media  

    const [layoutData, setLayoutData] = useState({
        "layoutId": props.id,
        "name": '',
        "xmlDesign": "",
        "createdBy": null,
    });

    useEffect(() => {
        GetResolution();
    }, []);

    async function GetResolution() {
        try {

            const data = await ResolutionListhooks();
            console.log('Resolutions fetched:', data); // Debug: log the data fetched
            if (Array.isArray(data.items)) {
                getData(data.items);
            } else {
                console.error('Resolution data is not an array:', data);

            }
        } catch (err) {
            console.error('Error fetching resolutions:', err);

        }
    }

    async function getData(resolutions) {

        const data = await LayoutGetbyIdhooks(props.id);
        if (data !== undefined) {
            const parsedData = parseLayoutXML(data.xmlDesign);
            var currentresolution = resolutions.filter(x => x.resolutionId === data.resolutionId);
            debugger;
            setResolution(currentresolution[0]);
            // const Resolution = await ResolutionGetByIdhooks(data.resolutionId);
            // if(Resolution !== undefined){
            //     console.log(resolution);
            //   //  setResolution(resolution);
            // }
            setParsedLayout(parsedData);
            setLayoutData(data);
        }
    }


    if (!parsedLayout) {
        return <div>Loading...</div>;
    }

    const { layout, regions } = parsedLayout;

    const addRegion = () => {
        const newRegion = {
            class: `mediaNew${regions.length}`,
            width: 350,
            height: 350,
            top: 100,
            left: 100,
            color: '#cccccc',
            repeat: false,
            media: [],
        };

        setParsedLayout((prevState) => ({
            ...prevState,
            regions: [...prevState.regions, newRegion],
        }));
    };

    const deleteRegion = (index) => {
        if (window.confirm("Are you sure you want to delete this region?")) {
            const updatedRegions = [...regions];
            updatedRegions.splice(index, 1);
            setParsedLayout((prevState) => ({
                ...prevState,
                regions: updatedRegions,
            }));
        }
    };

    const handleTimelineClick = (currentRegionIndex) => {
        console.log(currentRegionIndex)
        if (currentRegionIndex !== null) {
            const selectedRegion = regions[currentRegionIndex];
            debugger;
            SetCurrentRegionMedia(selectedRegion.media);
            SetRegionIndex(currentRegionIndex);
            console.log(selectedRegion.media);
            toast.success('Region Timeline Selected!', {
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

    const handleImageButtonClick = (type) => {
        debugger;
        if (RegionIndex !== null) {
            if (type === 'Image') {
                SetcurrentpopupMedia(mediaimages)
            } else if (type === 'Video') {
                SetcurrentpopupMedia(mediavideos)
            }

            setIsPopupVisible(true);
        } else {
            toast.error('Please Select Region Timeline!', {
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

    const handlePopupClose = () => {
        setIsPopupVisible(false);
    };

    const handleImageSelect = (image) => {
        if (RegionIndex !== null) {
            const updatedRegions = [...regions];
            updatedRegions[RegionIndex].media.push({
                type: image.Type,
                name: image.Title,
                duration: 10, // Example duration
                id: image.ContentId.toString()
            });
            setParsedLayout((prevState) => ({
                ...prevState,
                regions: updatedRegions,
            }));
            SetCurrentRegionMedia(updatedRegions[RegionIndex].media);
        }
        setIsPopupVisible(false);
    };

    const handleMediaEdit = (mediaIndex) => {
        const mediaToEdit = CurrentRegionMedia[mediaIndex];
        setEditingMedia(mediaToEdit);
        setIsEditPopupVisible(true);
    };

    const handleTextMediaEdit = (mediaIndex) => {
        debugger;
        const media = CurrentRegionMedia[mediaIndex];
        if (media.type === 'Text') {
            setTextMedia({ ...media, index: mediaIndex });
            setIsTextPopupVisible(true);
        }
    };

    const handleEditSave = (updatedMedia) => {
        if (RegionIndex !== null) {
            const updatedRegions = [...regions];
            updatedRegions[RegionIndex].media = updatedRegions[RegionIndex].media.map(media =>
                media.id === updatedMedia.id ? updatedMedia : media
            );
            setParsedLayout((prevState) => ({
                ...prevState,
                regions: updatedRegions,
            }));
            SetCurrentRegionMedia(updatedRegions[RegionIndex].media);
            setIsEditPopupVisible(false);
        }
    };

    const handleEditPopupClose = () => {
        setIsEditPopupVisible(false);
    };

    const handleMediaDelete = (mediaIndex) => {
        if (window.confirm('Are you sure you want to delete this media item?')) {
            const updatedRegions = [...regions];
            updatedRegions[RegionIndex].media.splice(mediaIndex, 1);
            setParsedLayout((prevState) => ({
                ...prevState,
                regions: updatedRegions,
            }));
            SetCurrentRegionMedia(updatedRegions[RegionIndex].media);
        }
    };

    const handleTextButtonClick = () => {
        if (RegionIndex !== null) {
            setIsTextPopupVisible(true);
        } else {
            toast.error('Please Select Region Timeline!', {
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

    const handleTextSave = (name, textContent, size, color, mediaId, behave) => {
        if (mediaId) {
            // Editing an existing text media
            const updatedMedia = [...CurrentRegionMedia];
            const updatedRegions = [...regions];
            const mediaIndex = updatedRegions[RegionIndex].media.findIndex((media) => media.id === mediaId);
            if (mediaIndex !== -1) {
                updatedRegions[RegionIndex].media[mediaIndex] = {
                    ...updatedRegions[RegionIndex].media[mediaIndex],
                    text: textContent,
                    size,
                    color,
                    name,
                    behave: behave,
                };

                setParsedLayout((prevState) => ({
                    ...prevState,
                    regions: updatedRegions,
                }));

                updatedMedia[mediaIndex] = {
                    ...updatedMedia[mediaIndex],
                    text: textContent,
                    size,
                    color,
                    name,
                    behave: behave,
                };
                SetCurrentRegionMedia(updatedMedia);
            }
        } else {
            // Adding new text media
            const newTextMedia = {
                type: 'Text',
                name,
                duration: 10,
                size,
                color,
                behave: 'Center',
                text: textContent,
                id: `text${Date.now()}`,
            };

            SetCurrentRegionMedia((prevState) => [...prevState, newTextMedia]);
            regions[RegionIndex].media = [...CurrentRegionMedia, newTextMedia];
        }
        setIsTextPopupVisible(false);
    };

    const saveLayout = async (e) => {
        const xmlDoc = create({ version: '1.0' })
            .ele('XML', { version: '0.0.1' })
            .ele('layout', { width: parsedLayout.layout.width, height: parsedLayout.layout.height, dur: parsedLayout.layout.duration });

        parsedLayout.regions.forEach(region => {
            const regionElement = xmlDoc.ele('region', {
                class: region.class,
                width: region.width,
                height: region.height,
                top: region.top,
                left: region.left,
                color: region.color,
                repeat: region.repeat
            });

            region.media.forEach(media => {
                regionElement.ele('media', {
                    type: media.type,
                    name: media.name,
                    dur: media.duration,
                    id: media.id,
                    size: media.size,
                    color: media.color,
                    behave: media.behave,
                    text: media.text
                }).txt(media.name);
            });
        });

        const xml = xmlDoc.end({ prettyPrint: true });
        layoutData.xmlDesign = xml;
        var res = await CreateLayouthooks(layoutData);
        if (res !== undefined) {
            toast.success('Design Saved successfully!', {
                position: "top-right",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
            });

        } else {
            toast.error('Design Saved Failed!', {
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

    const generateRandomString = (length) => {
        const characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
        let randomString = '';
        for (let i = 0; i < length; i++) {
            randomString += characters.charAt(Math.floor(Math.random() * characters.length));
        }
        return randomString;
    };




    return (
        <div className="container-fluid content-top-gap" style={{ minHeight: '600px' }}>

            <div className='container mb-2'>

                <div className="text-center">
                    <h4>{props.name !== null ? ' Layout Name - ' + props.name : ''}</h4>
                    <p><b>Layout Details - </b>Aspect Ratio: 16:9 | Background Color: Black | Resolution Type:
                        {resolution.resolutionType}
                    </p>
                </div>
            </div>
            <div className="row">
                <div className="col-sm-8">
                    <div className="container">
                        <div
                            id="layout"
                            layout_scale={resolution?.scale}
                            main_width={resolution.mainWidth}
                            main_height={resolution.mainHeight}
                            style={{
                                marginLeft: '1%',
                                position: 'relative',
                                width: resolution?.intentWidth + 'px',
                                height: resolution?.intentHeight + 'px',
                                backgroundColor: '#000000',
                            }}
                        >
                            {regions.map((region, index) => {
                                const regionid = generateRandomString(6);

                                // Calculate scaled values
                                const regionwid = Math.round(region.width * resolution.scale);
                                const regionhei = Math.round(region.height * resolution.scale);
                                const regiontop = Math.round(region.top * resolution.scale);
                                const regionleft = Math.round(region.left * resolution.scale);

                                return (
                                    <>
                                        <Draggable
                                            key={regionid}
                                            defaultPosition={{ x: regionleft, y: regiontop }}
                                            onStop={(e, data) => {
                                                const updatedRegions = [...regions];
                                                updatedRegions[index].left = Math.round(data.x / resolution.scale);
                                                updatedRegions[index].top = Math.round(data.y / resolution.scale);
                                                setParsedLayout((prevState) => ({
                                                    ...prevState,
                                                    regions: updatedRegions,
                                                }));
                                            }}
                                            handle=".resize-handle"
                                            cancel=".layoutbtnline"
                                        >
                                            <div
                                                style={{
                                                    position: 'absolute',
                                                    width: regionwid,
                                                    height: regionhei,
                                                }}
                                            >
                                                <ResizableBox
                                                    width={regionwid}
                                                    height={regionhei}
                                                    minConstraints={[50, 50]}
                                                    maxConstraints={[resolution.intentWidth, resolution.intentHeight]}
                                                    className="resize-handle"
                                                    handle={<span className="resize-handle"></span>}
                                                    onResizeStop={(e, data) => {
                                                        const updatedRegions = [...regions];
                                                        updatedRegions[index].width = Math.round(data.size.width / resolution.scale);
                                                        updatedRegions[index].height = Math.round(data.size.height / resolution.scale);
                                                        setParsedLayout((prevState) => ({
                                                            ...prevState,
                                                            regions: updatedRegions,
                                                        }));
                                                    }}
                                                    style={{
                                                        position: 'absolute',
                                                        backgroundColor: 'rgba(0, 0, 0, 0.2)', // Adjust if needed
                                                    }}
                                                >
                                                    <div className="region ui-widget-content" style={{ width: '100%', height: '100%' }}>
                                                        <div className="layoutbtnline">
                                                            <button
                                                                className="laybtn module"
                                                                onClick={(e) => {
                                                                    e.preventDefault(); // Prevent default action
                                                                    e.stopPropagation(); // Stop event propagation
                                                                    handleTimelineClick(index);
                                                                }}
                                                            >
                                                                <i className="fa fa-window-restore fa-lg" aria-hidden="true"></i>
                                                            </button>
                                                            <button
                                                                className="laybtn option"
                                                                onClick={() => console.log('Options')}
                                                            >
                                                                <i className="fa fa-bars fa-lg" aria-hidden="true"></i>
                                                            </button>
                                                            <button
                                                                className="laybtn close"
                                                                style={{ border: '2px solid black', padding: '5px' }}
                                                                onClick={(e) => {
                                                                    e.preventDefault(); // Prevent default action
                                                                    e.stopPropagation(); // Stop event propagation
                                                                    deleteRegion(index);
                                                                }}
                                                            >
                                                                <i className="fa fa-trash fa-lg" aria-hidden="true"></i>
                                                            </button>

                                                        </div>

                                                        <p className="res mt-5 p-2">
                                                            {region.width}x{region.height}
                                                        </p>
                                                        {/* <h5 className='ml-4'>Current Media</h5> */}
                                                        {region.media.map((media, mediaIndex) => (
                                                            <div key={mediaIndex} className="media ml-4 ">
                                                                <p className='medianame'>{media.type} - {media.name}</p>
                                                                {/* <img
                                                                    src={`path/to/media/${media.name}`}
                                                                    alt={media.name}
                                                                    style={{ width: '100%', height: 'auto' }}
                                                                /> */}
                                                            </div>
                                                        ))}
                                                    </div>

                                                </ResizableBox>
                                            </div>
                                        </Draggable>
                                    </>

                                );
                            })}
                        </div>
                    </div>
                    <div className='container-fluid content-top-gap'>
                        <div className='text-center'>
                            <button className="btn btn-primary" onClick={addRegion}>
                                Add Region
                            </button> &nbsp;
                            <button
                                className="btn btn-primary"
                                id="savepos"
                                onClick={saveLayout}
                            >
                                Save Layout
                            </button>
                            &nbsp;
                        </div>
                    </div>
                </div>
                <div className="col-md-4">
                    <div className="row">
                        <div className="col-md-12 col-sm-6 mb-4">
                            <div className='top-ads'>
                                <h4 className="text-center">Modules</h4>
                                <button className='btn btn-primary ml-2 mt-2' onClick={() => { handleImageButtonClick('Image') }}>Image</button>
                                <button className='btn btn-primary ml-2 mt-2' onClick={() => { handleImageButtonClick('Video') }}>Video</button>
                                <button className='btn btn-primary ml-2 mt-2' onClick={handleTextButtonClick}>Text</button>
                                <button className='btn btn-primary ml-2 mt-2'>Web Page</button>
                                <button className='btn btn-primary ml-2 mt-2'>Clock</button>
                                <button className='btn btn-primary ml-2 mt-2'>YouTube</button>
                                <button className='btn btn-primary ml-2 mt-2'>Pdf</button>
                                <button className='btn btn-primary ml-2 mt-2'>Weather</button>
                                <button className='btn btn-primary ml-2 mt-2'>RSS Feed</button>
                            </div>
                            <div className="top-ads">
                                {RegionIndex !== null ? <h4 className="text-center"> TimeLine</h4> : ''}

                                {/* <div class="card card_border mt-1">
                                    <div class="card-body">
                                        <div className='row'>
                                            <div className='col-sm-8'>Type &nbsp; Name &nbsp;&nbsp; Duration </div>
                                            <div className='col-sm-4'>Actions</div>
                                        </div>
                                    </div>
                                </div> */}
                                {CurrentRegionMedia.map((media, mediaIndex) => (

                                    <div key={mediaIndex} class="card card_border mt-1">
                                        <div class="card-body">
                                            <div className='row'>
                                                <div className='col-sm-8'>
                                                    <p>{media.type} - {media.name} - {media.duration} Seconds</p>
                                                </div>
                                                <div className='col-sm-4'>
                                                    {media.type === 'Image' || media.type === 'Video' ?
                                                        <button
                                                            className="btn btn-warning mr-2"
                                                            onClick={() => handleMediaEdit(mediaIndex)}
                                                        >
                                                            <i className="fa fa-edit fa-lg" aria-hidden="true"></i>
                                                        </button>
                                                        :
                                                        <button
                                                            className="btn btn-warning mr-2"
                                                            onClick={() => handleTextMediaEdit(mediaIndex)}
                                                        >
                                                            <i className="fa fa-edit fa-lg" aria-hidden="true"></i>
                                                        </button>
                                                    }

                                                    <button
                                                        className="btn btn-danger"
                                                        onClick={() => handleMediaDelete(mediaIndex)}
                                                    >
                                                        <i className="fa fa-trash fa-lg" aria-hidden="true"></i>
                                                    </button>
                                                </div>
                                            </div>


                                        </div>

                                    </div>
                                ))}

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            {isPopupVisible && (
                <Popup
                    images={currentpopupMedia}
                    onSelect={handleImageSelect}
                    onClose={handlePopupClose}
                />
            )}{isEditPopupVisible && (
                <MediaEditPopup
                    media={editingMedia}
                    onSave={handleEditSave}
                    onClose={handleEditPopupClose}
                />
            )}
            {isTextPopupVisible && (
                <TextMediaPopup
                    onClose={() => setIsTextPopupVisible(false)}
                    onSave={handleTextSave}
                    media={textMedia} // Pass the text media being edited
                />
            )}
        </div>
    );
}

export default DesignComponent;
