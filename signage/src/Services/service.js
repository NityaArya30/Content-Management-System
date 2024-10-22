import axios from 'axios';


const EMPLOYEE_API_BASE_URL = "https://localhost:7059/api/";

class SignageService {


    async getdata(method) {
        try {
            const res = await axios.get(EMPLOYEE_API_BASE_URL + method).

            then(response => {   
               // console.log(response);            
                return response;
            }).catch(error=>{
                console.log(error);
            });         

            return res;
        } catch (err) {
            console.log(`Error: ${err?.response?.data}`);
        }
    }



    async createorupdate(method,value){
        try{
            const res = await axios.post(EMPLOYEE_API_BASE_URL + method,value).
            then(response => {
                console.log(response);
                return response;               
            }).catch(error=>{
                console.log(error);
            });
            return res;
        } catch (err) {
            console.log(`Error: ${err?.response?.data}`);
        }
    }

    async getdatabyId(method, Id) {
        try {
            const res = await axios.get(EMPLOYEE_API_BASE_URL + method + Id).
                then(response => {
                    console.log(response);
                    return response;
                }).catch(error => {
                    console.log(error);
                });
            return res;
        } catch (err) {
            console.log(`Error: ${err?.response?.data}`);
        }
    }

    async delete(method, Id) {
        try {
            const res = await axios.post(EMPLOYEE_API_BASE_URL + method + Id).
                then(response => {
                    console.log(response);
                }).catch(error => {
                    console.log(error);
                });
            return res;
        } catch (err) {
            console.log(`Error: ${err?.response?.data}`);
        }
    }

}



export default new SignageService()