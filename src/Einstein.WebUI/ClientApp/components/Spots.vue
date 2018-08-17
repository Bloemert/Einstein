<template>
  <section>
    Spots overview with details...

    <spots-table>

    </spots-table>
  </section>
</template>

<script>

  import SpotsTable from './spots/SpotsTable.vue';

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
