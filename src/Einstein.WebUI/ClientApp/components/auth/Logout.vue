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
      userName: {
        get() {
          return this.$store.state.core.currentUser.name;
        },
        set(value) {
          var clonedUser = _.cloneDeep(this.$store.state.core.currentUser);
          clonedUser.name = value;
          this.$store.dispatch('core/updateCurrentUser', clonedUser);
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
