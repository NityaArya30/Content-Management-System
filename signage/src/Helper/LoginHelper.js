import axios from "axios";
import service from "../Services/service";

export async function Loginuser(payLoad) {
  var data;
  await service.createorupdate("Login/authentication", payLoad).then((res) => {
    if (res !== undefined) {
      data = res.data;
    } else {
      data = null;
    }
  });
  return data;
}
