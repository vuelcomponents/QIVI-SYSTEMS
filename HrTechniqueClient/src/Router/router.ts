import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/App/HomeView.vue';
import Unauthorized from '@/Features/Auth/ErrorViews/Unauthorized.vue';
import EmployeeModule from '@/Features/Employee_EmployeeSpecifications/Employee_EmployeeSpecifications_Stepper.vue';

export const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      redirect(to) {
          return to.name = 'employees'
      },
      children:[
        {
          path: 'employees',
          name: 'Employee',
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
