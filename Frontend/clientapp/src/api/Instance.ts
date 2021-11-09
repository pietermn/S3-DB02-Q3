import axios from "axios";

const instance = axios.create({
    baseURL: "http://localhost:5200/",
});

export default instance;
