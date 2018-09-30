<template>
  <section class="columns full-page">
    <div class="column is-three-quarters">
      Nieuws, meldingen, grafieken/rapporten etc....
    </div>
    <div class="column is-one-quarter user-actions">
      User specifieke actie(s)
    </div>
  </section>
</template>


<script>
  export default {
    data() {
      return {
        rawHtml: ''
      };
    }
  };
</script>

<style lang="scss">
  @import '../styles/site.scss';

  .user-actions {
    border-left-width: thin;
    border-left-style: solid;
  }

  .full-page {
    height: 100%;
    width: 100%;
  }
</style>
