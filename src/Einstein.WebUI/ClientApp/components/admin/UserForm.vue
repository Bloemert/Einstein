<!--

Remarks:
1.  b-field horizontal together with dynamic type setting does not show e.g. is-danger red box!
    Workarround is using extra inner b-field.
2.  vuevalidate (validation lib used) needs the variable path/name used in :value to be the same as in the validators part else validator values won't update correctly!

-->

<template>
  <section class="form-scrollable">

    <div v-if="!selected">
      No item selected! <br />
      Select or create one first!
    </div>
    <div v-else>
      <form @submit.prevent="">

        <b-field label="Active" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.active)">
              <b-checkbox id="userActive"
                          size="is-large"
                          type="is-success"
                          :value="selected.active" @change.native="selected.active = $event.target.checked"
                          @input="onInput($v.selected.active, 'active', $event)"
                          >
              </b-checkbox>
            </b-field>
            <div>
              <span class="help is-warning">Most times you need this checked!</span>
            </div>
          </div>
        </b-field>

        <b-field label="Login" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.login)">
              <b-input id="userLogin"
                       :value="selected.login" @change.native="selected.login = $event.target.value"
                       autocomplete="username"
                       @input="onInput($v.selected.login, 'login', $event)"
                       placeholder="New login">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.login.required">Login is required.</span>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.login.minLength">Login must have at least {{ $v.selected.login.$params.minLength.min }} letters.</span>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.login.isUnique">Login is already taken.</span>
            </div>
          </div>
        </b-field>

        <b-field label="Firstname" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.firstname)">
              <b-input id="userFirstname"
                       :value="selected.firstname" @change.native="selected.firstname = $event.target.value"
                       autocomplete="first-name"
                       @input="onInput($v.selected.firstname, 'firstname', $event)"
                       placeholder="New firstname">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.firstname.required">Firstname is required.</span>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.firstname.minLength">Firstname must have at least {{ $v.selected.firstname.$params.minLength.min }} letters.</span>
            </div>
          </div>
        </b-field>

        <b-field label="Lastname" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.lastname)">
              <b-input id="userLastname"
                       :value="selected.lastname" @change.native="selected.lastname = $event.target.value"
                       autocomplete="family-name"
                       @input="onInput($v.selected.lastname, 'lastname', $event)"
                       placeholder="New lastname">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.lastname.required">Lastname is required.</span>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.lastname.minLength">Lastname must have at least {{ $v.selected.lastname.$params.minLength.min }} letters.</span>
            </div>
          </div>
        </b-field>

        <b-field label="Email" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.email)">
              <b-input id="userEmail"
                       :value="selected.email" @change.native="selected.email = $event.target.value"
                       autocomplete="email"
                       @input="onInput($v.selected.email, 'email', $event)"
                       placeholder="New email">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.email.required">Email is required.</span>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.email.email">Email must be valid!</span>
            </div>
          </div>
        </b-field>



        <b-field label="Password" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.newPassword)">
              <b-input id="userPassword" type="password"
                       password-reveal
                       :value="selected.newPassword" @change.native="selected.newPassword = $event.target.value"
                       @input="onInput($v.selected.newPassword, 'newPassword', $event)"
                       placeholder="New password"></b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.newPassword.required">Password is required.</span>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.newPassword.minLength">Password must have at least {{ $v.selected.newPassword.$params.minLength.min }} letters.</span>
            </div>
          </div>
        </b-field>

        <b-field label="Confirm" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.confirmPassword)">
              <b-input id="userConfirmPassword" type="password"
                       password-reveal
                       :value="selected.confirmPassword" @change.native="selected.confirmPassword = $event.target.value"
                       @input="onInput($v.selected.confirmPassword, 'confirmPassword', $event)"
                       placeholder="Confirm password">
              </b-input>
            </b-field>
            <span class="help is-danger" v-if="isFormDirty() && !$v.selected.confirmPassword.sameAsPassword">Passwords must be identical.</span>
          </div>
        </b-field>


        <b-field label="Expire date" horizontal>
          <b-datepicker :value="selected.expireDate" @change.native="selected.expireDate = $event.target.value"
                        placeholder="Type or select a date..."
                        icon="calendar"
                        :readonly="false">
          </b-datepicker>
          <b-timepicker :value="selected.expireDate" @change.native="selected.expireDate = $event.target.value"
                        placeholder="Type or select a time..."
                        icon="clock"
                        :readonly="false">
          </b-timepicker>
        </b-field>


        <b-field label="Last login" horizontal>
          <b-datepicker :value="selected.lastLogin" @change.native="selected.lastLogin = $event.target.value"
                        placeholder="Type or select a date..."
                        icon="calendar"
                        :readonly="true">
          </b-datepicker>
          <b-timepicker :value="selected.lastLogin" @change.native="selected.lastLogin = $event.target.value"
                        placeholder="Type or select a time..."
                        icon="clock"
                        :readonly="true">
          </b-timepicker>
        </b-field>

        <b-field label="Good Logins" horizontal>
          <b-input id="goodLogins" :value="selected.goodLogins" @change.native="selected.goodLogins = $event.target.value" type="number" :readonly="true"></b-input>
        </b-field>

        <b-field label="Failed attempts" horizontal>
          <b-input id="failedAttempts" :value="selected.failedAttempts" @change.native="selected.failedAttempts = $event.target.value" type="number" :readonly="true"></b-input>
        </b-field>

      </form>
      </div>

  </section>

</template>

<script>
  import { HTTP } from '../../js/axios-common';

  import { mapUsersActions, mapUsersFields, mapUsersMultiRowFields } from './store/users';

  import { required, requiredIf, email, sameAs, minLength } from 'vuelidate/lib/validators';

  export default {

    data() {
      return {
        selectedSeqnoChanged: true,

        errors: []
      }
    },

    methods: {
      ...mapUsersActions({
        isUserUniqueByField: 'isUniqueByField'
      }),

      isFormDirty() {
        return this.$v.form.$anyDirty || this.$v.passwordsForm.$anyDirty;
      },

      storeValidFieldData(fieldValidator, fieldName, fieldValue) {
          if (!fieldValidator.$anyError) {
            if (this.selected[fieldName] === fieldValue) {
              fieldValidator.$reset();
            }
            else {
              this.selected[fieldName] = fieldValue;
            }
          }
      },

      onInput(fieldValidator, fieldName, fieldValue) {
        if (!this.selectedSeqnoChanged) {
          fieldValidator.$touch();
        }
      },

      fieldType(v) {
        return v.$anyDirty ? v.$anyError ? 'is-danger' : 'is-success' : '';
      }


    },



    updated: function (par1, par2, par3) {
      this.$nextTick(function () {
        // Code that will run only after the
        // entire view has been re-rendered
        if (this.selectedSeqnoChanged) {
          this.$v.$reset();
          this.selectedSeqnoChanged = false;
        }
      })
    },


    watch: {
      selectedSeqno: function (newSeqno, oldSeqno) {
        if (newSeqno != oldSeqno) {
          this.selectedSeqnoChanged = true;
        }
      }
    },

    computed: {
      ...mapUsersFields({
        selectedSeqno: 'selectedSeqno',
      }),

      ...mapUsersMultiRowFields(
        { users: 'rows' }
      ),

      selected: {
        get: function() {
          return this.users[this.selectedSeqno];
        }
      }
    },


    mounted: function () {
      this.$v.$reset();
    },


    validations: {
      selected: {
        login: {
          required,
          minLength: minLength(4),
          async isUnique(value) {
            if (value === '' ) return true;

            return await this.isUserUniqueByField({ fieldName: 'login', fieldValue: value })
              .then((unique) => {
                return unique;
              })
              .catch((error) => {
                return false;
              });
          }
        },
        lastname: {
          required,
          minLength: minLength(4)
        },
        firstname: {
          required,
          minLength: minLength(4)
        },
        email: {
          required,
          email
        },
        active: {
        },
        newPassword: {
          required: requiredIf(function (model) {
            return this.$v.selected.newPassword.$dirty || this.$v.selected.confirmPassword.$dirty
          }),
          minLength: minLength(6),
          sameAsPassword: function (newPassword) {
            return this.selected.confirmPassword == newPassword;
          }
        },
        confirmPassword: {
          sameAsPassword: function (confirmPassword) {
            return this.selected.newPassword == confirmPassword;
          }
        },

        expireDate: {

        },

        lastLogin: {

        },
      },

      form: ['selected.login', 'selected.active', 'selected.firstname', 'selected.lastname', 'selected.email'],

      // Passwords validation are threated differently!
      passwordsForm: ['selected.newPassword', 'selected.confirmPassword'],
    }
  }
</script>

<style lang="scss">
  @import '../../styles/site.scss';
  
  .form-scrollable {
    height: calc(100vh - ( 360px ));
    overflow-y: scroll;
  }

</style>