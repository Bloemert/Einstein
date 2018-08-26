<!--

Remarks:
1.  b-field horizontal together with dynamic type setting does not show e.g. is-danger red box!
    Workarround is using extra inner b-field.
2.  vuevalidate (validation lib used) needs the variable path/name used in :value to be the same as in the validators part else validator values won't update correctly!

-->

<template>
  <section class="form-scrollable">

    <div v-if="!selected">
      No item selected! <br/>
      Select or create one first!
    </div>
    <div v-else>
      <form @submit.prevent="">
        <b-field label="Name" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.name)">
              <b-input id="skillName"
                       :value="selected.name" @change.native="selected.name = $event.target.value"
                       autocomplete="skill-name"
                       @input="onInput($v.selected.name, 'name', $event)"
                       placeholder="New name">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.name.required">Name is required.</span>
            </div>
          </div>
        </b-field>

        <b-field label="Description" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.description)">
              <b-input id="skillDescription"
                       type="textarea"
                       :value="selected.description" @change.native="selected.description = $event.target.value"
                       autocomplete="skill-description"
                       @input="onInput($v.selected.description, 'description', $event)"
                       placeholder="New description">
              </b-input>
            </b-field>
            <div>
              <span class="help is-danger" v-if="isFormDirty() && !$v.selected.description.required">Description is required.</span>
            </div>
          </div>
        </b-field>


        <b-field label="Version" horizontal>
          <div class="tile is-vertical">
            <b-field :type="fieldType($v.selected.version)">
              <b-input id="skillVersion"
                       :value="selected.version" @change.native="selected.version = $event.target.value"
                       autocomplete="skill-version"
                       @input="onInput($v.selected.version, 'version', $event)"
                       placeholder="New version">
              </b-input>
            </b-field>
          </div>
        </b-field>


        <b-field label="Type" horizontal>
          <multiselect v-model="selectedType"
                       :custom-label="TypeName"
                       label="id"
                       track-by="id"
                       placeholder="Select Type"
                       :options="filteredTypes">
          </multiselect>
        </b-field>

        <b-field label="Category" horizontal>
          <multiselect v-model="selectedCategory"
                       :custom-label="CategoryName"
                       label="id"
                       track-by="id"
                       placeholder="Select Category"
                       :options="filteredCategories">
          </multiselect>
        </b-field>

      </form>
    </div>

  </section>

</template>

<script>
  import { HTTP } from '../../../js/axios-common';

  import { mapSkillsActions, mapSkillsFields, mapSkillsMultiRowFields } from './store';

  import { mapSkillTypeActions, mapSkillTypeFields, mapSkillTypeMultiRowFields } from '../../skills/skilltypes/store';
  import { mapSkillCategoryActions, mapSkillCategoryFields, mapSkillCategoryMultiRowFields } from '../../skills/skillcategories/store';
  
  import { required, requiredIf, email, sameAs, minLength } from 'vuelidate/lib/validators';

  import Multiselect from 'vue-multiselect';


  export default {

    data() {
      return {
        selectedSeqnoChanged: true,

        filteredTypes: [],
        filteredCategories: [],

        errors: []
      };
    },

    methods: {
      ...mapSkillTypeActions({
        loadTypesData: 'loadFromDB'
      }),

      ...mapSkillCategoryActions({
        loadCategoriesData: 'loadFromDB'
      }),


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

      TypeName(type) {
        if (type == undefined || type.name == undefined) {
          return "Select Type...";
        } else {
          return type.name + ' (' + type.seqno + ')';
        }
      },

      CategoryName(category) {
        if (category == undefined || category.name == undefined) {
          return "Select Sector...";
        } else {
          return category.name + ' (' + category.seqno + ')';
        }
      },


      initRelatedData() {

        this.loadTypesData().then((r) => {
          this.filteredTypes = _chain(this.types)
            .value();
        });

        this.loadCategoriesData().then((r) => {
          this.filteredCategories = _chain(this.categories)
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
      ...mapSkillsFields({
        selectedSeqno: 'selectedSeqno'
      }),

      ...mapSkillsMultiRowFields(
        { skills: 'rows' }
      ),

      selected: {
        get: function () {
          return this.skills[this.selectedSeqno];
        },

        set: function (skill) {
          this.skills[this.selectedSeqno] = skill;
        }
      },


      // Type(s) reference
      ...mapSkillTypeMultiRowFields(
        { types: 'rows' }
      ),

      selectedType: {
        get: function () {
          var that = this;
          return _find(this.filteredTypes, function (item) { return item.id == that.selected.skillTypeId; });
        },

        set: function (type) {
          this.selected.skillTypeId = type.id;
        }
      },


      // Category(s) reference
      ...mapSkillCategoryMultiRowFields(
        { categories: 'rows' }
      ),

      selectedCategory: {
        get: function () {
          var that = this;
          return _find(this.filteredCategories, function (item) { return item.id == that.selected.skillCategoryId; });
        },

        set: function (category) {
          this.selected.skillCategoryId = category.id;
        }
      }

    },


    mounted: function () {
      this.$v.$reset();

      this.initRelatedData();
    },


    validations: {
      selected: {
        name: {
          required
        },
        description: {
          required
        },
        version: {
        }        
      },

      form: ['selected.seqno', 'selected.name', 'selected.description', 'selected.version']
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

</style>