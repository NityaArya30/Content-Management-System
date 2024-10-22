import service from "../Services/service";
export async function LayoutListhooks(searchTerm, sortBy, sortDescending, pageNumber, pageSize) {
    var data;
    await service.getdata("Layout/GetAll?searchTerm=" + searchTerm + "&sortBy=" + sortBy + "&sortDescending=" + sortDescending + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize).then((res) => {
        data = res.data;
    });
    return data;
}

export async function CreateLayouthooks(formData) {
    var data;
    await service.createorupdate("Layout/CreateOrUpdate", formData)
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

export async function LayoutGetbyIdhooks(id) {
    var data;
    await service.getdatabyId("Layout/GetById?id=",id).then((res) => {
        data = res.data;
    });
    return data;
}