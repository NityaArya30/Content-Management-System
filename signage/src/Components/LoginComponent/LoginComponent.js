import React, { useState } from "react";
import { Loginuser } from "../../Helper/LoginHelper";
import { toast } from "react-toastify";
import css from "./LoginComponent.css";

const LoginComponent = () => {
  const [inputField, setinputField] = useState([
    {
      email: "",
      password: "",
    },
  ]);

  const handleInput = (evt) => {
    const { name, value } = evt.target;
    setinputField((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleLoginData = async (evt) => {
    evt.preventDefault();
    var data = {
      ...inputField,
    };
    const res = await Loginuser(data);
    debugger;
    if (res.user !== "null") {
      localStorage.setItem("userId", res.id);
      localStorage.setItem("name", res.firstname + " " + res.lastname);
      localStorage.setItem("userName", res.userName);
      toast.success(`login${res.status}done`);
      window.location.href = "/";
    } else {
      toast.error(`${res.status}`);
    }
  };

  return (
    // <div
    //   className="container-fluid d-flex justify-content-center "
    //   style={{ minHeight: "100vh", marginTop: "15vh" }}
    // >
    //   <div className="container d-flex justify-content-center">
    //     <div className="row">
    //       {/* {/ <div className="col-sm-12 col-md-10  border border-success"> /} */}
    //       <form onSubmit={handleLoginData}>
    //         <div className="form-group">
    //           <label htmlFor="exampleInputEmail1">Email</label>
    //           <input
    //             type="email"
    //             className="form-control"
    //             id="exampleInputEmail1"
    //             name="email"
    //             aria-describedby="emailHelp"
    //             placeholder="Enter email"
    //             onChange={handleInput}
    //           />
    //           <small id="emailHelp" className="form-text text-muted">
    //             We'll never share your email with anyone else.
    //           </small>
    //         </div>
    //         <div className="form-group">
    //           <label htmlFor="exampleInputPassword1">Password</label>
    //           <input
    //             type="password"
    //             className="form-control"
    //             id="exampleInputPassword1"
    //             name="password"
    //             placeholder="Password"
    //             onChange={handleInput}
    //           />
    //         </div>
    //         <button type="submit" className="btn btn-primary">
    //           Submit
    //         </button>
    //       </form>
    //       {/* {/ </div> /} */}
    //     </div>
    //   </div>
    // </div>
    <div className="" style={{ backgroundColor: "#eff1f9" }}>
      <div className="container-fluid">
        <div className="row main-content1  text-center">
          <div className="col-md-4 text-center company__info">
            <span className="company__logo">
              <h2>
                {/* <span className="fa fa-android"></span> */}
                <img
                  src="https://webmindstechnologies.com/assets/images/logo.png"
                  alt=""
                />
              </h2>
            </span>
            {/* <h4 className="company_title">Your Company Logo</h4> */}
          </div>
          <div className="col-md-8 col-xs-12 col-sm-12 login_form ">
            <div className="container-fluid">
              <div className="row">
                <h2 className="text-center">Log In</h2>
              </div>
              <div className="row">
                <form onSubmit={handleLoginData} className="form-group">
                  <div className="row">
                    <input
                      type="email"
                      name="email"
                      id="username"
                      className="form__input"
                      placeholder="Email"
                      onChange={handleInput}
                    />
                  </div>
                  <div className="row">
                    {/* <!-- <span className="fa fa-lock"></span> --> */}
                    <input
                      type="password"
                      name="password"
                      id="password"
                      className="form__input"
                      placeholder="Password"
                      onChange={handleInput}
                    />
                  </div>
                  <div className="row">
                    <input
                      type="checkbox"
                      name="remember_me"
                      id="remember_me"
                      className=""
                    />
                    <label for="remember_me">Remember Me!</label>
                  </div>
                  <div className="row">
                    <button type="submit" className="btn1 btn-primary btn-lg">
                      Login
                    </button>
                  </div>
                </form>
              </div>
              {/* <div className="row">
                <p>
                  Don't have an account? <a href="#">Register Here</a>
                </p>
              </div> */}
            </div>
          </div>
        </div>
      </div>
      {/* <!-- Footer --> */}
      <div className="container-fluid text-center footer">
        <p>
          {" "}
          Coded with &hearts; by{" "}
          <a href="https://webmindstechnologies.com/">Webminds Technologies.</a>
        </p>
      </div>
    </div>
  );
};

export default LoginComponent;
