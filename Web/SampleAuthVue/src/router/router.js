import { createRouter, createWebHashHistory } from 'vue-router'
import { useUserStore, useMenuStore } from '@/store/store'
import { api } from '@/tools/api'



const router = createRouter({
    history: createWebHashHistory(),
    routes: [
        {
            path: '/login',
            name: 'Login',
            component: () => import('@/views/Login.vue')
        },
        {
            path: '',
            name: 'Main',
            component: () => import('@/layout/Layout.vue'),
            redirect: '/setting/role',
            children: [
                // {
                //     path: '/home',
                //     name: 'Home',
                //     component: () => import('@/views/Home/index.vue')
                // },
                // {
                //     path: '/setting/role',
                //     name: 'Role',
                //     component: () => import('@/views/Settings/Role/index.vue')
                // },
                // {
                //     path: '/setting/menu',
                //     name: 'Menu',
                //     component: () => import('@/views/Settings/Menu/index.vue')
                // },
                // {
                //     path: '/setting/user',
                //     name: 'User',
                //     component: () => import('@/views/Settings/User/index.vue')
                // }
            ]
        }
    ]
})

const dynamicRoutes = [

];

/**
 * 路由首位设置
 */
router.beforeEach(async (to, from, next) => {
    const userStore = useUserStore()
    const menuStore = useMenuStore()
    // 判断是否有菜单
    if (menuStore.data.length == 0 || dynamicRoutes.length == 0) {
       let menus = await menuStore.getMenuList()
       getDynamicRoutes(menus)
       
       dynamicRoutes.forEach(x => {
        router.addRoute('Main', x)
       })
       // 如果更新了路由， 需要重定向一次
       return next(to.fullPath)
    }

    const isLogin = userStore.isLogin()
    if (!isLogin && to.fullPath != '/login') {
        return next('/login')
    }
    next()
})

const getDynamicRoutes = (menus) => {
    const modules = import.meta.glob('@/views/**/*.vue')  // 导入

    for (let i = 0; i < menus.length; i++) {
        const element = menus[i];
        
        if(element.type == 'button') continue;
        if(element.type == 'menu'){
            dynamicRoutes.push(
                {
                    path: element.route,
                    component: modules[`/src/views${element.componentPath}`]
                }
            )
        }
        if(element.children.length != 0){
            getDynamicRoutes(element.children)
        }
    }
}

export default router