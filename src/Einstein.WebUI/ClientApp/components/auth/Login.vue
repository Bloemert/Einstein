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
            <button class="button is-primary" @click="login({userName: userName, userPassword: userPassword, router: $router})">
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

  export default {
    name: 'Login',

    data() {
      return {
        userName: 'Guest',
        userPassword: 'Welcome!'
      }
    },

    methods: {
      ...mapActions({
        login: 'login'
      })
    }

    //computed: {
    //  user: {
    //    get() {
    //      return this.$store.state.core.currentUser;
    //    },
    //    set(value) {
    //      this.$store.dispatch('core/setCurrentUser', value);
    //    }
    //  }
    //}

  }


</script>