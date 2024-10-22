import service from "../Services/service";

export async function RoleCreateOrUpdate(rolePayload) {
  var data;
  await service
    .createorupdate("Role/CreateOrUpdate", rolePayload)
    .then((res) => {
      if (res.data !== undefined) {
        data = res.data;
      } else {
        data = null;
      }
    });

  return data;
}

export async function RoleGetByIdHooks(id) {
  var data;
  await service.getdatabyId("Role/GetById?id=", id).then((res) => {
    data = res.data;
  });
  return data;
}

export async function RoleGetAll() {
  var data;
  await service.getdata("Role/GetAll").then((res) => {
    if (res.data !== undefined) {
      data = res.data;
    } else {
      data = null;
    }
  });
  return data;
}
