<template>
  <div v-html="rawHtml">
  </div>
</template>


<script>
export default {
  data() {
    return {
      rawHtml: ''
    }
  }
}
</script>