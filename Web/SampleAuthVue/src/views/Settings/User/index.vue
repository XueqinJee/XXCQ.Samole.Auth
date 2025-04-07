<template>
    <div class="search">
        <div class="item">
            <span>用户名</span>
            <el-input style="width: 200px" v-model="obj.query.q" placeholder="请输入用户名" />
        </div>
        <el-button class="item" @click="onGetData">搜索</el-button>
    </div>
    <div class="tool">
        <el-button @click="onOpenDialog('add')">
            <el-icon>
                <DocumentAdd />
            </el-icon>新增
        </el-button>
    </div>

    <el-table border :data="obj.page.data" v-loading="obj.loading">
        <el-table-column label="编号" prop="id" width="80"/>
        <el-table-column label="用户名" prop="name" />
        <el-table-column label="登录名" prop="loginName" />
        <el-table-column label="角色" prop="roleName" />
        <el-table-column label="邮箱" prop="email" />
        <el-table-column label="手机号" prop="phone" />
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
    loading: false,
    query:{
        pageNumber: 1,
        pageSize: 6,
        q: ''
    },
    page: {
        data: [],
        total: 0,
        totalPage: 0
    }
})

const onDeleteHandler = async (id) => {
    const { code, message } = await api.user.delete(id)
    if (code != 200) {
        return ElMessage.error(message)
    }
    await onGetData()
}

const onGetData = async () => {
    obj.loading = true
    const { code, data } = await api.user.getUserList(obj.query)
    
    obj.page.data = data.data
    obj.page.total = data.total
    obj.page.totalPage = data.totalPage
    setTimeout(() => {
        obj.loading = false
    }, 200);
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