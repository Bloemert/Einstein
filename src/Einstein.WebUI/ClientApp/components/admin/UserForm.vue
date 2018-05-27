<!--

Remarks:
1.  b-field horizontal together with dynamic type setting does not show e.g. is-danger red box!
    Workarround is using extra inner b-field.
2.  vuevalidate (validation lib used) needs the variable path/name used in :value to be the same as in the validators part else validator values won't update correctly!

-->

<template>
  <section class="form-scrollable">

    <form @submit.prevent="">
      <b-tag :type="form.status.result == 'OK' ? 'is-primary block' : 'is-danger block'">
        Status: {{ form.status.action }} => {{ form.status.result }}
      </b-tag>

      <b-field label="Login" horizontal>
        <div class="tile is-vertical">
          <b-field :type="fieldType($v.login)">
            <b-input id="userLogin"
                     v-model.lazy="login"
                     @input="onInput($v.login, 'login', $event)"
                     placeholder="New login">
            </b-input>
          </b-field>
          <div>
            <span class="help is-danger" v-if="$v.login.$dirty && !$v.login.required">Login is required.</span>
            <span class="help is-danger" v-if="$v.login.$dirty && !$v.login.minLength">Login must have at least {{ $v.login.$params.minLength.min }} letters.</span>
            <span class="help is-danger" v-if="$v.login.$dirty && !$v.login.isUnique">Login is already taken.</span>
          </div>
        </div>
      </b-field>


      <b-field label="Active" horizontal>
        <div class="tile is-vertical">
          <b-field>
            <b-checkbox id="userActive"
                        v-model.lazzy="active">
            </b-checkbox>
          </b-field>
          <div>
            <span class="help is-warning">Most times you need this checked!</span>
          </div>
        </div>
      </b-field>

      <b-field label="Password" horizontal>
        <div class="tile is-vertical">
          <b-field>
            <b-input id="userPassword" type="password"
                     password-reveal
                     v-model.lazzy="newPassword"
                     placeholder="New password"></b-input>
          </b-field>
          <div>
            <span class="help is-danger" v-if="$v.newPassword.$dirty && !$v.newPassword.required">Password is required.</span>
            <span class="help is-danger" v-if="$v.newPassword.$dirty && !$v.newPassword.minLength">Password must have at least {{ $v.newPassword.$params.minLength.min }} letters.</span>
          </div>
        </div>
      </b-field>

      <b-field label="Confirm" horizontal>
        <div class="tile is-vertical">
          <b-field>
            <b-input id="userConfirmPassword" type="password"
                     password-reveal
                     v-model.lazzy="$v.confirmPassword.$model"
                     placeholder="Confirm password">
            </b-input>
          </b-field>
          <span class="help is-danger" v-if="($v.newPassword.$dirty || $v.confirmPassword.$dirty) && !$v.confirmPassword.sameAsPassword">Passwords must be identical.</span>
        </div>
      </b-field>


      <b-field label="Expire date" horizontal>
        <b-datepicker v-model.lazzy="expireDate"
                      placeholder="Type or select a date..."
                      icon="calendar"
                      :readonly="false">
        </b-datepicker>
        <b-timepicker v-model.lazzy="expireDate"
                      placeholder="Type or select a time..."
                      icon="clock"
                      :readonly="false">
        </b-timepicker>
      </b-field>


      <b-field label="Last login" horizontal>
        <b-datepicker v-model.lazzy="lastLogin"
                      placeholder="Type or select a date..."
                      icon="calendar"
                      :readonly="true">
        </b-datepicker>
        <b-timepicker v-model.lazzy="lastLogin"
                      placeholder="Type or select a time..."
                      icon="clock"
                      :readonly="true">
        </b-timepicker>
      </b-field>

      <b-field label="Good Logins" horizontal>
        <b-input id="goodLogins" :value.number="goodLogins" type="number" :readonly="true"></b-input>
      </b-field>

      <b-field label="Failed attempts" horizontal>
        <b-input id="failedAttempts" :value.number="failedAttempts" type="number" :readonly="true"></b-input>
      </b-field>

    </form>

  </section>

</template>

<script>
  import { HTTP } from '../../js/axios-common';

  import { mapUsersActions, mapUsersFields, mapUsersMultiRowFields } from './store/users';

  import { required, requiredIf, sameAs, minLength } from 'vuelidate/lib/validators';

  export default {

    data() {
      return {
        selectedChanged: true,

        form: {
          status: {
            action: 'Loading',
            result: 'OK'
          },

          isdirty: false
        },
          
        errors: []
      }
    },

    methods: {
      ...mapUsersActions({
        isUserUniqueByField: 'isUniqueByField'
      }),

      isFormDirty: function() {
        return  
          this.$v.login.$dirty && 
          this.$v.active.$dirty &&
          this.$v.newPassword.$dirty &&
          this.$v.confirmPassword.$dirty &&
          this.$v.expireDate.$dirty
      },

      onInput(fieldValidator, fieldName, fieldValue) {
        if (!this.selectedChanged) {
          fieldValidator.$touch();
        }
        //else if (fieldValidator.$dirty) {
        //  fieldValidator.$reset();
        //}
        this.selectedChanged = false;
      },

      /*
       * Vuevalidate behaviour is using AND on $dirty for all childrens validators in a form.
       * For $invalid the behaviour is using OR as what we want.
       * For $error should be ($invalid && $dirty) but our expirience is different..
       * We like an OR so beneath a dedicated function...
       */

      fieldType(v) {
        return v.$error ? 'is-danger' : !this.selectedChanged && v.$dirty ? 'is-success' : '';
      }

    },

    computed: {
      ...mapUsersFields([
        'selectedUser.status',
        'selectedUser.login',
        'selectedUser.active',
        'selectedUser.newPassword',
        'selectedUser.expireDate',
        'selectedUser.lastLogin',
        'selectedUser.failedAttempts',
        'selectedUser.goodLogins',
        'selectedSeqno'
      ])
    },

    watch: {
      selectedSeqno: function (newValue, oldValue) {
        if (newValue != oldValue) {
          this.selectedChanged = true;
          this.$v.$reset();
        }
      }
    },

    mounted: function () {
      this.form.status = { action: 'Loaded', result: 'OK' };
    },


    validations: {

        login: {
          required,
          minLength: minLength(4),
          async isUnique(value) {
            if (value === '' || this.selectedChanged ) return true;

            return await this.isUserUniqueByField({ fieldName: 'login', fieldValue: value })
              .then((unique) => {
                return unique;
              })
              .catch((error) => {
                return false;
              });
          }
        },
        active: {
          //minLength: minLength(1)
        },
        newPassword: {
          required: requiredIf(function (model) {
            return this.$v.newPassword.$dirty || this.$v.confirmPassword.$dirty
          }),
          minLength: minLength(6)
        },
        confirmPassword: {
          sameAsPassword: function (confirmPassword) {
            return this.newPassword == confirmPassword;
          }
        },

        expireDate: {

        },

        lastLogin: {

        },


      form: ['login', 'active'],

      // Passwords validation are threated differently!
      passwordsForm: ['newPassword', 'confirmPassword'],
    }
  }
</script>

<style lang="scss">

  .form-scrollable {
    height: calc(100vh - ( 360px ));
    overflow-y: scroll;
  }
</style>