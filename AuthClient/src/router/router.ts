import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import LoginView from '../views/Login/LoginView.vue';
import ServerDownView from '../views/Auth/ServerDownView.vue';
import UsersManagementView from '@/views/Users/UsersManagementView.vue';
import EditAccountView from '@/views/Account/EditAccount/EditAccountView.vue';
import ResetPasswordView from '@/views/Auth/ResetPasswordView.vue';
import DashboardView from '@/views/Dashboard/DashboardView.vue';
export const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      redirect: {
        name: 'Dashboard',
      },
      children: [
        {
          path: 'dashboard',
          name: 'Dashboard',
          component: DashboardView,
        },
        {
          path: 'users-management',
          name: 'UsersManagement',
          component: UsersManagementView,
        },
        {
          path: 'edit-account',
          name: 'EditAccount',
          component: EditAccountView,
        },
      ],
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/serverdown',
      name: 'ServerDown',
      component: ServerDownView,
    },
    {
      path: '/reset-password/:t',
      name: 'ResetPassword',
      component: ResetPasswordView,
    },
  ],
});
