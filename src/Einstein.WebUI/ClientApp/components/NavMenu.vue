<template>
  <section>
    <div class="columns">
      <!-- Left side -->
      <div class="column is-4">
        <div class="columns">
          <div class="column is-3">
            <b-icon class="is-large fa-3x" pack="fa" icon="universal-access" />
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
          <b-input placeholder="Search..."
                   type="search"
                   icon-pack="fa"
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
                <b-icon pack="fa fa-2x" icon="user-circle"></b-icon>
              </div>
              <div class="column">
                <b-icon pack="fa" icon="caret-down"></b-icon>
              </div>
            </div>
          </a>

          <b-dropdown-item custom>
            {{ userName }} <span v-if="userId > 0">({{ userId }})</span>
          </b-dropdown-item>
          <hr class="dropdown-divider">
          <b-dropdown-item v-if="userId <= 0" value="Login">
            <router-link to="/login">
              <b-icon pack="fa" icon="sign-in" /> Login
            </router-link>
          </b-dropdown-item>
          <b-dropdown-item v-if="userId > 0" value="Logout">
            <router-link to="/logout">
              <b-icon pack="fa" icon="sign-out" /> Logout
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
        <li v-if="user.admin">
          <router-link class="nav-item is-tab" to="/admin">Admin</router-link>
        </li>
      </ul>
    </nav>
  </section>
</template>
<script>
  import Vue from 'vue';
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
          var clonedUser = _.cloneDeep(this.$store.state.core.currentUser);
          clonedUser.id = value;
          this.$store.dispatch('core/updateCurrentUser', clonedUser);
        }
      },

      userName: {
        get() {
          return this.$store.state.core.currentUser.name;
        },
        set(value) {
          var clonedUser = _.cloneDeep(this.$store.state.core.currentUser);
          clonedUser.name = value;
          this.$store.dispatch('core/updateCurrentUser', clonedUser);
        }
      },

      user: {
        get() {
          return this.$store.state.core.currentUser;
        },
        set(value) {
          var clonedUser = _.cloneDeep(value);
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
  .is-active {
    color: white !important;
    font-weight: bold;
    background-color: hsl(271, 100%, 71%);
  }
</style>
