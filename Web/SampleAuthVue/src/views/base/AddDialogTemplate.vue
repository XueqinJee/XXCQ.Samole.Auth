<template>
    <el-dialog v-model="dialogVisible" :title="title" width="700px" :before-close="handleClose">
        <el-form :inline="true" ref="formRef" :model="form" :rules="rules" label-width="auto">
            
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
const title = ref('新增')
const form = reactive({
    id: 0,
    roleName: '',
    description: '',
    menuIds: []
})

const rules = reactive({
})

const handleSubmit = () => {
    formRef.value.validate(async (valid, fields) => {
        if (!valid) {
            return
        }
        let result = null
        if (props.type == 'add') {
            result = await api.menu.add(form)
        } else if (props.type == 'edit') {
            result = await api.menu.edit(form)
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
    if (!props.active) return;

    if (props.parentId != 0) {
        parentMenus.value = []
        var parent = await api.menu.getMenyById(props.parentId)
        parentMenus.value.push(parent.data)
        form.parentId = props.parentId
    }
    title.value = props.type == 'add' ? '新增' : '编辑'
    if (props.type == 'edit') {
        const { data } = await api.menu.getMenyById(props.id)

        var keys = Object.keys(form)
        keys.forEach(x => {
            form[x] = data[x]
        })
        form.id = data.id
    }
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