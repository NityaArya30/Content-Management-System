import { useState } from "react";

function LeftBar() {

    const [logo,setlogo] = useState(`${process.env.PUBLIC_URL}/assets/images/logo.png`)

    return (
        <div class="sidebar-menu sticky-sidebar-menu">
            <div class="logo">
                <h1><a href="index.html">Collective</a></h1>
            </div>
            <div class="logo">
                <a href="index.html">
                    <img src="image-path" alt="Your logo" title="Your logo" class="img-fluid" style={{ height: "35px" }} />
                </a>
            </div>
            <div class="logo-icon text-center">
                <a href="index.html" title="logo"><img src={logo} alt="logo-icon" /> </a>
            </div>
            <div class="sidebar-menu-inner">
                <ul class="nav nav-pills nav-stacked custom-nav">
                    <li class="active"><a href="index.html"><i class="fa fa-tachometer"></i><span> Dashboard</span></a>
                    </li>

                    <li class="menu-list">
                        <a href="#"><i class="fa fa-cogs"></i>
                            <span>Elements <i class="lnr lnr-chevron-right"></i></span></a>
                        <ul class="sub-menu-list">
                            <li><a href="carousels.html">Carousels</a> </li>
                            <li><a href="cards.html">Default cards</a> </li>
                            <li><a href="people.html">People cards</a></li>
                        </ul>
                    </li>
                    
                    <li class="menu-list">
                        <a href="#"><i class="fa fa-user"></i>
                            <span>User Blocks <i class="lnr lnr-chevron-right"></i></span></a>
                        <ul class="sub-menu-list">
                            <li><a href="/UserList">User List</a> </li>
                            <li><a href="/CreateUser">Add User</a> </li>
                        </ul>
                    </li> 
                    <li class="menu-list">
                        <a href="#"><i class="fa fa-clock-o"></i>
                            <span>Schedule Blocks <i class="lnr lnr-chevron-right"></i></span></a>
                        <ul class="sub-menu-list">
                            <li><a href="/schedule/ScheduleList">Schedule List</a> </li>
                            <li><a href="/schedule/CreateSchedule">Add Schedule</a> </li>
                        </ul>
                    </li> 

                    

                    <li class="menu-list">
                        <a href="#"><i class="fa fa-lock"></i>
                            <span>User Blocks <i class="lnr lnr-chevron-right"></i></span></a>
                        <ul class="sub-menu-list">
                            <li><a href="/permission/CreatePermission">Add Permission</a> </li>
                            <li><a href="/permission/permissionList">Permission List</a> </li>
                        </ul>
                    </li> 

                    <li class="menu-list">
                        <a href="#"><i class="fa fa-users"></i>
                            <span>User Blocks <i class="lnr lnr-chevron-right"></i></span></a>
                        <ul class="sub-menu-list">
                            <li><a href="/CreateGroupBy">Group By</a> </li>
                            <li><a href="/CreateGroupCampaign">Group Campaign</a> </li>
                            <li><a href="/createGroupSchedule">Group Schedule</a> </li>
                            <li><a href="/CreateGroupScreen">Group Screen</a> </li>
                        </ul>
                    </li> 

                    <li><a href="pricing.html"><i class="fa fa-table"></i> <span>Pricing tables</span></a></li>

                    <li class="menu-list">
                        <a href="/Content"><i class="fa fa-th"></i>
                            <span>Content Blocks <i class="lnr lnr-chevron-right"></i></span></a>
                        <ul class="sub-menu-list">
                            <li><a href="/content/CreateContent">Add Content</a> </li>
                            <li><a href="/content/ContentList">Content List</a> </li>
                        </ul>
                    </li> 
                    
                    <li><a href="forms.html"><i class="fa fa-file-text"></i> <span>Forms</span></a></li>

                </ul>
                
                <a class="toggle-btn">
                    <i class="fa fa-angle-double-left menu-collapsed__left"><span>Collapse Sidebar</span></i>
                    <i class="fa fa-angle-double-right menu-collapsed__right"></i>
                </a>
                
            </div>
        </div>
    );
}
export default LeftBar