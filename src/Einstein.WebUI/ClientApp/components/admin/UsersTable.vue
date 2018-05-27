<template>
  <div>
    <header>
      <div class="buttons is-centered">
        <div class="bbar">
          <b-tooltip label="Save changes..." position="is-bottom"><button class="button bbar-left is-primary" @click="saveChanges()"><i class="far fa-hdd" /></button></b-tooltip>
          <b-tooltip label="Refresh..." position="is-bottom"><button class="button bbar-middle is-primary" @click="refresh()"><i class="fas fa-sync-alt" /></button></b-tooltip>
          <b-tooltip label="Undo latest change..." position="is-bottom"><button class="button bbar-middle is-secondary" @click="undo()"><i class="fas fa-undo-alt" /></button></b-tooltip>
          <b-tooltip label="Redo latest change..." position="is-bottom"><button class="button bbar-middle is-secondary" @click="redo()"><i class="fas fa-redo-alt" /></button></b-tooltip>
          <b-tooltip label="Add new ..." position="is-bottom"><button class="button bbar-middle is-outlined" @click="addRow()"><i class="fas fa-plus-square" /></button></b-tooltip>
          <b-tooltip label="Remove selected ..." position="is-bottom"><button class="button bbar-right is-outlined" @click="removeRow()"><i class="fas fa-minus-square" /></button></b-tooltip>
        </div>
      </div>
    </header>

    <b-tag :type="form.status.result == 'OK' ? 'is-primary block' : 'is-danger block'">
      Status: {{ form.status.action }} => {{ form.status.result }}
    </b-tag>

    <div class="columns">
      <div class="column">

        <v-client-table class="table table-body-scroll"
                        ref="usersTable"
                        :data="users"
                        :columns="columns"
                        @row-click="onRowClick"
                        :options="options">
          <div slot="filter__id">
            <b-input type="search" class="form-control table-column-filter" v-model="filterId" @input="searchByField('id', $event)" placeholder="Filter by id"></b-input>
          </div>
          <div slot="filter__login">
            <b-input type="search" class="form-control table-column-filter" v-model="filterLogin" @input="searchByField('login', $event)" placeholder="Filter by login"></b-input>
          </div>
        </v-client-table>
        <b-pagination :total="users.length"
                      :current.sync="currentPage"
                      order="is-right"
                      per-page="10"
                      @change="onPageChange">
        </b-pagination>

      </div>
      <div class="column">
        <user-form></user-form>
      </div>
    </div>

  </div>
</template>


<script>
  import 'buefy/lib/buefy.css';

  import { ServerTable, ClientTable, Event } from 'vue-tables-2';

  import { mapUsersActions, mapUsersFields, mapUsersMultiRowFields } from './store/users';


  import UserForm from './UserForm.vue';
  import HTTP from '../../js/axios-common';

  import { createNamespacedHelpers } from 'vuex'
  const { mapGetters: mapCoreGetters } = createNamespacedHelpers('core')



  export default {
    name: 'UsersMasterDetail',

    data() {
      return {
        form: {
          status: {
            action: 'Loading',
            result: 'OK'
          },

          isdirty: false
        },
        columns: ['id', 'login'],
        options: {
          filterByColumn: true,
          filterable: [],
          customFilters: [{
            name: 'id',
            callback: function (row, query) {
              return row.id.toString().indexOf(query.id) !== -1;
            }
          },
          {
            name: 'login',
            callback: function (row, query) {
              return row.login.indexOf(query.login) !== -1;
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
        filterLogin: null,

        errors: []
      }
    },


    methods: {
      ...mapUsersActions({
        loadData: 'loadFromDB',
        //addUser: 'addUser',
        //removeUser: 'removeUser',
        //saveData: 'saveUserChangesToDB'
      }),

      addRow() {
        //var arr = this.data;
        //var btable = this.$refs.btable;

        //this.addUser().then(() => {
        //  btable.selectRow(arr[arr.length - 1]);
        //});
      },

      saveChanges() {

        //this.saveData();

        //this.$refs.btable.selectRow(this.data[0]);
      },

      refresh() {
        //this.$refs.btable.selectRow(this.data[0]);

        //this.loadData();
      },

      undo() {
      },

      redo() {
      },

      removeRow() {
        //this.removeUser(this.selected);
      },

      onRowClick: function (uicomponent) {

        this.form.status = { action: 'Selecting', result: 'OK' };

        if (uicomponent.event) {
          uicomponent.event.preventDefault();
        }

        if (this.selectedUser && this.selectedUser.status.result != 'OK') {
          this.$dialog.alert({
            title: 'Notification',
            message: 'Please leave the updated form in a valid state or click refresh to undo your changes!'
          });
        } else {
          if (this.selectedUser) {
            var oldUIRow = document.getElementsByClassName('row-seqno-' + this.selectedSeqno)[0];

            if (oldUIRow) {
              oldUIRow.classList.remove('row-selected');
            }
          }

          uicomponent.event.currentTarget.classList.add('row-selected');

          this.selectedSeqno = uicomponent.row.seqno;
        }

        this.form.status = { action: 'Selected', result: 'OK' };
      },

      onPageChange: function (newPage) {
        this.$refs.usersTable.setPage(newPage);
      },

      searchByField(field, newValue) {

        Event.$emit('vue-tables.filter::' + field, { [field]: newValue });
      }
    },

    created() {
      this.loadData();
    },

    mounted: function () {
      this.form.status = { action: 'Loaded', result: 'OK' };
    },

    computed: {
      //...mapUsersState(
      //  {
      //    //users: state => state.userRows,
      //    currentSeqno: state => state.userCurrentSeqno,
      //    currentPage: state => state.userCurrentPage
      //  }
      //),

      ...mapUsersFields({
        users: 'rows',
        selectedSeqno: 'selectedSeqno',
        selectedUser: 'selectedUser',
        currentPage: 'currentPage'
      }),

      //...mapUsersMultiRowFields(
      //),
      //...mapCoreGetters(['getAppSettings']),
    },

    components: {
      'user-form': UserForm
    }

  }




</script>


<style type="text/scss">

  .table tr th:nth-child(1), td:nth-child(1) {
    width: 20%;
  }

  .VuePagination {
    display: none;
  }

  .table-body-scroll tbody {
    display: block;
    height: calc(100vh - (335px + 140px));
    overflow-y: scroll;
  }

  .table-body-scroll thead, tbody tr {
    display: table;
    width: 100%;
    table-layout: fixed;
  }

  .row-selected {
    background-color: #8c67ef;
    color: white;
  }

  .iadd {
    color: green;
  }

  .iremove {
    color: red;
  }


  .bbar .button {
    margin-bottom: 0px;
  }

  .bbar {
    display: flex;
    margin: 5px 0px 5px 0px;
    padding: 0px 0px;
    border-radius: 7px;
    box-shadow: 2px 2px 2px #aaaaaa;
  }

  .bbar-right {
    font-size: 0.75rem;
    margin-left: 0px;
    margin-bottom: 0px;
    border-radius: 0px 7px 7px 0px;
  }

  .bbar-middle {
    margin-bottom: 0px;
    font-size: 0.75rem;
    margin-left: 0px;
    margin-right: 0px;
    border-radius: 0px;
  }

  .bbar-left {
    font-size: 0.75rem;
    margin-bottom: 0px;
    margin-right: 0px;
    border-radius: 7px 0px 0px 7px;
  }
</style>
