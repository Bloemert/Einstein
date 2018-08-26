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
        <b-field label="Name" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.name)">
              <b-input id="employeeSkillName"
                       :value="selected.name" @change.native="selected.name = $event.target.value"
                       autocomplete="employeeSkill-name"
                       @input="onInput($v.selected.name, 'name', $event)"
                       placeholder="New name">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.name.required">Name is required.</span>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.name.minLength">Name must have at least {{ $v.selected.name.$params.minLength.min }} letters.</span>
            </div>
          </div>
        </b-field>

        <b-field label="Description" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.description)">
              <b-input id="employeeSkillDescription"
                       type="textarea"
                       :value="selected.description" @change.native="selected.description = $event.target.value"
                       autocomplete="employeeSkill-description"
                       @input="onInput($v.selected.description, 'description', $event)"
                       placeholder="New description">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.description.required">Description is required.</span>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.description.email">Description must be valid!</span>
            </div>
          </div>
        </b-field>

      </form>
    </div>

  </section>

</template>

<script>
  import { HTTP } from '../../../js/axios-common';

  import { mapEmployeeSkillsActions, mapEmployeeSkillsFields, mapEmployeeSkillsMultiRowFields } from './store';

  import { required, requiredIf, email, sameAs, minLength } from 'vuelidate/lib/validators';


  export default {

    data() {
      return {
        selectedSeqnoChanged: true,

        searchEmployeeSkillterText: '',
        filteredEmployeeSkillters: [],

        errors: []
      }
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
      ...mapEmployeeSkillsFields({
        selectedSeqno: 'selectedSeqno',
      }),

      ...mapEmployeeSkillsMultiRowFields(
        { employeeSkills: 'rows' }
      ),

      selected: {
        get: function() {
          return this.employeeSkills[this.selectedSeqno];
        },

        set: function (employeeSkill) {
          this.employeeSkills[this.selectedSeqno] = employeeSkill;
        }
      },

    },


    mounted: function () {
      this.$v.$reset();
    },


    validations: {
      selected: {
        name: {
          required,
          minLength: minLength(4)
        },
        description: {
        },
      },

      form: ['selected.seqno', 'selected.name', 'selected.description'],
    }
  }
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style lang="scss">
  @import '../../../styles/site.scss';
  
  .form-scrollable {
    height: calc(100vh - ( 360px ));
    overflow-y: scroll;
  }

</style>