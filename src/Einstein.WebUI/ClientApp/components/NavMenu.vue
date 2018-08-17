<template>
  <section>
    <div class="columns">
      <!-- Left side -->
      <div class="column is-4">
        <div class="columns">
          <div class="column is-3">
            <i class="fas fa-universal-access fa-4x app-logo" />
          </div>
          <div class="column is-9">
            <p class="title">
              Einstein
            </p>
            <p class="subtitle is-tiny">
              Tech Radar
            </p>
          </div>
        </div>
      </div>

      <!-- Center -->
      <div class="column  is-4">
        <b-field style="margin-top:15px;">
          <b-input class="search"
                   placeholder="Search..."
                   type="search"
                   icon-pack="fas"
                   icon="search">
          </b-input>
        </b-field>
      </div>


      <!-- Right side -->
      <div class="column is-4">
        <b-dropdown class="is-pulled-right" style="margin-top:15px;">
          <a class="navbar-item" slot="trigger">
            <div class="columns">
              <div class="column">
                <i class="fas fa-user-circle fa-2x"/>
              </div>
              <div class="column">
                <i class="fas fa-caret-down"/>
              </div>
            </div>
          </a>

          <b-dropdown-item custom>
            {{ userName }} <span v-if="userId > 0">({{ userId }})</span>
          </b-dropdown-item>
          <hr class="dropdown-divider">
          <b-dropdown-item v-if="userId <= 0" value="Login">
            <router-link to="/login">
              <b-icon pack="fas" icon="sign-in-alt" /> Login
            </router-link>
          </b-dropdown-item>
          <b-dropdown-item v-if="userId > 0" value="Logout">
            <router-link to="/logout">
              <b-icon pack="fas" icon="sign-out-alt" /> Logout
            </router-link>
          </b-dropdown-item>
        </b-dropdown>
      </div>
    </div>
    <nav class="tabs">
      <ul>
        <li>
          <router-link class="nav-item is-tab" to="/" exact>Home</router-link>
        </li>
        <li v-if="user && user.admin">
          <router-link class="nav-item is-tab" to="/admin">Admin</router-link>
        </li>
        <li v-if="userId > 0">
          <router-link class="nav-item is-tab" to="/spots">Spots</router-link>
        </li>
        <li v-if="userId > 0">
          <router-link class="nav-item is-tab" to="/sectors">Sectors</router-link>
        </li>
        <li v-if="userId > 0">
          <router-link class="nav-item is-tab" to="/rings">Rings</router-link>
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
      }
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

  .app-logo {
    color: $primary;
    background-color: $primary-invert;
  }

  a.navbar-item div div i {
    color: $primary;
    background-color: $primary-invert;
  }


  .search {
    display: none;
  }
</style>
