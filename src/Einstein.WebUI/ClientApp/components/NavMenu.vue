<template>
  <section>

    <nav class="tabs">
      <ul>
        <li>
          <router-link class="nav-item is-tab" to="/" exact>Home</router-link>
        </li>
        <li v-if="user && user.admin">
          <router-link class="nav-item is-tab" to="/admin">Admin</router-link>
        </li>
        <li v-if="userId > 0">
          <router-link class="nav-item is-tab" to="/work">Work</router-link>
        </li>
        <li v-if="userId > 0">
          <router-link class="nav-item is-tab" to="/skills">Skills</router-link>
        </li>
        <li>
          <router-link class="nav-item is-tab" to="/about">About</router-link>
        </li>
      </ul>
    </nav>
  </section>
</template>
<script>
  import Login from './auth/Login.vue';

  import { mapGetters } from 'vuex';
  export default Vue.extend({
    name: 'NavMenu',
    data() {
      return {
        isOpen: true
      };
    },
    computed: {

      userId: {
        get() {
          return this.$store.state.core.currentUser.id;
        },
        set(value) {
          var clonedUser = _cloneDeep(this.$store.state.core.currentUser);
          clonedUser.id = value;
          this.$store.dispatch('core/updateCurrentUser', clonedUser);
        }
      },

      userName: {
        get() {
          return this.$store.state.core.currentUser.name;
        },
        set(value) {
          var clonedUser = _cloneDeep(this.$store.state.core.currentUser);
          clonedUser.name = value;
          this.$store.dispatch('core/updateCurrentUser', clonedUser);
        }
      },

      user: {
        get() {
          return this.$store.state.core.currentUser;
        },
        set(value) {
          var clonedUser = _cloneDeep(value);
          this.$store.dispatch('core/updateCurrentUser', clonedUser);
        }
      }
    },

    created() {
      let sessionUser = window.sessionStorage.getItem('sessionUser');
      if (sessionUser && sessionUser != 'null') {
        this.$store.dispatch('core/updateCurrentUser', JSON.parse(sessionUser));
      }
    },

    components: {
      'login': Login
    }
  });
</script>
<style lang="scss">
@import '../styles/site.scss';

  .nav-item.is-tab.is-active {
    font-weight: bold;
    background-color: $primary;
    color: $primary-invert;
  }

  a.navbar-item div div i {
    color: $primary;
    background-color: $primary-invert;
  }


  .search {
    display: none;
  }
</style>
