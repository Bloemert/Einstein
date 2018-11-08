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

        <b-field label="TimeStamp" horizontal>
          <div class="tile is-vertical">
            <b-field>
              <b-input id="logTimeStamp"
                       :value="selected.timeStamp">
              </b-input>
            </b-field>
          </div>
        </b-field>

        <b-field label="Level" horizontal>
          <div class="tile is-vertical">
            <b-field>
              <b-input id="logLevel"
                       :value="selected.level">
              </b-input>
            </b-field>
          </div>
        </b-field>

        <b-field label="Message" horizontal>
          <div class="tile is-vertical">
            <b-field>
              <b-input id="logMessage"
                       :value="selected.message">
              </b-input>
            </b-field>
          </div>
        </b-field>

        <b-field label="Exception" horizontal>
          <div class="tile is-vertical">
            <b-field>
              <b-input id="logException"
                       type="textarea"
                       rows="5"
                       @scroll.native="onScroll"
                       :value="selected.exception">
              </b-input>
            </b-field>
          </div>
        </b-field>

        <b-field label="Properties" horizontal>
          <div class="tile is-vertical">
            <b-field>
              <b-input id="logProperties"
                       type="textarea"
                       rows="5"
                       @scroll.native="onScroll"
                       :value="selected.properties">
              </b-input>
            </b-field>
          </div>
        </b-field>


        <b-field label="LogEvent" horizontal>
          <div class="tile is-vertical">
            <b-field>
              <b-input id="logEvent"
                       type="textarea"
                       rows="5"
                       @scroll.native="onScroll"
                       :value="selected.logEvent">
              </b-input>
            </b-field>
          </div>
        </b-field>

      </form>
      </div>

  </section>

</template>

<script>
  import { HTTP } from '../../js/axios-common';

  import { mapLogsActions, mapLogsFields, mapLogsMultiRowFields } from './store/logs';


  export default {

    data() {
      return {
        selectedSeqnoChanged: true,

        errors: []
      }
    },

    methods: {
      ...mapLogsActions({
        isLogUniqueByField: 'isUniqueByField'
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
      ...mapLogsFields({
        selectedSeqno: 'selectedSeqno',
      }),

      ...mapLogsMultiRowFields(
        { logs: 'rows' }
      ),

      selected: {
        get: function() {
          return this.logs[this.selectedSeqno];
        }
      }
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