<template>
  <div v-if="user.role > 1">
    <AdminDasboard />
  </div>
  <div v-else>
    <UserDashboard />
  </div>
</template>
<script setup lang="ts">
import { UseUserStore } from '@/stores/UserStore';
import AdminDasboard from '@/views/Dashboard/AdminDasboard.vue';
import UserDashboard from '@/views/Dashboard/UserDashboard.vue';

const userStore = UseUserStore();
const user = userStore.user;
</script>
