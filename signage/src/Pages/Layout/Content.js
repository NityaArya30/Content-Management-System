import React, { Component } from 'react';
import routes from '../../Routes/routes'
import { BrowserRouter as Router, Route, Redirect, Navigate, Routes } from 'react-router-dom';
import LeftBar from './LeftBar';
import TopBar from './TopBar';
import Footer from './Footer';

export class Content extends Component {
    loading = () => <div className="animated fadeIn pt-1 text-center">Loading...</div>
    render() {
        return (
            <>
                <section>
                    <LeftBar />
                    <TopBar />

                    <div id="main-content" className="main-content" style={{ marginTop: "70px" }}>
                        {/* <Routes>
                                <Route path='/' element={<Home />} />
                                <Route path='/Table' element={<Tables />} />
                            </Routes> */}
                        <Routes>
                            {routes.map((route, idx) => {
                                return (
                                    route.component && (
                                        <Route
                                            key={idx}
                                            path={route.path}
                                            exact={route.exact}
                                            name={route.name}
                                            // element
                                            element={<route.component />}
                                        />
                                    )
                                )
                            })}
                        </Routes>
                    </div>

                </section>
                <Footer />
            </>
        )
    }
}

export default Content