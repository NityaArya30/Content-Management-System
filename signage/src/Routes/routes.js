import HomeComponent from "../Components/HomeComponent/HomeComponent";
import Login from "../Pages/Main/Login/Login";

import CreateRole from "../Pages/Main/Role/CreateRole";
import RoleList from "../Pages/Main/Role/RoleList";
import UserList from "../Pages/Users/UserList";

import LayoutList from "../Pages/DesignLayout/LayoutList";
import CreateLayout from "../Pages/DesignLayout/CreateLayout";
import CreateUser from "../Pages/Users/CreateUser";
import DesignLayout from "../Pages/DesignLayout/DesignLayout";
import CreateContent from '../Pages/Contents/CreateContent';
import ContentList from '../Pages/Contents/ContentList';
import CreatePermission from "../Pages/Permission/CreatePermission";
import CreateGroupBy from "../Pages/GroupBy/CreateGroupBy";

import CreateSchedule from '../Pages/Schedule/CreateSchedule';
import ScheduleList from '../Pages/Schedule/ScheduleList';
import DemoLayout from '../Components/Layout/DemoLayout';
import GroupCampaign from "../Pages/GroupBy/GroupCampaign";
import GroupScreen from "../Pages/GroupBy/GroupScreen";
import GroupSchedule from "../Pages/GroupBy/GroupSchedule";
import PermissionList from "../Pages/Permission/PermissionList";
const routes = [

    { path: '/', name: 'Home', component: HomeComponent, element: HomeComponent, exact: true },
    { path: '/Login', name: 'Login', component: Login, element: Login, exact: true },
    { path: '/CreateUser', name: 'CreateUser', component: CreateUser, element: CreateUser, exact: true },
    { path: '/CreateRole', name: 'CreateRole', component: CreateRole, element: CreateRole, exact: true },
    { path: '/RoleList', name: 'RoleList', component: RoleList, element: RoleList, exact: true },

    { path: '/UserList', name: 'UserList', component: UserList, element: UserList, exact: true },
    { path: '/EditUser/:id', name: 'CreateUser', component: CreateUser, element: CreateUser },

    { path: '/LayoutList', name: 'LayoutList', component: LayoutList, element: LayoutList, exact: true },
    { path: '/CreateLayout', name: 'CreateLayout', component: CreateLayout, element: CreateLayout, exact: true },
    { path: '/EditLayout/:id', name: 'CreateLayout', component: CreateLayout, element: CreateLayout },
    { path: '/Design/:id', name: 'DesignLayout', component: DesignLayout, element: DesignLayout },
    { path: '/content/CreateContent', name: 'CreateContent', component: CreateContent, element: CreateContent, exact: true },
    { path: '/content/ContentList', name: 'ContentList', component: ContentList, element: ContentList, exact: true },
    { path: '/schedule/CreateSchedule', name: 'CreateSchedule', component: CreateSchedule, element: CreateSchedule, exact: true },
    { path: '/schedule/ScheduleList', name: 'ScheduleList', component: ScheduleList, element: ScheduleList, exact: true },
    { path: '/EditSchedule/:id', name: 'CreateSchedule', component: CreateSchedule, element: CreateSchedule },
    { path: 'permission/CreatePermission', name: 'CreatePermission', component: CreatePermission, element: CreatePermission, exact: true },
    { path: 'permission/PermissionList', name: 'PermissionList', component: PermissionList, element: PermissionList, exact: true },

    { path: '/Demo', name: 'DemoLayout', component: DemoLayout, element: DemoLayout, exact: true },
    { path: '/CreateGroupBy', name: 'CreateGroupBy', component: CreateGroupBy, element: CreateGroupBy, exact: true },
    { path: '/CreateGroupCampaign', name: 'GroupCampaign', component: GroupCampaign, element: GroupCampaign, exact: true },
    { path: '/CreateGroupScreen', name: 'GroupScreen', component: GroupScreen, element: GroupScreen, exact: true },
    { path: '/CreateGroupSchedule', name: 'GroupSchedule', component: GroupSchedule, element: GroupSchedule, exact: true },
]
export default routes;