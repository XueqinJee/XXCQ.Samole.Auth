<template>
    <el-dialog v-model="dialogVisible" :title="obj.title" width="700px" :before-close="handleClose">
        <el-form v-loading="obj.loading" :inline="true" ref="formRef" :model="form" :rules="rules" label-width="auto">
            <el-form-item label="用户名" prop="name" style="display: flex;">
                <el-input v-model="form.name" />
            </el-form-item>
            <el-form-item label="登录名" prop="loginName" style="display: flex;">
                <el-input v-model="form.loginName" />
            </el-form-item>
            <el-form-item label="密码" prop="password" style="display: flex;">
                <el-input v-model="form.password" />
            </el-form-item>
            <el-form-item label="邮箱" prop="email" style="display: flex;">
                <el-input v-model="form.email" />
            </el-form-item>
            <el-form-item label="手机号" prop="phone" style="display: flex;">
                <el-input v-model="form.phone" />
            </el-form-item>
            <el-form-item label="角色" prop="roleId" style="display: flex;">
                <el-select v-model="form.roleId" placeholder="请选择角色">
                    <el-option v-for="item in obj.roles" :key="item.id" :value="item.id" :label="item.roleName"/>
                </el-select>
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
    loading: false,
    roles: []
})
const form = reactive({
    id: 0,
    name: '',
    loginName: '',
    password: '',
    email: '',
    phone: '',
    roleId: ''
})

const rules = reactive({
    name: [
        { required: true, message: '用户名不可为空', trigger: 'blur' }
    ],
    loginName: [
        { required: true, message: '登录名不可为空', trigger: 'blur' }
    ],
    password: [
        { required: true, message: '密码不可为空', trigger: 'blur' }
    ],
    roleId: [
        { required: true, message: '角色不可为空', trigger: 'blur' }
    ]
})

const handleSubmit = () => {
    formRef.value.validate(async (valid, fields) => {
        if (!valid) {
            return
        }
        let result = null
        if (props.type == 'add') {
            result = await api.user.add(form)
        } else if (props.type == 'edit') {
            result = await api.user.edit(form)
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


watchEffect(async () => {
    obj.loading = true
    if (!props.active) return;
    obj.title = props.type == 'add' ? '新增' : '编辑'
    if (props.type == 'edit') {
        const { data } = await api.user.getUserById(props.id)
        var keys = Object.keys(form)
        keys.forEach(x => {
            form[x] = data[x]
        })
        form.id = data.id
    }

    // 获取所有角色
    const {data} = await api.role.getRoleList()
    obj.roles = data

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