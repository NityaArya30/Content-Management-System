import service from "../Services/service";
export async function ResolutionListhooks() {
    var data;
    // await service.getdata("Layout/GetAll?searchTerm=" + searchTerm + "&sortBy=" + sortBy + "&sortDescending=" + sortDescending + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize).then((res) => {
    //     data = res.data;
    // });
    await service.getdata("Resolution/GetAll?sortBy=Name&sortDescending=false&pageNumber=1&pageSize=10").then((res) => {
        data = res.data;
    });
    
    return data;
}

export async function ResolutionGetByIdhooks(id) {
    var data;   
    await service.getdatabyId("Resolution/GetById?id=",id).then((res) => {
        data = res.data;
    });
    
    return data;
}