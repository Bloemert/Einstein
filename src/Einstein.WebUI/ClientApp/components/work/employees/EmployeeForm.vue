<!--

Remarks:
1.  b-field horizontal together with dynamic type setting does not show e.g. is-danger red box!
    Workarround is using extra inner b-field.
2.  vuevalidate (validation lib used) needs the variable path/name used in :value to be the same as in the validators part else validator values won't update correctly!

-->

<template>
  <section class="hero">

    <div v-if="!selected">
      No item selected! <br />
      Select or create one first!
    </div>
    <div v-else>
      <form @submit.prevent="">


        <b-tabs position="is-right" type="is-boxed" class="block" expanded>

          <b-tab-item>
            <template slot="header">
              <i class="fas fa-info-circle" />
              <span>Details</span>
            </template>

            <div class="hero-body">

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
            </div>
          </b-tab-item>

          <b-tab-item>
            <template slot="header">
              <i class="fas fa-user-astronaut" />
              <span>Skill(s)</span>
              <b-tag rounded> {{employeeSkills.length}} </b-tag>
            </template>

            <div class="hero-body" style="height:100%">

              <div class="buttons is-centered">
                <div class="bbar">
                  <b-tooltip class="tooltip" label="Add skill..." position="is-bottom"><button class="button bbar-left is-primary" @click="addSkill()"><i class="fas fa-plus-circle" /></button></b-tooltip>
                </div>
              </div>

              <div v-for="s in employeeSkills">
                <div class="tile is-child is-small tiny-box">
                  <div class="columns">
                    <div class="column has-text-left"><b-tag rounded>{{s.seqno}}</b-tag></div>
                    <div class="column is-6">
                      <multiselect v-model="selectedSkillArray[s.seqno]"
                                   :custom-label="SkillName"
                                   label="id"
                                   track-by="id"
                                   placeholder="Select skill"
                                   :options="filteredSkills">
                      </multiselect>
                    </div>
                    <div class="column is-4">{{s.name}}</div>
                    <div class="column has-text-right">
                      <b-tooltip class="tooltip" label="Remove skill..." position="is-bottom"><button class="button is-small" @click="removeSkill(s.seqno)"><i class="fas fa-minus-circle" /></button></b-tooltip>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </b-tab-item>

        </b-tabs>

      </form>
    </div>

  </section>

</template>

<script>
  import { HTTP } from '../../../js/axios-common';

  import { mapEmployeesActions, mapEmployeesFields, mapEmployeesMultiRowFields } from './store';

  //import { mapManagersActions, mapManagersFields, mapManagersMultiRowFields } from './store';

  import { required, requiredIf, email, sameAs, minLength } from 'vuelidate/lib/validators';

  import { mapSkillsActions, mapSkillsFields, mapSkillsMultiRowFields } from '../../skills/skills/store';

  import Multiselect from 'vue-multiselect';


  export default {

    data() {
      return {
        selectedSeqnoChanged: true,

        employeeSkills: [
          { seqno: 1, name: 'HOI' },
          { seqno: 2, name: 'Hello' },
          { seqno: 3, name: 'Doei' }
        ],

        selectedSkillArray: [],

        filteredSkills: [],
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


      ...mapSkillsActions({
        loadSkillsData: 'loadFromDB'
      }),

      SkillName(skill) {
        if (skill == undefined || skill.name == undefined) {
          return "Select Skill...";
        } else {
          return skill.name + ' (' + skill.seqno + ')';
        }
      },

      ManagerFullName(manager) {
        if (manager == undefined || manager.name == undefined) {
          return "Select Manager...";
        } else {
          return manager.lastname + ', ' + manager.firstname;
        }
      },

      addSkill() {
        this.employeeSkills.push({ seqno: this.employeeSkills.length + 1, name: 'Added...' });
      },

      removeSkill(sq) {
        this.employeeSkills.splice(sq, 1);
      },

      initRelatedData() {

        this.loadSkillsData().then((r) => {
          this.filteredSkills = _chain(this.skills)
            .value();
        });

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


      // Type(s) reference
      ...mapSkillsMultiRowFields(
        { skills: 'rows' }
      ),

      selectedSkill: {
        get: function () {
          var that = this;
          return _find(this.filteredSkills, function (item) {
            return item.id == that.selected.skillId;
          });
        },

        set: function (skill) {
          this.selected.skillId = skill.id;
        }
      },

    },


    mounted: function () {
      this.initRelatedData();

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
    },

    components: {
      Multiselect
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

  .tiny-box {
    background-color: white;
    border-radius: 6px;
    box-shadow: 0 2px 3px rgba(10, 10, 10, 0.1), 0 0 0 1px rgba(10, 10, 10, 0.1);
    color: #4a4a4a;
    display: block;
    padding: 0.15rem; 
  }
</style>