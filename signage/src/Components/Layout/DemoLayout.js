import React, { useState, useEffect, useRef } from 'react';
import './DesignComponent.css'; // Custom styles
import { LayoutGetbyIdhooks } from '../../Helper/LayoutHelper';
import { ResolutionListhooks } from '../../Helper/ResolutionHelper';

function parseLayoutXML(xmlString) {
  const parser = new DOMParser();
  const xmlDoc = parser.parseFromString(xmlString, 'text/xml');

  const layoutElement = xmlDoc.querySelector('layout');
  const layout = {
    width: parseInt(layoutElement.getAttribute('width')),
    height: parseInt(layoutElement.getAttribute('height')),
    duration: parseInt(layoutElement.getAttribute('dur')),
  };

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

    const mediaElements = regionElement.querySelectorAll('media');
    region.media = Array.from(mediaElements).map(mediaElement => ({
      type: mediaElement.getAttribute('type'),
      name: mediaElement.getAttribute('name'),
      duration: parseInt(mediaElement.getAttribute('dur')),
      size: parseInt(mediaElement.getAttribute('size')),
      color: mediaElement.getAttribute('color'),
      behave: mediaElement.getAttribute('behave'),
      text: mediaElement.getAttribute('text') || mediaElement.textContent.trim(), // Ensure we get the text content
      id: mediaElement.getAttribute('id'),
    }));

    return region;
  });

  return { layout, regions };
}

function DemoLayout(props) {
  const [resolution, setResolution] = useState('');
  const [parsedLayout, setParsedLayout] = useState(null);
  const [currentMediaIndices, setCurrentMediaIndices] = useState([]);
  const [mediaTimers, setMediaTimers] = useState([]);
  const [layoutData, setLayoutData] = useState({
    "layoutId": props.id,
    "name": '',
    "xmlDesign": "",
    "createdBy": null,
  });

  const requestRef = useRef();
  const previousTimeRef = useRef();

  useEffect(() => {
    GetResolution();
  }, []);

  async function GetResolution() {
    try {
      const data = await ResolutionListhooks();
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
    const data = await LayoutGetbyIdhooks(1);
    if (data !== undefined) {
      const parsedData = parseLayoutXML(data.xmlDesign);
      const currentResolution = resolutions.find(x => x.resolutionId === data.resolutionId);
      const initialMediaIndices = parsedData.regions.map(() => 0);
      const initialTimers = parsedData.regions.map(region => {
        return region.media.length > 0 ? region.media[0].duration * 1000 : 0;
      });

      setCurrentMediaIndices(initialMediaIndices);
      setMediaTimers(initialTimers);
      setResolution(currentResolution);
      setParsedLayout(parsedData);
      setLayoutData(data);
    }
  }



  useEffect(() => {
      if (parsedLayout) {
          const loopMedia = (timestamp) => {
              if (previousTimeRef.current === undefined) {
                  previousTimeRef.current = timestamp;
              }
              const deltaTime = timestamp - previousTimeRef.current;
              previousTimeRef.current = timestamp;

              setMediaTimers(prevTimers =>
                  prevTimers.map((timer, regionIndex) => {
                      const region = parsedLayout.regions[regionIndex];
                      if (region && region.media.length > 0) {
                          const media = region.media;
                          const currentMediaIndex = currentMediaIndices[regionIndex];
                          const currentMedia = media[currentMediaIndex];

                          if (currentMedia.type === 'Text') {
                              // Handle text media with infinite duration
                              return timer; // Keep timer unchanged for text
                          }

                          const nextTimer = timer - deltaTime;

                          if (nextTimer <= 0) {
                              const nextIndex = (currentMediaIndex + 1) % media.length;
                              setCurrentMediaIndices(prevIndices => {
                                  const updatedIndices = [...prevIndices];
                                  updatedIndices[regionIndex] = nextIndex;
                                  return updatedIndices;
                              });
                              return media[nextIndex].duration * 1000;
                          } else {
                              return nextTimer;
                          }
                      }
                      return timer;
                  })
              );

              requestRef.current = requestAnimationFrame(loopMedia);
          };

          requestRef.current = requestAnimationFrame(loopMedia);

          return () => {
              if (requestRef.current) {
                  cancelAnimationFrame(requestRef.current);
              }
          };
      }
  }, [parsedLayout, currentMediaIndices]);

  const generateRandomString = (length) => {
    const characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
    let randomString = '';
    for (let i = 0; i < length; i++) {
      randomString += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return randomString;
  };

  if (!parsedLayout) {
    return <div>Loading...</div>;
  }

  const { layout, regions } = parsedLayout;

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
        <div className="col-sm-12">
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
              {regions.map((region, regionIndex) => {
                const regionid = generateRandomString(6);

                const regionwid = Math.round(region.width * resolution.scale);
                const regionhei = Math.round(region.height * resolution.scale);
                const regiontop = Math.round(region.top * resolution.scale);
                const regionleft = Math.round(region.left * resolution.scale);

                const currentMediaIndex = currentMediaIndices[regionIndex];
                const currentMedia = region.media[currentMediaIndex];

                return (
                  <div
                    key={regionid}
                    style={{
                      position: 'absolute',
                      width: regionwid,
                      height: regionhei,
                      top: regiontop,
                      left: regionleft,
                    }}
                  >
                    <div className="region" style={{ width: '100%', height: '100%' }}>
                      <div className="media-display">
                        {currentMedia.type === 'Video' && (
                          <p>{currentMedia.name}</p>
                          // <video
                          //     src={currentMedia.src}
                          //     style={{ width: '100%', height: '100%' }}
                          //     autoPlay
                          //     loop
                          // />
                        )}
                        {currentMedia.type === 'Image' && (
                          <img
                            src={`http://localhost:3000/assets/images/${currentMedia.name}`}
                            alt=""
                            style={{ width: '100%', height: '100%' }}
                          />
                        )}
                        {currentMedia.type === 'Text' && (
                          <div
                          className={`scroll-text ${currentMedia.behave}`}
                          style={{
                              color: currentMedia.color,
                              fontSize: `${currentMedia.size}px`,
                              position: 'absolute',
                              top: 0,
                              left: 0,
                              width: '100%',
                              height: '100%',
                              display: 'flex',
                              alignItems: 'center',
                          }}
                          >
                            {currentMedia.text}
                          </div>
                          
                        )}
                      </div>
                    </div>
                  </div>
                );
              })}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default DemoLayout;