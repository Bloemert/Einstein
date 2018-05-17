<template>
  <section>
    <b-field label="Dirty" size="is-small">
      <b-checkbox id="userDirty" v-model="dirty" size="is-small" rounded>Record changed!</b-checkbox>
    </b-field>


    <b-field label="Login" size="is-small">
      <b-input id="userLogin" v-model.trim="selected.login" @input="$v.selected.login.$touch()" placeholder="User name or email" size="is-small" rounded></b-input>
    </b-field>

    <b-field label="Active" size="is-small">
      <b-checkbox id="userActive" v-model="selected.active" @input="$v.selected.active.$touch()" size="is-small" rounded>Active</b-checkbox>
    </b-field>

    <div class="form-group" v-bind:class="{ 'form-group--error': $v.selected.newPassword.$error }">
      <b-field label="Password" size="is-small">
        <b-input id="userPassword" type="password" password-reveal v-model.trim="selected.newPassword" @input="$v.selected.newPassword.$touch()" placeholder="New password" size="is-small" rounded></b-input>
      </b-field>
    </div><span class="form-group__message" v-if="!$v.selected.newPassword.required">Password is required.</span><span class="form-group__message" v-if="!$v.selected.newPassword.minLength">Password must have at least {{ $v.selected.newPassword.$params.minLength.min }} letters.</span>

    <div class="form-group" v-bind:class="{ 'form-group--error': $v.confirmPassword.$error }">
      <b-field label="Confirm password" size="is-small">
        <b-input id="userConfirmPassword" type="password" password-reveal v-model.trim="confirmPassword" @input="$v.confirmPassword.$touch()" placeholder="Confirm password" size="is-small" rounded></b-input>
      </b-field>
    </div><span class="form-group__message" v-if="!$v.confirmPassword.sameAsPassword">Passwords must be identical.</span>

    <b-field label="Last login" size="is-small">
      <b-datepicker v-model="lastloginDate"
                    @input="$v.lastloginDate.$touch()"
                    placeholder="Type or select a date..."
                    icon="calendar-today"
                    :readonly="false"
                    size="is-small" rounded>
      </b-datepicker>
      <b-timepicker v-model="lastloginTime"
                    @input="$v.lastloginTime.$touch()"
                    placeholder="Type or select a time..."
                    icon="clock"
                    :readonly="false"
                    size="is-small" rounded>
      </b-timepicker>
    </b-field>

    <b-field label="Expire date" size="is-small">
      <b-datepicker v-model="expireDate"
                    @input="$v.expireDate.$touch()"
                    placeholder="Type or select a date..."
                    icon="calendar-today"
                    :readonly="false"
                    size="is-small" rounded>
      </b-datepicker>
      <b-timepicker v-model="expireTime"
                    @input="$v.expireTime.$touch()"
                    placeholder="Type or select a time..."
                    icon="clock"
                    :readonly="false"
                    size="is-small" rounded>
      </b-timepicker>
    </b-field>

    <b-field label="Good Logins">
      <b-input id="goodLogins" v-model.number="selected.goodLogins" @input="$v.selected.goodLogins.$touch()" type="number" size="is-small" rounded></b-input>
    </b-field>

    <b-field label="Failed attempts">
      <b-input id="failedAttempts" v-model.number="selected.failedAttempts" @input="$v.selected.failedAttempts.$touch()" type="number" size="is-small" rounded></b-input>
    </b-field>

  </section>
</template>

<script>
  import { HTTP } from '../../js/axios-common';
  import { required, sameAs, minLength } from 'vuelidate/lib/validators'

  export default {

    props: ['selected'],

    data() {
      return {
        data: {},
        errors: [],

        isdirty: false,

        confirmPassword: "",
        lastloginDate: null,
        lastloginTime: null,
        expireDate: null,
        expireTime: null,
      }
    },

    validations: {

      selected: {
        login: {
          required
        },
        active: {
          required
        },

        newPassword: {
          required,
          minLength: minLength(8)
        },

        goodLogins: {
        },
        failedAttempts: {
        }
      },

      confirmPassword: {
        sameAsPassword: function (confirmPassword) {
          return this.selected.newPassword == confirmPassword;
        }
      },

      lastloginDate: {
      },
      lastloginTime: {
      },

      expireDate: {
      },
      expireTime: {
      },
    },

    mounted: function () {
      this.isdirty = false;
    },

    watch: {
      '$props': {
        handler: function (val, oldVal) {
          console.log('props: ', oldVal, val)
        },
        deep: true
      }
    },

    computed: {
      dirty: {
        get: function () {
          return this.isdirty ||
            this.$v.selected.login.$dirty ||
            this.$v.selected.active.$dirty;
        },
        set: function (newValue) {
          this.isdirty = newValue;
        }
      }
    },

  }
</script>
