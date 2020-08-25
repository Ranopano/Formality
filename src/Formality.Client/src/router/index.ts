import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import HomePage from 'app/common/pages/HomePage.vue';
import NotFoundPage from 'app/common/pages/NotFoundPage.vue';
import { Pages } from './types';

Vue.use(VueRouter);

const routes: RouteConfig[] = [
  {
    path: '/',
    name: Pages.Home,
    component: HomePage,
  },
  {
    path: '/forms/',
    name: Pages.Forms,
    component: () => import('@/app/forms/pages/FormsPage.vue'),
  },
  {
    path: '/forms/edit/:id/',
    name: Pages.FormEdit,
    component: () => import('@/app/forms/pages/FormEditPage.vue'),
    props: route => ({
      id: Number(route.params.id),
    }),
  },
  {
    path: '/forms/:id/',
    name: Pages.FormSubmit,
    component: () => import('@/app/forms/pages/FormSubmitPage.vue'),
    props: route => ({
      id: Number(route.params.id),
    }),
  },
  {
    path: '/submissions/',
    name: Pages.Submissions,
    component: () => import('@/app/submissions/pages/SubmissionsPage.vue'),
  },
  {
    path: '*',
    name: Pages.NotFound,
    component: NotFoundPage,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
