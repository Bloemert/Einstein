<template>
  <div class="content">
    <h2>Login</h2>
    <div class="field is-horizontal">
      <div class="field-label is-normal">
        <label class="label">Username</label>
      </div>
      <div class="field-body">
        <div class="field">
          <div class="control">
            <input class="input" type="text"
                   placeholder="Your username" v-model="userName">
          </div>
        </div>
      </div>
    </div>
    <div class="field is-horizontal">
      <div class="field-label is-normal">
        <label class="label">Password</label>
      </div>
      <div class="field-body">
        <div class="field">
          <div class="control">
            <input class="input" type="password"
                   placeholder="Your password" v-model="userPassword">
          </div>
        </div>
      </div>
    </div>
    <div class="field is-horizontal">
      <div class="field-label">
        <!-- Left empty for spacing -->
      </div>
      <div class="field-body">
        <div class="field">
          <div class="control">
            <button class="button is-primary" @click="doLogin()">
              Login
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'

  const { mapState, mapActions } = createNamespacedHelpers('core')

  var _ = require('lodash');

  export default {
    name: 'Login',

    data() {
      return {
        userName: '',
        userPassword: ''
      }
    },

    computed: {
      ...mapState({
        currentUser: state => state.currentUser,
      })
    },

    mounted: function () {
      if (this.currentUser) {
        this.userName = this.currentUser.name;
        this.userPassword = this.currentUser.password;
      }
    },

    methods: {
      doLogin() {
        var clonedUser = _.cloneDeep(this.currentUser);
        clonedUser.name = this.userName;
        clonedUser.password = this.userPassword;
        this.$store.dispatch('core/setCurrentUser',
          clonedUser
        );
      }
    }

  }


</script>