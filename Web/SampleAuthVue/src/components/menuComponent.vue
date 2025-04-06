<template>
    <el-row class="tac">
        <el-menu default-active="2" :collapse-transition="false" :hide-timeout="0" :collapse="props.isCollapse"
            style="width: 200px;" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose">

            <sub-menu :menu-data="menuRefs.data.value"></sub-menu>
        </el-menu>
    </el-row>
</template>

<script setup>
import SubMenu from './SubMenu.vue'
import { useMenuStore } from '@/store/menuStore'
import { storeToRefs } from 'pinia'
const menuStore = useMenuStore()
const props = defineProps({
    isCollapse: Boolean
})

const menuRefs = storeToRefs(menuStore)

const handleOpen = (key, keyPath) => {
    
}
const handleClose = (key, keyPath) => {
    
}
</script>