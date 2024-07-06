<script setup lang="ts">
import { computed, inject, ref, watch } from 'vue';
import type { ServiceRoots } from '@/services/ServiceRoots';
import type { AuthService } from '@/services/AuthService';
import { useI18n } from 'vue-i18n';
import { storeToRefs } from 'pinia';
import { UseSettingStore } from '@/stores/SettingStore';
import LoginLeftView from '@/views/Login/LoginLeftView.vue';
import LoginRightView from '@/views/Login/LoginRightView.vue';

const loginBgDark = encodeURIComponent(
  '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100" width="100%" height="100%"><rect width="100%" height="100%" fill="#11172950" /><circle cx="65%" cy="60%" r="50%" stroke="#33415595" stroke-dasharray="#2c2042" stroke-width="20px" fill="#0f172a" /></svg>'
);
const loginBgLight = encodeURIComponent(
  '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100" width="100%" height="100%"><rect width="100%" height="100%" fill="#b0ea8810" /><circle cx="65%" cy="60%" r="50%" stroke="#d0f1b450"  stroke-width="20px" fill="#d0f1b4" /></svg>'
);

const { t } = useI18n({});

const services = inject('services') as ServiceRoots;
const authService = services.create('AuthService') as AuthService;
const store = storeToRefs(UseSettingStore());
const background = computed(() => (store.theme.value === 'dark' ? loginBgDark : loginBgLight));
</script>

<template>
  <LoginRightView :store="store" :auth-service="authService" />
</template>
