<template>
  <section>
    Rings overview with details...

    <rings-chart></rings-chart>

    <rings-table></rings-table>
  </section>
</template>

<script>

  import RingsTable from './rings/RingsTable.vue';
  import RingsChart from './rings/RingsChart.vue';

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
