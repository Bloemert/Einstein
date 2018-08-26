<template>
  <section>
    <employees-table></employees-table>
  </section>
</template>

<script>

  import EmployeesTable from './EmployeesTable.vue';

  export default {
    name: 'Employees',
    data() {
      return {
        id: this.$route.params.id
      };
    },

    components: {
      "employees-table": EmployeesTable
    }
  };
</script>
