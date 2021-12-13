import axios from "axios";

const instance = axios.create({
    baseURL: "http://localhost:5200/",
    // baseURL: "http://192.168.0.166:5200",
});

export default instance;
