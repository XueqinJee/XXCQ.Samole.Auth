import { defineStore } from 'pinia'
import { api } from '@/tools/api'
import { ElMessage } from 'element-plus'
import { useRouter } from 'vue-router'

export const useUserStore = defineStore('_user', {
    persist: true,
    state: () => {
        return {
            token: null,
            refreshToken: null,
            userName: null,
            avator: ''
        }
    },

    actions: {
        async login(userName, password) {
            var result = await api.user.Login(userName, password)
            if (result.code != 200) {
                ElMessage.error(result.message)
                return false
            }
            console.log(result.data);
            this.userName = result.data.userName
            this.token = result.data.token
            this.refreshToken = result.data.refreshToken
            return true
        },
        isLogin() {
            if (this.token) {
                return true
            }
            return false
        },

        logout() {
            this.token = null
            this.refreshToken = null
            this.userName = null
            this.avator = null
        }
    }
})