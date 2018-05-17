<template>
  <div class="content">
    <h2>Logout</h2>
    <p>
      {{ userName }}, we hate to see you leave!
    </p>

    <div class="field is-horizontal">
      <div class="field-label">
        <!-- Left empty for spacing -->
      </div>

      <div class="control">
        <button class="button is-primary" @click="logout({router: $router})">Do Logout..
        </button>
      </div>

    </div>
  </div>
</template>
<script>
  import { HTTP } from '../../js/axios-common';
  import { createNamespacedHelpers } from 'vuex'
  const { mapState, mapActions } = createNamespacedHelpers('core')
  var _ = require('lodash');


  export default {
    name: 'Logout',

    computed: {
      userId: {
        get() {
          return this.$store.state.core.currentUser.id;
        },
        set(value) {
          var clonedUser = _.cloneDeep(this.$store.state.core.currentUser);
          clonedUser.id = value;
          this.$store.dispatch('core/setCurrentUser', clonedUser);
        }
      },
      userName: {
        get() {
          return this.$store.state.core.currentUser.name;
        },
        set(value) {
          var clonedUser = _.cloneDeep(this.$store.state.core.currentUser);
          clonedUser.name = value;
          this.$store.dispatch('core/setCurrentUser', clonedUser);
        }
      },
      userPassword: {
        get() {
          return this.$store.state.core.currentUser.password;
        },
        set(value) {
          var clonedUser = _.cloneDeep(this.$store.state.core.currentUser);
          clonedUser.password = value;
          this.$store.dispatch('core/setCurrentUser', clonedUser);
        }
      },
      userAdmin: {
        get() {
          return this.$store.state.core.currentUser.admin;
        },
        set(value) {
          var clonedUser = _.cloneDeep(this.$store.state.core.currentUser);
          clonedUser.admin = value;
          this.$store.dispatch('core/setCurrentUser', clonedUser);
        }
      }
    },
    methods: {
      ...mapActions({
        logout: 'logout'
      })
    }
  }
</script>
