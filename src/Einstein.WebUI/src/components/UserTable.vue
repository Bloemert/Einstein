<template>
  <q-table title="Users"
           :data="data"
           :columns="columns"
           row-key="id">

    <template slot="top-right" slot-scope="props">
      <q-search hide-underline v-model="filter" />
    </template>

  </q-table>

</template>

<script>
  export default {
    name: 'UserTable',
    data: () => ({
      columns: [
        {
          name: 'lastname',
          required: true,
          label: 'Last name',
          align: 'left',
          field: 'lastname',
          sortable: true,
          //classes: 'class'
          //style: 'width: 500px'
        },
        {
          name: 'firstname',
          required: true,
          label: 'First name',
          align: 'left',
          field: 'lastname',
          sortable: true,
          //classes: 'class'
          //style: 'width: 500px'
        }
    ],
      data: [],
      visibleColumns: ['desc', 'fat', 'carbs', 'protein', 'sodium', 'calcium', 'iron'],
      separator: 'horizontal',
      filter: ''
    })
  }
</script>

