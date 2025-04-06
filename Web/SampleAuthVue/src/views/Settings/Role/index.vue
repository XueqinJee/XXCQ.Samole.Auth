<template>
    <div class="search">
        <div class="item">
            <span>角色名称</span>
            <el-input style="width: 200px" placeholder="请输入菜单名称！" />
        </div>
        <el-button class="item">搜索</el-button>
    </div>
    <div class="tool">
        <el-button @click="onOpenDialog('add')"><el-icon>
                <DocumentAdd />
            </el-icon>新增</el-button>
    </div>

    <el-table border :data="obj.data" v-loading="obj.loading">
        <el-table-column label="编号" prop="id" />
        <el-table-column label="角色名称" prop="roleName" />
        <el-table-column label="描述" prop="description" />
        <el-table-column label="操作">
            <template #default="scope">
                <el-button type="primary" link size="small" @click="onOpenDialog('edit', scope.row.id)"><el-icon>
                        <Edit />
                    </el-icon>编辑</el-button>
                <el-button type="danger" link size="small" @click="onDeleteHandler(scope.row.id)"><el-icon>
                        <Delete />
                    </el-icon>删除</el-button>
            </template>
        </el-table-column>
    </el-table>
    <add-or-edit :id="dialog.id" :active="dialog.active" :type="dialog.type" @on-closed="onClosedDialog"></add-or-edit>
</template>

<script setup>
import { onMounted, reactive } from 'vue';
import AddOrEdit from './AddOrEdit.vue';
import { api } from '@/tools/api';
import { ElMessage } from 'element-plus';

const dialog = reactive({
    active: false,
    id: 0,
    type: 'add'
})

const obj = reactive({
    data: [],
    loading: false
})

const onDeleteHandler = async (id) => {
    const { code, message } = await api.role.delete(id)
    if(code != 200){
        return ElMessage.error(message)
    }
    await onGetData()
}

const onGetData = async () => {
    obj.loading = true
    const { code, data } = await api.role.getRoleList()
    obj.data = data

    obj.loading = false
}

const onOpenDialog = (type, id = 0) => {
    dialog.active = true
    dialog.id = id
    dialog.type = type
}

const onClosedDialog = async () => {
    dialog.id = 0
    dialog.active = false

    await onGetData()
}

onMounted(() => {
    onGetData()
})

</script>

<style lang="less"></style>