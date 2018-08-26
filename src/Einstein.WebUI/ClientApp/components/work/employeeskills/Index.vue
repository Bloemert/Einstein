<template>
  <section>
    <employeeskills-table></employeeskills-table>
  </section>
</template>

<script>

  import EmployeeSkillsTable from './EmployeeSkillsTable.vue';

  export default {
    name: 'EmployeeSkills',
    data() {
      return {
        id: this.$route.params.id
      };
    },

    components: {
      "employeeskills-table": EmployeeSkillsTable
    }
  };
</script>
