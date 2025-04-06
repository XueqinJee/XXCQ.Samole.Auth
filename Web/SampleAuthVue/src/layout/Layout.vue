<template>
    <div class="common-layout">
        <el-container style="height: 100%;">
            <el-aside :width="IsFoldComputed">
                <header class="logo" style="height: 60px">
                    管理系统
                </header>
                <menuComponent :is-collapse="menuRefs.isFold.value"></menuComponent>
            </el-aside>
            <el-container>
                <el-header class="header">
                    <header-vue></header-vue>
                </el-header>
                <el-main class="body">
                    <RouterView></RouterView>
                </el-main>
            </el-container>
        </el-container>
    </div>
</template>

<script setup>
import menuComponent from '@/components/menuComponent.vue';
import HeaderVue from '@/components/Header.vue';

import {RouterLink, RouterView} from 'vue-router'
import {useMenuStore} from '@/store/menuStore'
import { storeToRefs } from 'pinia';
import { computed } from 'vue';
const menuStore = useMenuStore()
const menuRefs = storeToRefs(menuStore)

const IsFoldComputed = computed(() => {
    if(menuRefs.isFold.value){
        return 60 + 'px'
    }
    return menuRefs.width.value + 'px'
})

</script>

<style lang="less">
.common-layout {
    height: 100%;
    
    .logo{
        display: flex;
        align-items: center;
        justify-content: space-around;
        background-color: #545c64;
        color: #fff;
        font-weight: 600;
    }

    .el-header{
        padding: 0px;
    }

    .el-main{
        padding: 10px;
        background-color: #f5f7fa;
    }
}


.header {
}

</style>