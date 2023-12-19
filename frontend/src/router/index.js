import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/views/Home.vue';
import Manage from '@/views/Manage.vue';

const routes = [
  {
    name: 'home',
    path:'/', // -> example.com
    component: Home,
  },
  {
    name: 'manage',
    path:'/manage-cars', // -> example.com
    component: Manage,
  },
  {
    path: '/manage',
    redirect: { name: 'manage' }, // redirecting from /manage to /manage-music
  },
  {
    path: '/:catchAll(.*)*',
    redirect: { name: 'home' }
  }
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

export default router;
