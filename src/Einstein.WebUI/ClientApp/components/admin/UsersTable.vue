<template>
  <div>
    <header>
      <div class="buttons is-centered">
        <div class="bbar">
          <b-tooltip label="Save changes..." position="is-bottom"><button class="button bbar-left is-primary" @click="saveChanges()"><fa-icon :icon="iSave" /></button></b-tooltip>
          <b-tooltip label="Refresh..." position="is-bottom"><button class="button bbar-middle is-primary" @click="refresh()"><fa-icon :icon="iRefresh" /></button></b-tooltip>
          <b-tooltip label="Undo latest change..." position="is-bottom"><button class="button bbar-middle is-secondary" @click="undo()"><fa-icon :icon="iUndo" /></button></b-tooltip>
          <b-tooltip label="Redo latest change..." position="is-bottom"><button class="button bbar-middle is-secondary" @click="redo()"><fa-icon :icon="iRedo" /></button></b-tooltip>
          <b-tooltip label="Add new ..." position="is-bottom"><button class="button bbar-middle is-outlined" @click="addRow()"><fa-icon class="iadd" :icon="iAdd" /></button></b-tooltip>
          <b-tooltip label="Remove selected ..." position="is-bottom"><button class="button bbar-right is-outlined" @click="removeRow()"><fa-icon class="iremove" :icon="iRemove" /></button></b-tooltip>
        </div>
      </div>
    </header>

    <div class="columns">
      <div class="column">
        <b-table ref="btable"
                 :data="data"
                 :striped="true"
                 :narrowed="true"
                 :hoverable="true"
                 :selected.sync="selected"
                 size="is-small"
                 focusable>

          <template slot-scope="props">
            <b-table-column field="id" label="ID" width="20" size="is-small">{{ props.row.id }}</b-table-column>
            <b-table-column field="active" label="Active" size="is-small"><b-checkbox v-model="props.row.active" size="is-small"></b-checkbox></b-table-column>
            <b-table-column field="login" label="Login" size="is-small">{{ props.row.login }}</b-table-column>
            <!--<b-table-column field="expireDate" label="Expire date" size="is-small">{{ props.row.expireDate }}</b-table-column>
            <b-table-column field="lastLogin" label="Last login" size="is-small">{{ props.row.lastLogin }}</b-table-column>
            <b-table-column field="failedAttempts" label="Failed Attempt(s)" size="is-small">{{ props.row.failedAttempts }}</b-table-column>
            <b-table-column field="goodLogins" label="Allowed login(s)" size="is-small">{{ props.row.goodLogins }}</b-table-column>-->
          </template>

        </b-table>

      </div>
      <div class="column">
        <user-form v-bind:selected="selected"></user-form>
      </div>
    </div>

    <!--<footer>
      <div class="buttons is-centered">
        <div class="bbar">
          <b-tooltip label="Save changes..." position="is-top" class="is-marginless"><button class="bbar-left button is-primary"><fa-icon :icon="iSave" /></button></b-tooltip>
          <b-tooltip label="Refresh..." position="is-top"  class="is-marginless"><button class="bbar-middle button is-primary"><fa-icon :icon="iRefresh" /></button></b-tooltip>
          <b-tooltip label="Undo latest change..." position="is-top"  class="is-marginless"><button class="bbar-middle button is-secondary"><fa-icon :icon="iUndo" /></button></b-tooltip>
          <b-tooltip label="Redo latest change..." position="is-top"  class="is-marginless"><button class="bbar-middle button is-secondary"><fa-icon :icon="iRedo" /></button></b-tooltip>
          <b-tooltip label="Add new ..." position="is-top"  class="is-marginless"><button class="bbar-middle button is-outlined"><fa-icon class="iadd" :icon="iAdd" /></button></b-tooltip>
          <b-tooltip label="Remove selected ..." position="is-top"  class="is-marginless"><button class="bbar-right button is-outlined"><fa-icon class="iremove" :icon="iRemove" /></button></b-tooltip>
        </div>
      </div>
    </footer>-->

  </div>
</template>


<script>
  import VueFontAwesome from '@fortawesome/vue-fontawesome';
  import { faHdd } from '@fortawesome/fontawesome-free-regular'
  import { faSyncAlt } from '@fortawesome/fontawesome-free-solid'
  import { faUndoAlt } from '@fortawesome/fontawesome-free-solid'
  import { faRedoAlt } from '@fortawesome/fontawesome-free-solid'
  import { faPlusSquare } from '@fortawesome/fontawesome-free-solid'
  import { faMinusSquare } from '@fortawesome/fontawesome-free-solid'

  import undoRedoHistory from './../../UndoRedoHistory.js';

  import UserForm from './UserForm.vue';
  import HTTP from '../../js/axios-common';
  var _ = require('lodash');

  import { createNamespacedHelpers } from 'vuex'
  const { mapState, mapActions } = createNamespacedHelpers('admin')



  export default {
    name: 'UsersMasterDetail',

    props: ['row'],

    data() {
      return {
        localSelected: { empty: true },
        localPreviousRowState: null,
        localCurrentRowState: null,

        errors: []
      }
    },

    methods: {
      ...mapActions({
        loadData: 'loadUsersFromDB',
        addUser: 'addUser',
        removeUser: 'removeUser',
        saveData: 'saveUserChangesToDB'
      }),

      addRow() {
        var arr = this.data;
        var btable = this.$refs.btable;

        this.addUser().then(() => {
          btable.selectRow(arr[arr.length - 1]);
        });
      },

      saveChanges() {

        this.saveData();

        this.$refs.btable.selectRow(this.data[0]);

        undoRedoHistory.clear();
      },

      refresh() {
        this.$refs.btable.selectRow(this.data[0]);

        this.loadData();
      },

      undo() {
        undoRedoHistory.undo();
      },

      redo() {
        undoRedoHistory.redo();
      },

      removeRow() {
        this.removeUser(this.selected);
      }
    },

    created() {
      this.loadData();
    },

    mounted() {
      this.$refs.btable.selectRow(this.data[0]);
    },

    computed: {
      iSave: function () {
        return faHdd;
      },
      iRefresh: function () {
        return faSyncAlt;
      },
      iUndo: function () {
        return faUndoAlt;
      },
      iRedo: function () {
        return faRedoAlt;
      },
      iAdd: function () {
        return faPlusSquare;
      },
      iRemove: function () {
        return faMinusSquare;
      },

      ...mapState({
        data: state => state.usersData.data
      }),

      selected: {
        get: function () {
          return this.localSelected;
        },
        set: function (newValue) {
          this.localPreviousRowState = null;
          this.localSelected = newValue;
          this.localCurrentRowState = _.cloneDeep(this.localSelected);
        }
      },

      rowPreviousState: {
        get: function () {
          return this.localPreviousRowState
        }
      },

      rowState: {
        get: function () {
          return this.localCurrentRowState;
        },
        set: function (newValue) {
          this.localPreviousRowState = this.localCurrentRowState;
          this.localCurrentRowState = _.cloneDeep(newValue);
        }
      }
    },

    components: {
      'fa-icon': VueFontAwesome,
      'user-form': UserForm
    }

  }
</script>


<style type="text/scss">


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
