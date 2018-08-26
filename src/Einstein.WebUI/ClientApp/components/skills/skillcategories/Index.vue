<template>
  <section>
    <skillcategories-table></skillcategories-table>
  </section>
</template>

<script>

  import SkillCategoriesTable from './SkillCategoriesTable.vue';

  export default {
    name: 'SkillCategories',
    data() {
      return {
        id: this.$route.params.id
      }
    },

    components: {
      "skillcategories-table": SkillCategoriesTable
    }
  };
</script>
