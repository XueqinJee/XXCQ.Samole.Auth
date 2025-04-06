<template>
    <el-dialog v-model="dialogVisible" :title="obj.title" width="700px" :before-close="handleClose">
        <el-form v-loading="obj.loading" :inline="true" ref="formRef" :model="form" :rules="rules" label-width="auto">
            <el-form-item label="角色名称" prop="roleName" style="display: flex;">
                <el-input v-model="form.roleName" />
            </el-form-item>
            <el-form-item label="权限" prop="description" style="display: flex;">
                <el-tree 
                    ref="treeRef"
                    :check-strictly="true"
                    show-checkbox
                    :data="obj.treeData"
                    node-key="id"
                    @check-change="onTreeCheckHandler"
                    :props="{label: 'name', id: 'id'}"/>
            </el-form-item>
            <el-form-item label="描述" prop="description" style="display: flex;">
                <el-input v-model="form.description" type="textarea" />
            </el-form-item>
        </el-form>
        <template #footer>
            <div class="dialog-footer">
                <el-button @click="handleClose">取消</el-button>
                <el-button type="primary" @click="handleSubmit">
                    确定
                </el-button>
            </div>
        </template>
    </el-dialog>
</template>

<script setup>
import { ref, reactive, watch, computed, watchEffect } from 'vue'
import { api } from '@/tools/api'
import { ElMessage } from 'element-plus'

const dialogVisible = ref(false)
const emits = defineEmits(['onClosed'])
const props = defineProps({
    active: Boolean,
    id: Number,
    type: String
})

const formRef = ref()
const treeRef = ref()
const obj = reactive({
    title: '新增',
    treeData: [],
    loading: false
})
const form = reactive({
    id: 0,
    roleName: '',
    description: '',
    menuIds: []
})

const rules = reactive({
    roleName: [
        { required: true, message: '角色名称不可为空', trigger: 'blur' }
    ]
})

const handleSubmit = () => {
    formRef.value.validate(async (valid, fields) => {
        if (!valid) {
            return
        }
        let treeIds = treeRef.value.getCheckedKeys()
        form.menuIds = treeIds
        let result = null
        if (props.type == 'add') {
            result = await api.role.add(form)
        } else if (props.type == 'edit') {
            result = await api.role.edit(form)
        }
        const { code, message } = result
        if (code == 200) {
            ElMessage.success('执行成功！')
            return handleClose()
        } else {
            ElMessage.error(message)
        }
    })
}

const handleClose = () => {
    // 表单重置
    formRef.value.resetFields()
    form.id = 0
    emits('onClosed', false)
}

const onTreeCheckHandler = (data, node) => {
    
}


watchEffect(async () => {
    obj.loading = true
    if (!props.active) return;
    obj.title = props.type == 'add' ? '新增' : '编辑'
    if (props.type == 'edit') {
        const { data } = await api.role.getRoleById(props.id)
        var keys = Object.keys(form)
        keys.forEach(x => {
            form[x] = data[x]
        })
        form.id = data.id
        treeRef.value.setCheckedKeys(form.menuIds)
    }
    // 获取菜单
    var { data } = await api.menu.getMenuTree()
    obj.treeData = data
    obj.loading = false
})

watch(() => props.active, (val, old) => {
    dialogVisible.value = val
}, { immediate: true })
</script>

<style lang="less">
.el-form-item {
    margin-bottom: 15px;
}
</style>