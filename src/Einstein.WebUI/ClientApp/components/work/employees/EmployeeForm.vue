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
        <b-field label="Number" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.employeeNumber)">
              <b-input id="employeeNumber"
                       :value="selected.employeeNumber" @change.native="selected.employeeNumber = $event.target.value"
                       autocomplete="employee-number"
                       @input="onInput($v.selected.employeeNumber, 'employeeNumber', $event)"
                       placeholder="New number">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.employeeNumber.required">Number is required.</span>
            </div>
          </div>
        </b-field>

        <b-field label="Lastname" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.lastname)">
              <b-input id="employeeLastname"
                       :value="selected.lastname" @change.native="selected.lastname = $event.target.value"
                       autocomplete="employee-lastname"
                       @input="onInput($v.selected.lastname, 'lastname', $event)"
                       placeholder="New lastname">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.lastname.required">Lastname is required.</span>
            </div>
          </div>
        </b-field>


        <b-field label="Firstname" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.firstname)">
              <b-input id="employeeFirstname"
                       :value="selected.firstname" @change.native="selected.firstname = $event.target.value"
                       autocomplete="employee-firstname"
                       @input="onInput($v.selected.firstname, 'firstname', $event)"
                       placeholder="New firstname">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.firstname.required">Firstname is required.</span>
            </div>
          </div>
        </b-field>

        <b-field label="Email" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.email)">
              <b-input id="employeeEmail"
                       :value="selected.email" @change.native="selected.email = $event.target.value"
                       autocomplete="employee-email"
                       @input="onInput($v.selected.email, 'email', $event)"
                       placeholder="New email">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.email.required">Email is required!</span>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.email.email">Must be a valid email address!</span>
            </div>
          </div>
        </b-field>


        <b-field label="Employed since" horizontal>
          <b-datepicker :value="selected.employedSince" @change.native="selected.employedSince = $event.target.value"
                        placeholder="Type or select a date..."
                        icon="calendar"
                        :readonly="false">
          </b-datepicker>
          <b-timepicker :value="selected.employedSince" @change.native="selected.employedSince = $event.target.value"
                        placeholder="Type or select a time..."
                        icon="clock"
                        :readonly="false">
          </b-timepicker>
        </b-field>

        <b-field label="Perc. available" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.availabilityPerWeek)">
              <b-input id="availabilityPerWeek"
                       :value="selected.availabilityPerWeek" @change.native="selected.availabilityPerWeek = $event.target.value"
                       autocomplete="employee-availabilityPerWeek"
                       @input="onInput($v.selected.availabilityPerWeek, 'availabilityPerWeek', $event)"
                       placeholder="New percentage">
              </b-input>
            </b-field>
          </div>
        </b-field>

        <b-field label="Function level" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.functionLevel)">
              <b-input id="functionLevel"
                       :value="selected.functionLevel" @change.native="selected.functionLevel = $event.target.value"
                       autocomplete="employee-functionLevel"
                       @input="onInput($v.selected.functionLevel, 'functionLevel', $event)"
                       placeholder="New function Level">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.firstname.required">Function level is required.</span>
            </div>
          </div>
        </b-field>

        <b-field label="Function title" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.functionTitle)">
              <b-input id="functionTitle"
                       :value="selected.functionTitle" @change.native="selected.functionTitle = $event.target.value"
                       autocomplete="employee-functionTitle"
                       @input="onInput($v.selected.functionTitle, 'functionTitle', $event)"
                       placeholder="New function Title">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.firstname.required">Function title is required.</span>
            </div>
          </div>
        </b-field>

        <!--
  <b-field label="Manager" horizontal>
    <multiselect v-model="selectedManager"
                 :custom-label="ManagerFullName"
                 label="id"
                 track-by="id"
                 placeholder="Select manager"
                 :options="filteredManagers">
    </multiselect>
  </b-field>
    -->

      </form>
    </div>

  </section>

</template>

<script>
  import { HTTP } from '../../../js/axios-common';

  import { mapEmployeesActions, mapEmployeesFields, mapEmployeesMultiRowFields } from './store';

  //import { mapManagersActions, mapManagersFields, mapManagersMultiRowFields } from './store';

  import { required, requiredIf, email, sameAs, minLength } from 'vuelidate/lib/validators';


  export default {

    data() {
      return {
        selectedSeqnoChanged: true,

        searchEmployeeterText: '',
        filteredEmployeeters: [],
        filteredManagers: [],

        errors: []
      };
    },

    methods: {
      isFormDirty() {
        return this.$v.form.$anyDirty;
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
      },


      ManagerFullName(manager) {
        if (manager == undefined || manager.name == undefined) {
          return "Select Manager...";
        } else {
          return manager.lastname + ', ' + manager.firstname;
        }
      },

      /*
      initRelatedData() {
        this.loadManagersData().then((r) => {
          this.filteredManagers = _chain(this.managers)
            .sortBy('lastname')
            .value();
        });
      },
      */
    },

    updated: function (par1, par2, par3) {
      this.$nextTick(function () {
        // Code that will run only after the
        // entire view has been re-rendered
        if (this.selectedSeqnoChanged) {
          this.$v.$reset();
          this.selectedSeqnoChanged = false;
        }
      });
    },


    watch: {
      selectedSeqno: function (newSeqno, oldSeqno) {
        if (newSeqno != oldSeqno) {
          this.selectedSeqnoChanged = true;
        }
      }
    },

    computed: {
      // Entity
      ...mapEmployeesFields({
        selectedSeqno: 'selectedSeqno'
      }),

      ...mapEmployeesMultiRowFields(
        { employees: 'rows' }
      ),

      selected: {
        get: function () {
          return this.employees[this.selectedSeqno];
        },

        set: function (employee) {
          this.employees[this.selectedSeqno] = employee;
        }
      },

    },


    mounted: function () {
      this.$v.$reset();
    },


    validations: {
      selected: {
        employeeNumber: {
          required
        },
        firstname: {
          required
        },
        lastname: {
          required
        },
        email: {
          required,
          email
        },
        employedSince: {
        },
        availabilityPerWeek: {
        },
        functionLevel: {
          required
        },
        functionTitle: {
          required
        }
      },

      form: [
        'selected.seqno',
        'selected.employeeNumber',
        'selected.lastname',
        'selected.firstname',
        'selected.email',
        'selected.employedSince',
        'selected.availabilityPerWeek',
        'selected.functionLevel',
        'selected.functionTitle'
      ]
    }
  };
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style lang="scss">
  @import '../../../styles/site.scss';
  
  .form-scrollable {
    height: calc(100vh - ( 360px ));
    overflow-y: scroll;
  }

</style>