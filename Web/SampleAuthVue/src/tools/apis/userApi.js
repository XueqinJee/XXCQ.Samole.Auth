import hook from "../hook";

const user = {
    async Login(username, password) {
        return await hook.post(`/account/login`, { UserName: username, Password: password })
    },
    async getUserList(query) {
        return await hook.get('/user/page', {params: query})
    },
    async getUserById(id) {
        return await hook.get('/user/get/' + id)
    },
    async add(data) {
        return await hook.post(`/user/add`, data)
    },
    async edit(data) {
        return await hook.post(`/user/edit`, data)
    },
    async delete(id) {
        return await hook.delete(`/user/delete/${id}`)
    }
}

export { user }