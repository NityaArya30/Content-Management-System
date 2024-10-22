import Service from '../Services/service'
// export async function CategoryData() {
//     var data;
//     await service.getdata("Category/GetAllCategory").then((res) => {
//         data = res.data;
//     });
//     return data;
// }

export async function UserListHooks(searchTerm, sortBy, sortDescending, pageNumber, pageSize) {
    var data;
    await Service.getdata("User/GetAll?searchTerm=" + searchTerm + "&sortBy=" + sortBy + "&sortDescending=" + sortDescending + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize).then((res) => {
        data = res.data;
    });
    return data;
}

export async function CreateUserHooks(formData) {
    var data;
    console.log(formData);
    await Service.createorupdate("User/CreateOrUpdate", formData)
        .then(res => {
            //  setApiResponse(res.data); // Set the API response in state  
            data = res.data;
        })
        .catch(error => {
            // Handle error if API call fails
            console.error("API Error:", error);

        });
    return data;
}

export async function CreateUserRole() {
    var data;
    await Service.getdata("Role/GetAll")
        .then(res => {
            data = res.data;
        })
        .catch((err) => {
            console.error(err);
    });
    return data;
}

export async function isEmailRegistered (email) {
    var data;
    await Service.getdata("User/GetAllEmail?email=" +email)
    .then((res) => {
        data=res.data;
        console.log(data);
    })
    .catch((err) => {
        console.log(err);
    }) 
    return data;
}

export async function UserGetbyIdhooks(id) {
    var data;
    await Service.getdatabyId("User/GetById?id=",id).then((res) => {
        data = res.data;
    });
    return data;
}