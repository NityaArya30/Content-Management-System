import Service from "../Services/service";


export async function GroupProps() {
    var data;
    await Service.getdata("Group/GetAll?pageSize=100")
        .then(res => {
            data = res.data;
        })
        .catch((err) => {
            console.error(err);
        });
    return data;
}

export async function ScheduleIdd() {
    var data;
    await Service.getdata("Schedule/GetAll?pageSize=100")
        .then(res => {
            data = res.data;
        })
        .catch((err) => {
            console.error(err);
        });
    return data;
}