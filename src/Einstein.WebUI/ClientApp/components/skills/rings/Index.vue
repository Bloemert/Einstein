<template>
  <section>
    <rings-chart></rings-chart>
    <rings-table></rings-table>
  </section>
</template>

<script>

  import RingsTable from './RingsTable.vue';
  import RingsChart from './RingsChart.vue';

  export default {
    name: 'Rings',
    data() {
      return {
        id: this.$route.params.id
      }
    },

    components: {
      "rings-table": RingsTable,
      "rings-chart": RingsChart
    }
  }
</script>
