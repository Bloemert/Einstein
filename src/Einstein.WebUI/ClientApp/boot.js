//require('font-awesome');
require("font-awesome-sass-loader!./font-awesome-sass.config.js");


import Vue from 'vue';
import VueFontAwesome from '@fortawesome/vue-fontawesome';

import VueRouter from 'vue-router';
import Buefy from 'buefy';

import App from "./components/DefaultApp.vue";
import Home from "./components/Home.vue";

Vue.use(VueFontAwesome);
Vue.use(VueRouter);
Vue.use(Buefy);

const routes = [
  { path: '/', component: Home }
];

const router = new VueRouter({
    routes 
})

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
	}
});//.$mount('#einstein')
