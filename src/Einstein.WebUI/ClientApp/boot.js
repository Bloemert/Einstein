require("font-awesome-sass-loader!./font-awesome-sass.config.js");

import Vue from 'vue';

import router from './router.js';
import store from './store.js';

import VueFontAwesome from '@fortawesome/vue-fontawesome';


import Buefy from 'buefy';
import 'buefy/lib/buefy.css';

import Vuelidate from 'vuelidate';

import App from "./components/DefaultApp.vue";
import Home from "./components/Home.vue";

Vue.use(VueFontAwesome);
Vue.use(Buefy);
Vue.use(Vuelidate);




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

