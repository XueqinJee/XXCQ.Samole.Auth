import axios from "axios";

var hook = axios

// 超时时间
hook.defaults.timeout = 1000 * 30
hook.defaults.baseURL = "/api"
/**
 * 请求拦截
 */
hook.interceptors.request.use(config => {
    return config;
}, error => {
    return Promise.reject(error)
})

hook.interceptors.response.use(response => {
    return response.data;
}, error => {
    return Promise.reject(error)
})

export default hook






