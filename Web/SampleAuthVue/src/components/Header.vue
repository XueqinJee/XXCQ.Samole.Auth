<template>
    <header>
        <div class="left-components" @click="menuTaggleHandler">
            <el-icon class="component">
                <component :is="IconComputed"/>
            </el-icon>
        </div>
        <div class="right-components">
           <el-button @click="userStore.logout(); router.push('/login')">登出</el-button>
        </div>
    </header>
</template>

<script setup>
import {useMenuStore, useUserStore} from '@/store/store'
import { storeToRefs } from 'pinia';
import { computed } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter()
const menuStore = useMenuStore()
const userStore = useUserStore()
var menuRefs = storeToRefs(menuStore)

const menuTaggleHandler = () => {
    menuRefs.isFold.value = !menuRefs.isFold.value
}

const IconComputed = computed(()=>{
    if(menuRefs.isFold.value){
        return 'Expand'
    }
    return 'Fold'
})
</script>

<style lang="less">
header{
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 0 10px;

    .component{
        font-size: 23px;
        cursor: pointer;
    }
}
</style>