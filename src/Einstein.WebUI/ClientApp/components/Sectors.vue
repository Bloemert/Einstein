<template>
  <section>
    Sectors overview with details...

    <sectors-table>

    </sectors-table>
  </section>
</template>

<script>

  import SectorsTable from './sectors/SectorsTable.vue';

  export default {
    name: 'Sectors',
    data() {
      return {
        id: this.$route.params.id
      }
    },

    components: {
      "sectors-table": SectorsTable
    }
  }
</script>
