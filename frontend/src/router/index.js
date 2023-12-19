import { createRouter, createWebHistory } from 'vue-router';
import useUserStore from '@/stores/user';
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
    beforeEnter: (to, from, next) => {
      console.log('beforeEnter hook for manage router')
      next();
    },
    meta: {
      requiresAuth: true,
    }
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

router.beforeEach((to, from, next) => {
  if (!to.meta.requiresAuth) {
    next(); // it doesnt require auth
    return;
  }

  const userStore = useUserStore();
  if (userStore.isLoggedIn) {
    next(); // we allow user stay on the page
  } else {
    next({ name: 'home' });
  }
});

export default router;
