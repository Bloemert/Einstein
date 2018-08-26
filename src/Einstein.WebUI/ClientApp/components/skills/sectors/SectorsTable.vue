<template>
  <div>
    <header>
      <div class="buttons is-centered">
        <div class="bbar">
          <b-tooltip class="tooltip" label="Save changes..." position="is-bottom"><button class="button bbar-left is-primary" @click="saveChanges()"><i class="far fa-hdd" /></button></b-tooltip>
          <b-tooltip label="Refresh..." position="is-bottom"><button class="button bbar-middle is-primary" @click="refresh()"><i class="fas fa-sync-alt" /></button></b-tooltip>
          <!--
  <b-tooltip label="Undo latest change..." position="is-bottom"><button class="button bbar-middle is-secondary" @click="undo()"><i class="fas fa-undo-alt" /></button></b-tooltip>
  <b-tooltip label="Redo latest change..." position="is-bottom"><button class="button bbar-middle is-secondary" @click="redo()"><i class="fas fa-redo-alt" /></button></b-tooltip>
    -->
          <b-tooltip label="Add new ..." position="is-bottom"><button class="button bbar-middle is-outlined" @click="addRow()"><i class="fas fa-plus-square" /></button></b-tooltip>
          <b-tooltip label="Remove selected ..." position="is-bottom"><button class="button bbar-right is-outlined" @click="removeRow()"><i class="fas fa-minus-square" /></button></b-tooltip>
        </div>
      </div>
    </header>

    <div class="columns">
      <div class="column">

        <v-client-table class="table table-body-scroll"
                        ref="sectorsTable"
                        :data="!sectors ? [] : sectors"
                        :columns="columns"
                        @row-click="onRowClick"
                        :options="options">
          <div slot="filter__id">
            <b-input type="search" class="form-control table-column-filter" v-model="filterId" @input="searchByField('id', $event)" placeholder="Filter by Id"></b-input>
          </div>
          <div slot="filter__name">
            <b-input type="search" class="form-control table-column-filter" v-model="filterName" @input="searchByField('name', $event)" placeholder="Filter by Name"></b-input>
          </div>
        </v-client-table>
        <b-pagination :total="!sectors ? 0 : sectors.length"
                      :current.sync="currentPage"
                      order="is-right"
                      per-page="10"
                      @change="onPageChange">
        </b-pagination>

      </div>
      <div class="column">
        <sector-form></sector-form>
      </div>
    </div>

  </div>
</template>


<script>
  import 'buefy/lib/buefy.css';

  import { ClientTable, Event } from 'vue-tables-2';

  import { mapSectorsActions, mapSectorsFields, mapSectorsMultiRowFields } from './store';


  import SectorForm from './SectorForm.vue';
  import HTTP from '../../../js/axios-common';

  import { createNamespacedHelpers } from 'vuex'
  const { mapGetters: mapCoreGetters } = createNamespacedHelpers('core')



  export default {
    name: 'SectorsMasterDetail',

    data() {
      return {
        form: {
          status: {
            action: 'Loading',
            result: 'OK'
          },
        },
        columns: [
          'id',
          'name',
        ],

        options: {
          useVuex: true,
          filterByColumn: true,
          filterable: [],
          customFilters: [{
            name: 'id',
            callback: function (row, query) {
              return row.id.toString().indexOf(query.id) !== -1;
            }
          },
            {
            name: 'name',
            callback: function (row, query) {
              return row.name.indexOf(query.name) !== -1;
            }
          }
          ],

          perPageValues: [],
          uniqueKey: 'seqno',
          rowClassCallback: function (row) {
            return 'row row-seqno-' + row.seqno;
          }
        },

        filterId: null,
        filterName: null,

        errors: []
      }
    },


    methods: {
      ...mapSectorsActions({
        loadData: 'loadFromDB',
        addSector: 'addSector',
        removeSector: 'removeSector',
        saveData: 'saveSectorChangesToDB'
      }),

      getUIRowBySeqno(seqno) {
        return document.getElementsByClassName('row-seqno-' + seqno)[0];
      },

      refreshCSSClasses() {
        var that = this;

        _forEach(that.sectors, function (row) {
          var uiRow = that.getUIRowBySeqno(row.seqno);

          if (uiRow) {
            if (row.status.added) {
              uiRow.classList.add('row-added');
            } else {
              uiRow.classList.remove('row-added');
            }

            if (row.status.removed ) {
              uiRow.classList.add('row-removed');
            } else {
              uiRow.classList.remove('row-removed');
            }

            if (row.seqno == that.selectedSeqno) {
              uiRow.classList.add('row-selected');
            } else {
              uiRow.classList.remove('row-selected');
            }

          }

        })
      },

      addRow() {
        this.addSector().then(() => {
          this.refreshCSSClasses();
        });
      },

      removeRow() {
        this.removeSector(this.sectors[this.selectedSeqno]).then(() => {
          this.refreshCSSClasses();
        });
      },

      saveChanges() {
        this.saveData();
        this.refreshCSSClasses();
      },

      refresh() {
        this.loadData();
        this.refreshCSSClasses();
      },

      undo() {
      },

      redo() {
      },



      onRowClick: function (uicomponent) {

        if (uicomponent.event) {
          uicomponent.event.preventDefault();
        }

        this.selectedSeqno = uicomponent.row.seqno;
        this.refreshCSSClasses();
      },

      onPageChange: function (newPage) {
        if (this.$refs.sectorsTable && newPage) {
          this.$refs.sectorsTable.setPage(newPage);
        }
      },

      searchByField(field, newValue) {

        Event.$emit('vue-tables.filter::' + field, { [field]: newValue });
      }
    },

    created() {
      this.loadData();
    },

    mounted: function () {
      this.form.status.loaded = true;
    },

    computed: {

      ...mapSectorsFields({
        sectors: 'rows',
        selectedSeqno: 'selectedSeqno',
        currentPage: 'currentPage',
      }),

    },

    components: {
      'sector-form': SectorForm
    }

  }

</script>


<style lang="scss">
@import '../../../styles/site.scss';

  .table tr th:nth-child(1), td:nth-child(1) {
    width: 15%;
  }

  .VuePagination {
    display: none;
  }

  .pagination-link.is-current {
    position: static !important;
    background-color: $primary !important;
    border-color: $primary !important;
  }



  .table-body-scroll tbody {
    display: block;
    height: calc(100vh - (335px + 180px));
    overflow-y: scroll;
  }

  .table-body-scroll thead, tbody tr {
    display: table;
    width: 100%;
    table-layout: fixed;
  }

  .row-selected {
    background-color: $primary;
    color: $primary-invert;
  }

  .row-added td:first-child {
    color: #00c104;
    text-decoration: underline;
  }

  .row-removed td:first-child {
    color: #ff4242;
    text-decoration: line-through;
  }

  .iadd {
    color: green;
  }

  .iremove {
    color: red;
  }


  .bbar .button {
    font-size: $size-small;
    margin-bottom: 0 !important;
  }

  .bbar {
    display: flex;
    margin: 5px 0px 5px 0px;
    padding: 0px 0px;
  }

  .bbar-right {
    font-size: $size-small;
    margin-left: 0px;
    margin-bottom: 0px;
    border-radius: 0px 7px 7px 0px;
  }

  .bbar-middle {
    margin-bottom: 0px;
    font-size: $size-small;
    margin-left: 0px;
    margin-right: 0px;
    border-radius: 0px;
  }

  .bbar-left {
    font-size: $size-small;
    margin-bottom: 0px;
    margin-right: 0px;
    border-radius: 7px 0px 0px 7px;
  }

</style>
