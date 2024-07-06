import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import Unauthorized from '@/views/Auth/Unauthorized.vue';
import EmployeeModule from '@/views/Products/EmployeeModule.vue';

export const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      redirect(to) {
          return to.name = 'Employees'
      },
      children:[
        {
          path: 'employees',
          name: 'Employees',
          component: EmployeeModule,
        },
      ]
    },
    {
      path:'/unauthorized',
      name:'Unauthorized',
      component:Unauthorized
    }
  ],
});
