//require('./styles/site.scss');

import babelPolyfill from 'babel-polyfill'

import './js/icons.js';

import router from './router.js';
import store from './store.js';

import Buefy from 'buefy';

import { ServerTable, ClientTable, Event } from 'vue-tables-2';

import Vuelidate from 'vuelidate';

import App from "./components/DefaultApp.vue";
import Home from "./components/Home.vue";


Vue.use(Buefy, {
	defaultIconPack: 'fa'
});
Vue.use(Vuelidate);
Vue.use(ClientTable, { options: {}, useVuex: false, theme: 'bulma', template: 'default'});


// 4. Create and mount the root instance.
// Make sure to inject the router with the router option to make the
// whole app router-aware.

const app = new Vue({
	el: '#einstein',
	router: router,
  data: { 
      name: "Einstein"
  },
  components: {
      'app': App 
	},
	store: store
});

