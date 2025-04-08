<template>
    <!-- <div class="search">
        <div class="item">
            <span>菜单名称</span>
            <el-input style="width: 200px" placeholder="请输入菜单名称！" />
        </div>
        <div class="item">
            <span>状态</span>
            <el-input style="width: 200px" placeholder="菜单状态" />
        </div>
        <el-button class="item">搜索</el-button>
    </div> -->
    <div class="tool">
        <el-button @click="onOpenDialogHandler('add')"><el-icon>
                <DocumentAdd />
            </el-icon>新增</el-button>
    </div>
    <el-table :data="obj.data" border row-key="id" :tree-props="{ children: 'children' }">
        <el-table-column prop="name" label="名称" />
        <el-table-column label="类型" width="80">
            <template #default="scope">
                <el-tag v-if="scope.row.type == 'directory'">目录</el-tag>
                <el-tag v-else-if="scope.row.type == 'menu'">菜单</el-tag>
                <el-tag v-else>按钮</el-tag>
            </template>
        </el-table-column>
        <el-table-column prop="sortOrder" label="排序" width="80" />
        <el-table-column prop="route" label="路由" />
        <el-table-column prop="pre" label="权限字段" />
        <el-table-column prop="componentPath" label="组件路径" />
        <el-table-column label="图标" width="80">
            <template #default="scope">
                <div style="width: 100%; height: 100%; display: flex; align-items: center;">
                    <component :is="scope.row.icon" style="width: 18px;" />
                </div>
            </template>
        </el-table-column>
        <el-table-column prop="createTime" label="创建时间" width="200" />
        <el-table-column label="状态" width="80">
            <template #default="scope">
                <el-switch v-model="scope.row.state" :active-value="1" disabled />
            </template>
        </el-table-column>
        <el-table-column label="操作" width="200">
            <template #default="scope">
                <el-button type="primary" link size="small"
                    @click="onOpenDialogHandler('add', scope.row.id, scope.row.id)"><el-icon>
                        <FolderAdd />
                    </el-icon>新增</el-button>
                <el-button type="primary" link size="small"
                    @click="onOpenDialogHandler('edit', scope.row.id, scope.row.parentId)"><el-icon>
                        <Edit />
                    </el-icon>编辑</el-button>
                <el-button type="danger" link size="small" @click="onDeleteHandler(scope.row.id)"><el-icon>
                        <Delete />
                    </el-icon>删除</el-button>
            </template>
        </el-table-column>
    </el-table>
    <!-- 组件 -->
    <add-vue :active="obj.dialog.active" :parent-id="obj.dialog.parentId" :id="obj.dialog.id" :type="obj.dialog.type"
        @on-closed="onClosedDialogHandler"></add-vue>
</template>

<script setup>
import { onMounted, reactive } from 'vue';
import AddVue from './AddOrEdit.vue';
import { api } from '@/tools/api';
import { ElMessage } from 'element-plus';
const obj = reactive({
    addActive: false,
    data: [],
    dialog: {
        active: false,
        id: 0,
        parentId: 0,
        type: 'add'
    }
})

const onOpenDialogHandler = (type, id = 0, parentId = 0) => {
    obj.dialog.type = type
    obj.dialog.parentId = parentId
    obj.dialog.id = id
    obj.dialog.active = true
}

const onClosedDialogHandler = () => {
    obj.dialog.active = false
    obj.dialog.id = 0
    obj.dialog.parentId = 0

    onGetTree()
}

const onGetTree = async () => {
    var result = await api.menu.getMenuTree()
    obj.data = result.data
}

const onDeleteHandler = async (id) => {
    var result = await api.menu.delete(id)
    if (result.code != 200) {
        return ElMessage.error("删除失败，" + result.message)
    }
    await onGetTree()
}

onMounted(async () => {
    await onGetTree()
})
</script>