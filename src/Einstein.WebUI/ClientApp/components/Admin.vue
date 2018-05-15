<template>
  <section>
    <div>{{id}}</div>
  </section>
</template>

<script>
  export default {
    name: 'Admin',
    data() {
      return {
        id: this.$route.params.id
      }
    }
  }
</script>