import Content from "./Pages/Layout/Content";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";
import React, { useEffect, useRef } from "react";
import Login from "./Pages/Main/Login/Login";

function App() {
  const myDivRef = useRef(null);
  const userId = localStorage.getItem("userId");
  debugger;
  useEffect(() => {
    // Delay script addition by 10 milliseconds
    const timer = setTimeout(() => {
      const script = document.createElement("script");
      script.src = `${process.env.PUBLIC_URL}/assets/js/scripts.js`; // Replace with your script URL
      script.async = true;
      document.body.appendChild(script);

      // Clean up script when component unmounts
      return () => {
        document.body.removeChild(script);
      };
    }, 1000); // 10ms delay

    // Cleanup timeout if component unmounts before script is added
    return () => clearTimeout(timer);
  }, []); // Empty dependency array means this effect runs once when the component mounts

  return (
    <div ref={myDivRef}>
      <>
        {userId === undefined ||
        !userId ||
        userId === null ||
        userId === "" ||
        userId === 0 ||
        userId === "undefined" ? (
          <Login />
        ) : (
          <Content />
        )}
        <ToastContainer
          position="top-right"
          autoClose={5000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
          bodyClassName="toastBody"
          theme="colored"
        />
      </>
    </div>

    // <LayoutDesign />
  );
}

export default App;
