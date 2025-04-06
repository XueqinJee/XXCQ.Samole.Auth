import hook from "../hook";

const menu = {
    async getMenyById(id){
        return await hook.get(`/menu/get/${id}`)
    },
    async getMenuTree(){
        return await hook.get('/menu/tree')
    },
    async add(data){
        return await hook.post(`/menu/add`, data) 
    },
    async edit(data){
        return await hook.post(`/menu/edit/${data.id}`, data)
    },
    async delete(id){
        return await hook.delete(`/menu/delete/${id}`)
    }
}

export {menu}