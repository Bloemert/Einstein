<template>
  <section>
    <spots-table></spots-table>
  </section>
</template>

<script>

  import SpotsTable from './SpotsTable.vue';

  export default {
    name: 'Spots',
    data() {
      return {
        id: this.$route.params.id
      }
    },

    components: {
      "spots-table": SpotsTable
    }
  }
</script>
