import {defineStore} from 'pinia'
import { api } from '@/tools/api'
import { ElMessage } from 'element-plus'

export const useMenuStore = defineStore('menu', {
    persist: true,
    state: () => {
        return {
            isFold: false,
            width: 200,
            data: []
        }
    },

    actions: {
        async getMenuList(){
            if(this.data.length != 0){
                return this.data
            }
            const { code, data, message} = await api.role.getRoleMenu()
            if(code != 200){
                return ElMessage.error(message)
            }
            this.data =data
        }
    }
})