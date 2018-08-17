<!--

Remarks:
1.  b-field horizontal together with dynamic type setting does not show e.g. is-danger red box!
    Workarround is using extra inner b-field.
2.  vuevalidate (validation lib used) needs the variable path/name used in :value to be the same as in the validators part else validator values won't update correctly!

-->

<template>
  <section class="form-scrollable">

    <form @submit.prevent="">
      <div v-if="isFormDirty()">
        <b-tag :type="!$v.$anyError ? 'is-primary block' : 'is-danger block'">
          Status: {{ selected.status.action }} => {{ selected.status.result }}
        </b-tag>
      </div>

      <b-field label="Name" horizontal>
        <div class="tile is-vertical">
          <b-field :type="fieldType($v.selected.name)">
            <b-input id="spotName"
                     :value="selected.name" @change.native="selected.name = $event.target.value"
                     autocomplete="spot-name"
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
            <b-input id="spotDescription"
                     type="textarea"
                     :value="selected.description" @change.native="selected.description = $event.target.value"
                     autocomplete="spot-description"
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


      <b-field label="Ring" horizontal>
        <multiselect v-model="selectedRing"
                     :custom-label="RingNumberAndName"
                     label="id"
                     track-by="id"
                     placeholder="Select Ring"
                     :options="filteredRings">
        </multiselect>
      </b-field>

      <b-field label="Sector" horizontal>
        <multiselect v-model="selectedSector"
                     :custom-label="SectorNumberAndName"
                     label="id"
                     track-by="id"
                     placeholder="Select Sector"
                     :options="filteredSectors">
        </multiselect>
      </b-field>


      <b-field label="Spotter" horizontal>
        <multiselect v-model="selectedSpotter"
                     :custom-label="fullNameWithLogin"
                     label="id"
                     track-by="id"
                     placeholder="Select User"
                     :options="filteredSpotters">
        </multiselect>
      </b-field>


      <b-field label="Spotted date" horizontal>
        <div class="tile is-vertical">
          <b-datepicker :value="selected.SpottedOn" @change.native="selected.SpottedOn = $event.target.value"
                        placeholder="Type or select a date..."
                        icon="calendar"
                        :readonly="false">
          </b-datepicker>
          <b-timepicker :value="selected.SpottedOn" @change.native="selected.SpottedOn = $event.target.value"
                        placeholder="Type or select a time..."
                        icon="clock"
                        :readonly="false">
          </b-timepicker>
        </div>
      </b-field>


      <b-field label="isNew" horizontal>
        <div class="tile is-vertical">
          <b-field>
            <b-checkbox id="isNew"
                        :value="selected.isNew" @change.native="selected.isNew = $event.target.value">
            </b-checkbox>
          </b-field>
        </div>
      </b-field>


    </form>

  </section>

</template>

<script>
  import { HTTP } from '../../js/axios-common';

  import { mapSpotsActions, mapSpotsFields, mapSpotsMultiRowFields } from './store';
  import { mapUsersActions, mapUsersFields, mapUsersMultiRowFields } from '../admin/store/users';
  import { mapRingsActions, mapRingsFields, mapRingsMultiRowFields } from '../rings/store';
  import { mapSectorsActions, mapSectorsFields, mapSectorsMultiRowFields } from '../sectors/store';

  import { required, requiredIf, email, sameAs, minLength } from 'vuelidate/lib/validators';

  import Multiselect from 'vue-multiselect';


  const touchMap = new WeakMap();

  export default {

    data() {
      return {
        selectedSeqnoChanged: true,

        searchSpotterText: '',
        filteredSpotters: [],
        filteredRings: [],
        filteredSectors: [],

        errors: []
      }
    },

    methods: {
      ...mapUsersActions({
        loadUsersData: 'loadFromDB',
      }),

      ...mapRingsActions({
        loadRingsData: 'loadFromDB',
      }),

      ...mapSectorsActions({
        loadSectorsData: 'loadFromDB',
      }),


      isFormDirty() {
        return this.$v.form.$anyDirty;
      },

      delayTouch($v) {
        $v.$reset()
        if (touchMap.has($v)) {
          clearTimeout(touchMap.get($v))
        }
        touchMap.set($v, setTimeout($v.$touch, 1000))
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
        //this.delayTouch(fieldValidator);
        if (!this.selectedSeqnoChanged) {
          fieldValidator.$touch();
        }
      },

      fieldType(v) {
        return v.$anyDirty ? v.$anyError ? 'is-danger' : 'is-success' : '';
      },

      fullNameWithLogin(user) {
        if (user == undefined || user.lastname == undefined) {
          return "Select User...";
        } else {
          return user.firstname + ' ' + user.lastname + ' (' + user.login + ')';
        }
      },

      RingNumberAndName(ring) {
        if (ring == undefined || ring.name == undefined) {
          return "Select Ring...";
        } else {
          return ring.name + ' (' + ring.seqno + ')';
        }
      },

      SectorNumberAndName(sector) {
        if (sector == undefined || sector.name == undefined) {
          return "Select Sector...";
        } else {
          return sector.name + ' (' + sector.seqno + ')';
        }
      },

      initRelatedData() {
        this.loadUsersData().then((r) => {
          this.filteredSpotters = _chain(this.users)
            .sortBy('lastname')
            .value();
        });

        this.loadRingsData().then((r) => {
          this.filteredRings = _chain(this.rings)
            .sortBy('seqno')
            .value();
        });

        this.loadSectorsData().then((r) => {
          this.filteredSectors = _chain(this.sectors)
            .sortBy('seqno')
            .value();
        });

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
      })
    },


    watch: {
      // whenever question changes, this function will run
      selectedSeqno: function (newSeqno, oldSeqno) {
        if (newSeqno != oldSeqno) {
          this.selectedSeqnoChanged = true;
        }
      }
    },

    computed: {
      // Entity
      ...mapSpotsFields({
        selectedSeqno: 'selectedSeqno',
      }),

      ...mapSpotsMultiRowFields(
        { spots: 'rows' }
      ),

      selected: {
        get: function() {
          return this.spots[this.selectedSeqno];
        },

        set: function (spot) {
          this.spots[this.selectedSeqno] = spot;
        }
      },


      // User(s) reference
      ...mapUsersMultiRowFields(
        { users: 'rows' }
      ),

      selectedSpotter: {
        get: function () {
          var that = this;
          return _find(this.filteredSpotters, function (item) { return item.id == that.selected.spotterId; });
        },

        set: function (user) {
          this.selected.spotterId = user.id;
        }
      },


      // Ring(s) reference
      ...mapRingsMultiRowFields(
        { rings: 'rows' }
      ),

      selectedRing: {
        get: function () {
          var that = this;
          return _find(this.filteredRings, function (item) { return item.id == that.selected.ringId; });
        },

        set: function (ring) {
          this.selected.ringId = ring.id;
        }
      },


      // Sector(s) reference
      ...mapSectorsMultiRowFields(
        { sectors: 'rows' }
      ),

      selectedSector: {
        get: function () {
          var that = this;
          return _find(this.filteredSectors, function (item) { return item.id == that.selected.sectorId; });
        },

        set: function (sector) {
          this.selected.sectorId = sector.id;
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
          required,
          minLength: minLength(4)
        },
        description: {
          required
        },

        spottedOn: {

        },

        isnew: {

        },
      },

      form: ['selected.name', 'selected.description', 'selected.isnew', 'selected.spottedOn'],
    },

    components: {
      Multiselect
    }
  }
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style lang="scss">
  @import '../../styles/site.scss';
  
  .form-scrollable {
    height: calc(100vh - ( 360px ));
    overflow-y: scroll;
  }

</style>