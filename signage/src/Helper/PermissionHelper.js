import Service from "../Services/service";

export async function PermissionListHooks(searchTerm, sortBy, sortDescending, pageNumber, pageSize) {
    var data;
    await Service.getdata("Permission/GetAll?searchTerm=" + searchTerm + "&sortBy=" + sortBy + "&sortDescending=" + sortDescending + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize).then((res) => {
        data = res.data;
    });
    return data;
}


export async function CreateUserPermission() {
    var data;
    await Service.getdata("User/GetAll?pageSize=100")
        .then(res => {
            data = res.data;
        })
        .catch((err) => {
            console.error(err);
    });
    return data;
}

export async function CreatePermissionHooks(formData) {
    var data;
    console.log(formData);
    await Service.createorupdate("Permission/CreateOrUpdate", formData)
        .then(res => {
            data = res.data;
        })
        .catch(error => {
            // Handle error if API call fails
            console.error("API Error:", error);
        });
    return data;
}

export async function PermissionGetbyIdhooks(id) {
    var data;
    await Service.getdatabyId("Permission/GetById?id=",id).then((res) => {
        data = res.data;
    });
    return data;
}