import hook from "../hook";

const user = {
    async Login(username, password){
        return await hook.post(`/account/login`, {UserName: username, Password: password}) 
    }
}

export {user}