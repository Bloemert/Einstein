<template>
  <div>
    <logs-table></logs-table>
  </div>
</template>

<script>
  import LogsTable from './LogsTable.vue';

  export default {
    name: 'Logs',

    components: {
      'logs-table': LogsTable
    }

  }
</script>



