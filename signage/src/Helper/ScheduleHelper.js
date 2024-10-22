import Service from "../Services/service";

export async function ScheduleListHooks(searchTerm, sortBy, sortDescending, pageNumber, pageSize) {
    var data;
    await Service.getdata("Schedule/GetAll?searchTerm=" + searchTerm + "&sortBy=" + sortBy + "&sortDescending=" + sortDescending + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize).then((res) => {
        data = res.data;
    });
    return data;
}

export async function getLayout() {
    var data;
    await Service.getdata("Layout/GetAll")
        .then(res => {
            data = res.data;
        })
        .catch((err) => {
            console.error(err);
    });
    return data;
}


export async function ScheduleGetByIdHooks(id) {
    var data;
    await Service.getdatabyId("Schedule/GetById?id=",id).then((res) => {
        data = res.data;
    });
    return data;
}

export async function CreateScheduleHooks(formData) {
    var data;
    console.log(formData);
    await Service.createorupdate("Schedule/CreateOrUpdate", formData)
        .then(res => {
            data = res.data;
        })
        .catch(error => {
            console.error("API Error:", error);

        });
    return data;
}

