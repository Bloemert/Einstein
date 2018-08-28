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
                        ref="employeesTable"
                        :data="!employees ? [] : employees"
                        :columns="columns"
                        @row-click="onRowClick"
                        :options="options">
          <div slot="filter__id">
            <b-input type="search" class="form-control table-column-filter" v-model="filterId" @input="searchByField('id', $event)" placeholder="Filter by Id"></b-input>
          </div>
          <div slot="filter__employeeNumber">
            <b-input type="search" class="form-control table-column-filter" v-model="filterNumber" @input="searchByField('employeeNumber', $event)" placeholder="Filter by Number"></b-input>
          </div>
          <div slot="filter__lastname">
            <b-input type="search" class="form-control table-column-filter" v-model="filterLastname" @input="searchByField('lastname', $event)" placeholder="Filter by Lastname"></b-input>
          </div>
          <div slot="filter__firstname">
            <b-input type="search" class="form-control table-column-filter" v-model="filterFirstname" @input="searchByField('firstname', $event)" placeholder="Filter by Firstname"></b-input>
          </div>
        </v-client-table>
        <b-pagination :total="!employees ? 0 : employees.length"
                      :current.sync="currentPage"
                      order="is-right"
                      per-page="10"
                      @change="onPageChange">
        </b-pagination>

      </div>
      <div class="column">
        <employee-form></employee-form>
      </div>
    </div>

  </div>
</template>


<script>
  import { ClientTable, Event } from 'vue-tables-2';

  import { mapEmployeesActions, mapEmployeesFields, mapEmployeesMultiRowFields } from './store';


  import EmployeeForm from './EmployeeForm.vue';
  import HTTP from '../../../js/axios-common';

  import { createNamespacedHelpers } from 'vuex';
  const { mapGetters: mapCoreGetters } = createNamespacedHelpers('core');



  export default {
    name: 'EmployeesMasterDetail',

    data() {
      return {
        form: {
          status: {
            action: 'Loading',
            result: 'OK'
          }
        },
        columns: [
          'id',
          'employeeNumber',
          'lastname',
          'firstname'
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
            name: 'employeeNumber',
            callback: function (row, query) {
              return row.employeeNumber.toString().indexOf(query.employeeNumber) !== -1;
            }
            },
            {
              name: 'lastname',
              callback: function (row, query) {
                return row.lastname.indexOf(query.lastname) !== -1;
              }
            },
            {
              name: 'firstname',
              callback: function (row, query) {
                return row.firstname.indexOf(query.firstname) !== -1;
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
        filterNumber: null,
        filterLastname: null,
        filterFirstname: null,

        errors: []
      };
    },


    methods: {
      ...mapEmployeesActions({
        loadData: 'loadFromDB',
        addEmployee: 'addEmployee',
        removeEmployee: 'removeEmployee',
        saveData: 'saveEmployeeChangesToDB'
      }),

      getUIRowBySeqno(seqno) {
        return document.getElementsByClassName('row-seqno-' + seqno)[0];
      },

      refreshCSSClasses() {
        var that = this;

        _forEach(that.employees, function (row) {
          var uiRow = that.getUIRowBySeqno(row.seqno);

          if (uiRow) {
            if (row.status.added) {
              uiRow.classList.add('row-added');
            } else {
              uiRow.classList.remove('row-added');
            }

            if (row.status.removed) {
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
        });
      },

      addRow() {
        this.addEmployee().then(() => {
          this.refreshCSSClasses();
        });
      },

      removeRow() {
        this.removeEmployee(this.employees[this.selectedSeqno]).then(() => {
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
        if (this.$refs.employeesTable && newPage) {
          this.$refs.employeesTable.setPage(newPage);
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

      ...mapEmployeesFields({
        employees: 'rows',
        selectedSeqno: 'selectedSeqno',
        currentPage: 'currentPage',
      })

    },

    watch: {
      selectedSeqno: {
        handler: function (newVal, oldVal) {
          this.refreshCSSClasses();
        }
      }
    },

    components: {
      'employee-form': EmployeeForm
    }

  };

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
