import hook from "./hook";
import { user } from './apis/userApi'
import { menu } from './apis/menuApi'
import {role} from './apis/roleApi'

const api = {
    user,
    menu,
    role
}

export { api, user, menu, role }