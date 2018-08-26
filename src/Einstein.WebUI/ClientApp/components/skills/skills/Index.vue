<template>
  <section>
    <skills-table></skills-table>
  </section>
</template>

<script>

  import SkillsTable from './SkillsTable.vue';

  export default {
    name: 'Skills',
    data() {
      return {
        id: this.$route.params.id
      };
    },

    components: {
      "skills-table": SkillsTable
    }
  };
</script>
