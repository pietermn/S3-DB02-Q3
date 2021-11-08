import axios from "axios";

const instance = axios.create({
  baseURL: "http://Q3-Backend:5000/",
});

export default instance;
