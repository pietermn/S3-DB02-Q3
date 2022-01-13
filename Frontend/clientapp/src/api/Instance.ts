import axios from "axios";
import { setupCache } from "axios-cache-adapter";

const cache = setupCache({
    maxAge: 15 * 60 * 1000,
});

const instance = axios.create({
    baseURL: "http://localhost:5200/",
    // baseURL: "http://192.168.0.166:5200",
    adapter: cache.adapter,
});

export default instance;
