<template>
    <el-dialog v-model="dialogVisible" :title="title" width="700px" :before-close="handleClose">
        <el-form :inline="true" ref="formRef" :model="form" :rules="rules" label-width="auto">
            <el-form-item label="上级菜单" style="display: flex;" v-if="props.parentId != 0">
                <el-select v-model="form.parentId" placeholder="Select" disabled style="width: 240px">
                    <el-option v-for="item in parentMenus" :label="item.name" :value="item.id" />
                </el-select>
            </el-form-item>
            <el-form-item label="菜单类型" style="display: flex;">
                <el-radio-group v-model="form.type">
                    <el-radio value="directory">目录</el-radio>
                    <el-radio value="menu">菜单</el-radio>
                    <el-radio value="button">按钮</el-radio>
                </el-radio-group>
            </el-form-item>
            <el-form-item label="菜单名称" prop="name">
                <el-input v-model="form.name" />
            </el-form-item>
            <el-form-item label="组件路径">
                <el-input v-model="form.componentPath" />
            </el-form-item>
            <el-form-item label="图标" style="display: flex;" prop="icon">
                <el-select v-model="form.icon">
                    <el-option v-for="item in icons" :key="item" :label="item" :value="item">
                        <component :is="item" style="width: 20px;" /> {{ item }}
                    </el-option>
                    <template #tag>
                        <component :is="form.icon" style="width: 20px;" /> {{ form.icon }}
                    </template>
                </el-select>
            </el-form-item>
            <el-form-item label="权限字符" prop="pre">
                <el-input v-model="form.pre" />
            </el-form-item>
            <el-form-item label="路由地址" prop="route">
                <el-input v-model="form.route" />
            </el-form-item>
            <el-form-item label="排序">
                <el-input v-model="form.sortOrder" />
            </el-form-item>
            <el-form-item label="状态">
                <el-radio-group v-model="form.state">
                    <el-radio :value="1">启用</el-radio>
                    <el-radio :value="0">禁用</el-radio>
                </el-radio-group>
            </el-form-item>
            <el-form-item label="是否缓存">
                <el-radio-group v-model="form.isCache">
                    <el-radio :value="true">缓存</el-radio>
                    <el-radio :value="false">不缓存</el-radio>
                </el-radio-group>
            </el-form-item>
            <el-form-item label="是否隐藏" style="margin-left: 60px;">
                <el-radio-group v-model="form.isHide">
                    <el-radio :value="true">隐藏</el-radio>
                    <el-radio :value="false">不隐藏</el-radio>
                </el-radio-group>
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
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import { api } from '@/tools/api'
import { ElMessage } from 'element-plus'

const dialogVisible = ref(false)
const emits = defineEmits(['onClosed'])
const props = defineProps({
    active: Boolean,
    parentId: Number,
    id: Number,
    type: String
})

const parentMenus = ref([])
const formRef = ref()
const icons = ref([])
const title = ref('新增')
const form = reactive({
    name: '',
    type: 'directory',
    sortOrder: 0,
    route: '',
    pre: '',
    componentPath: '',
    icon: '',
    isCache: false,
    isHide: false,
    state: 0,
    parentId: 0
})

const rules = reactive({
    name: [
        { required: true, message: '菜单名称不可为空', trigger: 'blur' },
    ],
    route: [
        { required: true, message: '路由地址不可为空', trigger: 'blur' },
    ],
    icon: [
        { required: true, message: '图标不可为空', trigger: 'blur' },
    ],
    pre: [
        { required: true, message: '权限字符不可为空', trigger: 'blur' },
    ]
})

// 获取所有图标
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    icons.value.push(key)
}

const handleSubmit = () => {
    formRef.value.validate(async (valid, fields) => {
        if (!valid) {
            return
        }
        let result = null
        if(props.type == 'add'){
            result = await api.menu.add(form)
        }else if(props.type == 'edit'){
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
    form.componentPath = ''
    form.id = 0
    emits('onClosed', false)
}


watchEffect(async () => {
    if(!props.active) return;

    if(props.parentId != 0){
        parentMenus.value = []
        var parent = await api.menu.getMenyById(props.parentId)
        parentMenus.value.push(parent.data)
        form.parentId = props.parentId
    }
    title.value = props.type == 'add' ? '新增' : '编辑'
    if(props.type == 'edit'){
        const {data} = await api.menu.getMenyById(props.id)
        
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