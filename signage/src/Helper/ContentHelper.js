import service from "../Services/service";

export async function ContentListhooks(searchTerm, sortBy, sortDescending, pageNumber, pageSize) {
    var data;
    await service.getdata("Content/GetAll?searchTerm=" + searchTerm + "&sortBy=" + sortBy + "&sortDescending=" + sortDescending + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize).then((res) => {
        data = res.data;
    });
    return data;
}

export async function CreateContenthooks(formData) {
    var data;
    await service.createorupdate("Content/CreateOrUpdate", formData)
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

export async function ContentGetbyIdhooks(id) {
    var data;
    await service.getdatabyId("Content/GetById?id=",id).then((res) => {
        data = res.data;
    });
    return data;
}