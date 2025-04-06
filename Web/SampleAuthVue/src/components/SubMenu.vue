<template>
    <template v-for="item in menuData">
        <el-sub-menu :key="item.path" v-if="item && item.children && item.children.length > 0" :index="item.route"
            :class="item.icon ? '' : 'noIcon'">
            <template #title>
                <el-icon> <component :is="item.icon"/></el-icon>
                <span>{{ item.name }}</span>
            </template>
            <sub-menu :menuData="item.children"></sub-menu>
        </el-sub-menu>
        <el-menu-item :key="item.id" v-else :index="item.route" :disabled="item.state == 0" @click="go(item.route)">
            <template #title>
                <el-icon> <component :is="item.icon"/></el-icon>
                <span>{{ item.name }}</span>
            </template>
        </el-menu-item>
    </template>
</template>

<script setup>
import SubMenu from "./SubMenu.vue";
import { computed } from "vue";
import { useRouter } from "vue-router";
const router = useRouter();
const props = defineProps({
    menuData: {
        type: Array,
        default: [],
    },
});

const go = (route) => {
    router.push(route)
}

const curRoute = computed(() => {
    const { path } = router.currentRoute.value;
    return path;
});
</script>