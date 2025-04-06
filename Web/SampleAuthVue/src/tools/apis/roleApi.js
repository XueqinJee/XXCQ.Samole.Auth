import hook from "../hook";

const role = {
    async getRoleById(id){
        return await hook.get(`/role/get/${id}`)
    },
    async getRoleList(){
        return await hook.get('/role/get')
    },
    async getRoleMenu(){
        return await hook.get('/role/menu')
    },
    async add(data){
        return await hook.post(`/role/add`, data) 
    },
    async edit(data){
        return await hook.post(`/role/edit`, data)
    },
    async delete(id){
        return await hook.delete(`/role/delete/${id}`)
    }
}

export {role}