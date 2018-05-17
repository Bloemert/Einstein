<template>
  <div>
    <header>
      <div class="buttons is-centered">
        <div class="bbar">
          <b-tooltip label="Save changes..." position="is-bottom"><button class="button bbar-left is-primary"><fa-icon :icon="iSave" /></button></b-tooltip>
          <b-tooltip label="Refresh..." position="is-bottom"><button class="button bbar-middle is-primary"><fa-icon :icon="iRefresh" /></button></b-tooltip>
          <b-tooltip label="Undo latest change..." position="is-bottom"><button class="button bbar-middle is-secondary"><fa-icon :icon="iUndo" /></button></b-tooltip>
          <b-tooltip label="Redo latest change..." position="is-bottom"><button class="button bbar-middle is-secondary"><fa-icon :icon="iRedo" /></button></b-tooltip>
          <b-tooltip label="Add new ..." position="is-bottom"><button class="button bbar-middle is-outlined" @click="addRow()"><fa-icon class="iadd" :icon="iAdd" /></button></b-tooltip>
          <b-tooltip label="Remove selected ..." position="is-bottom"><button class="button bbar-right is-outlined"><fa-icon class="iremove" :icon="iRemove" /></button></b-tooltip>
        </div>
      </div>
    </header>

    <div class="columns">
      <div class="column">
        <b-table :data="data"
                 :striped="true"
                 :narrowed="true"
                 :hoverable="true"
                 :selected.sync="selected"
                 size="is-small"
                 focusable>

          <template slot-scope="props">
            <b-table-column field="id" label="ID" width="20" size="is-small">{{ props.row.id }}</b-table-column>
            <b-table-column field="dirty" label="Dirty" ><b-checkbox v-model="props.row.dirty" size="is-small"></b-checkbox></b-table-column>
            <b-table-column field="active" label="Active" size="is-small"><b-checkbox v-model="props.row.active" size="is-small"></b-checkbox></b-table-column>
            <b-table-column field="login" label="Login" size="is-small">{{ props.row.login }}</b-table-column>
            <b-table-column field="expireDate" label="Expire date" size="is-small">{{ props.row.expireDate }}</b-table-column>
            <b-table-column field="lastLogin" label="Last login" size="is-small">{{ props.row.lastLogin }}</b-table-column>
            <b-table-column field="failedAttempts" label="Failed Attempt(s)" size="is-small">{{ props.row.failedAttempts }}</b-table-column>
            <b-table-column field="goodLogins" label="Allowed login(s)" size="is-small">{{ props.row.goodLogins }}</b-table-column>
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

  import UserForm from './UserForm.vue';
  import HTTP from '../../js/axios-common';



  export default {
    name: 'UsersMasterDetail',

    props: ['row'],

    data() {
      return {
        data: [],
        selected: { empty: true },
        errors: []
      }
    },

    methods: {
      addRow() {
        HTTP.get('/users/new.json')
          .then(response => {
            // JSON responses are automatically parsed.
            this.data.push(response.data.data);
          })
          .catch(e => {
            this.errors.push(e)
          });
        
      },

      removeRow() {

      }

    },

    created() {
      HTTP.get('/users/list.json')
        .then(response => {
          // JSON responses are automatically parsed.
          this.data = response.data.data;
        })
        .catch(e => {
          this.errors.push(e)
        });
    },

    //watch: {
    //  '$props': {
    //    handler: function (val, oldVal) {
    //      console.log('watch', val);
    //    },
    //    deep: true
    //  }
    //},

    mounted() {
      console.log(this.data.dirty);
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
