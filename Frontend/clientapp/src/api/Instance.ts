import axios from "axios";

const instance = axios.create({
    baseURL: "http://localhost:5200/",
    // baseURL: "http://145.93.144.177:5200",
});

export default instance;
