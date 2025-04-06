<template>
    <header>
        <div class="left-components" @click="menuTaggleHandler">
            <el-icon class="component">
                <component :is="IconComputed"/>
            </el-icon>
        </div>
        <div class="right-components">
            {{ IconComputed }}
        </div>
    </header>
</template>

<script setup>
import {useMenuStore} from '@/store/menuStore'
import { storeToRefs } from 'pinia';
import { computed } from 'vue';

const menuStore = useMenuStore()
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