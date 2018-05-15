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
            User Name
          </b-dropdown-item>
          <hr class="dropdown-divider">
          <b-dropdown-item value="Logout">
            <b-icon pack="fa" icon="power-off" /> Logout
          </b-dropdown-item>
        </b-dropdown>
      </div>
    </div>

    <nav class="tabs">
      <ul>
        <li>
          <router-link class="nav-item is-tab" to="/" exact>Home</router-link>
        </li>
        <li>
          <router-link class="nav-item is-tab" to="/admin/test">AdminTest</router-link>
        </li>
      </ul>
    </nav>
  </section>

</template>

<script>
    import Vue from 'vue';
    import UsersTable from './auth/UsersTable.vue';

    export default Vue.extend({
        name: 'NavMenu',
        data() {
            return {
                isOpen: true
            }
        },
        components: {
            'users-table': UsersTable
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