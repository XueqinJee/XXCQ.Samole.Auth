import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';
import * as ElementPlusIconsVue from '@element-plus/icons-vue'

import router from './router/router'
import 'element-plus/dist/index.css'
import './style.css'
import '@/assets/style.less'
import App from './App.vue'


const app = createApp(App)
const pinia = createPinia()

app.use(router)
pinia.use(piniaPluginPersistedstate)
app.use(ElementPlus)
app.use(pinia)

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component)
}

app.mount('#app')
