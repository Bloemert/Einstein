<template>
  <div v-html="rawHtml">
  </div>
</template>


<script>
export default {
  data() {
    return {
      rawHtml: '<h1>Hello, world!</h1>' +
       '<ul>' +
       '<li> dotnet core 2.1</li>' +
       '<li> NancyFX as WebAPI / REST Service</li>' +
       '<li> ASP.NET Core 2.1 Serving WebUI</li>' +
        '<li> Vue.js Client framework</li>' +
        '<li> Buefy.js Client Components framework</li>' +
        '<li> Bulmaswatch.js Client Themes library</li>' +
       '<li> Webpack Javascript build library</li>' +
       '</ul>'
    }
  }
}
</script>