<template>
  <section>
    <skilltypes-table></skilltypes-table>
  </section>
</template>

<script>

  import SkillTypesTable from './SkillTypesTable.vue';

  export default {
    name: 'SkillTypes',
    data() {
      return {
        id: this.$route.params.id
      };
    },

    components: {
      "skilltypes-table": SkillTypesTable
    }
  }
</script>
