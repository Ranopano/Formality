import Vue from 'vue';
import VueRouter, { RouteConfig, Route } from 'vue-router';
import HomePage from 'app/common/pages/HomePage.vue';
import NotFoundPage from 'app/common/pages/NotFoundPage.vue';
import { Pages } from './pages';

Vue.use(VueRouter);

const idProps = (route: Route) => ({
  id: Number(route.params.id),
});

const routes: RouteConfig[] = [
  {
    path: '/',
    name: Pages.Home,
    component: HomePage,
  },
  {
    path: '/forms/',
    name: Pages.Forms,
    component: () => import('app/forms/pages/FormListPage.vue'),
  },
  {
    path: '/forms/edit/:id/',
    name: Pages.FormEdit,
    component: () => import('app/forms/pages/FormEditPage.vue'),
    props: idProps,
  },
  {
    path: '/forms/:id/',
    name: Pages.FormSubmit,
    component: () => import('app/forms/pages/FormSubmitPage.vue'),
    props: idProps,
  },
  {
    path: '/submissions/',
    name: Pages.Submissions,
    component: () => import('app/submissions/pages/SubmissionListPage.vue'),
  },
  {
    path: '/submissions/:id/',
    name: Pages.SubmissionView,
    component: () => import('app/submissions/pages/SubmissionViewPage.vue'),
    props: idProps,
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
