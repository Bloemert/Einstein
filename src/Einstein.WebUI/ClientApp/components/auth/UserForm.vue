<template>
  <section>

      <b-field label="Login" size="is-small">
        <b-input id="userLogin" v-model.trim="selected.login" placeholder="User name or email" size="is-small" rounded></b-input>
      </b-field>

      <b-field label="Active" size="is-small">
        <b-checkbox id="userActive" v-model="selected.active" size="is-small" rounded>Active</b-checkbox>
      </b-field>


      <b-field label="Password" size="is-small">
        <b-input id="userPassword" type="password" password-reveal v-model="selected.newPassword" placeholder="New password" size="is-small" rounded></b-input>
      </b-field>

      <b-field label="Confirm password" size="is-small">
        <b-input id="userConfirmPassword" type="password" password-reveal v-model="confirmPassword" placeholder="Confirm password" size="is-small" rounded></b-input>
      </b-field>


      <b-field label="Last login" size="is-small">
        <b-datepicker v-model="lastloginDate"
                      placeholder="Type or select a date..."
                      icon="calendar-today"
                      :readonly="false"
                      size="is-small" rounded>
        </b-datepicker>
        <b-timepicker v-model="lastloginTime"
                      placeholder="Type or select a time..."
                      icon="clock"
                      :readonly="false"
                      size="is-small" rounded>
        </b-timepicker>
      </b-field>

      <b-field label="Expire date" size="is-small">
        <b-datepicker v-model="expireDate"
                      placeholder="Type or select a date..."
                      icon="calendar-today"
                      :readonly="false"
                      size="is-small" rounded>
        </b-datepicker>
        <b-timepicker v-model="expireTime"
                      placeholder="Type or select a time..."
                      icon="clock"
                      :readonly="false"
                      size="is-small" rounded>
        </b-timepicker>
      </b-field>

      <b-field label="Good Logins">
        <b-input id="goodLogins" v-model.number="selected.goodLogins" type="number" size="is-small" rounded></b-input>
      </b-field>

      <b-field label="Failed attempts">
        <b-input id="failedAttempts" v-model.number="selected.failedAttempts" type="number" size="is-small" rounded></b-input>
      </b-field>
    
  </section>
</template>

<script>
  import { HTTP } from '../../js/axios-common';

  export default {

    props: ['selected'],

    data() {
      return {
        data: {},
        errors: [],
      }
    }


  }
</script>
