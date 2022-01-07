import axios from "axios";
import { setupCache } from "axios-cache-adapter";

const cache = setupCache({
    maxAge: 5 * 60 * 1000,
});

console.log(cache.store);

const instance = axios.create({
    baseURL: "http://localhost:5200/",
    adapter: cache.adapter,
    // baseURL: "http://192.168.0.166:5200",
});

export default instance;
