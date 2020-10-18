import Vue from 'vue';
import { BootstrapVue } from 'bootstrap-vue';
import App from 'app/App.vue';
import dayjs from 'dayjs';
import router from './router';
import store from './store';
import utc from 'dayjs/plugin/utc';

import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-vue/dist/bootstrap-vue.min.css';
import 'typeface-open-sans';

dayjs.extend(utc);

Vue.use(BootstrapVue);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
