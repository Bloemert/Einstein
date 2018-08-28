<template>
  <div class="column is-2 menu-back">
    <div class="columns">
      <div class="column is-1 empty-column"></div>

      <div class="column">
        <div>
          <p class="title app-title">
            Einstein
          </p>
          <p class="subtitle is-tiny">
            Skill Radar
          </p>

          <b-dropdown class="is-centered" style="margin-top:15px;margin-bottom:15px;" hoverable>
            <a class="navbar-item" slot="trigger">
              <div class="columns">
                <div class="column">
                  <i class="fas fa-universal-access fa-4x app-logo" />
                </div>
                <!--<div class="column">
                  <i class="fas fa-caret-down" />
                </div>-->
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

        <div>
          <aside class="menu">

            <div class="menu-part">
              <p class="menu-label">
                General
              </p>
              <ul class="menu-list">
                <li><router-link class="nav-item is-tab" to="/" exact>Home</router-link></li>
                <li>
                  <router-link class="nav-item is-tab" to="/about">About</router-link>
                </li>
              </ul>
            </div>

            <div v-if="user && user.id > 0" class="menu-part">
              <p class="menu-label">
                Work
              </p>
              <ul class="menu-list">
                <li>
                  <router-link class="nav-item is-tab" to="/work/employees">Employees</router-link>
                </li>
              </ul>
            </div>

            <div v-if="user && user.id > 0"  class="menu-part">
              <p class="menu-label">
                Skills
              </p>
              <ul class="menu-list">
                <li>
                  <router-link class="nav-item is-tab" to="/skills/skills">Skill(s)</router-link>
                </li>
                <li>
                  <router-link class="nav-item is-tab" to="/skills/types">Type(s)</router-link>
                </li>
                <li>
                  <router-link class="nav-item is-tab" to="/skills/categories">Category('s)</router-link>
                </li>
                <li>
                  <router-link class="nav-item is-tab" to="/skills/rings">Rings</router-link>
                </li>
                <li>
                  <router-link class="nav-item is-tab" to="/skills/sectors">Sectors</router-link>
                </li>
                <li>
                  <router-link class="nav-item is-tab" to="/skills/spots">Spots</router-link>
                </li>
              </ul>
            </div>



            <div v-if="user && user.admin"  class="menu-part">
              <p class="menu-label">
                Administration
              </p>
              <ul class="menu-list">
                <li>
                  <router-link class="nav-item is-tab" to="/admin/users">User(s)</router-link>
                </li>
                <li>
                  <router-link class="nav-item is-tab" to="/admin/logs">Log(s)</router-link>
                </li>
              </ul>

            </div>

          </aside>
        </div>
      </div>
    </div>
  </div>

</template>

<script>

  export default Vue.extend({
    name: 'MainMenu',
    data: function () {
      return {
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
    }
  });
</script>

<style lang="scss">
  @import '../styles/site.scss';

  .menu-back {
    background-color: $primary;
  }

  .menu-part {
    margin-bottom: 25px;
  }

  .app-logo {
    color: $primary-invert;
    background-color: $primary;
  }

  .title.app-title {
    color: white;
  }
</style>