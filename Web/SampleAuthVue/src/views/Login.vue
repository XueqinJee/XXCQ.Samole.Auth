<template>
  <div class="flex h-screen bg-gray-100 justify-center items-center">
    <el-form :model="loginForm" :rules="loginRules" ref="formRef" label-width="80px"
      class="bg-white p-8 rounded shadow-md w-full max-w-md">
      <h1 class="text-2xl font-bold text-center mb-6">登录</h1>
      <el-form-item label="用户名" prop="username">
        <el-input v-model="loginForm.username" placeholder="请输入用户名"></el-input>
      </el-form-item>
      <el-form-item label="密码" prop="password">
        <el-input v-model="loginForm.password" type="password" placeholder="请输入密码"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="handleLogin">登录</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import { ElMessage } from 'element-plus';
import { useUserStore } from '@/store/userStore'
import { useRoute, useRouter } from 'vue-router'

const router = useRouter()
const route = useRoute()
const userStore = useUserStore()
const formRef = ref(null);
const loginForm = reactive({
  username: '',
  password: ''
});

const loginRules = {
  username: [
    { required: true, message: '请输入用户名', trigger: 'blur' }
  ],
  password: [
    { required: true, message: '请输入密码', trigger: 'blur' }
  ]
};

const handleLogin = async () => {
  const valid = await formRef.value.validate();
  if (valid) {
    var login = await userStore.login(loginForm.username, loginForm.password)
    if (!login) {
      return;
    }
    router.push('/')
    ElMessage.success('登录成功');
  } else {
    ElMessage.error('请检查输入信息');
  }
};
</script>

<style scoped>
/* 可以添加自定义样式 */
</style>