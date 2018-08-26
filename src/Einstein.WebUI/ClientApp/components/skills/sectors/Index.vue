<template>
  <section>
    <sectors-chart></sectors-chart>
    <sectors-table></sectors-table>
  </section>
</template>

<script>

  import SectorsChart from './SectorsChart.vue';
  import SectorsTable from './SectorsTable.vue';

  export default {
    name: 'Sectors',
    data() {
      return {
        id: this.$route.params.id
      }
    },

    components: {
      "sectors-chart": SectorsChart,
      "sectors-table": SectorsTable
    }
  }
</script>
