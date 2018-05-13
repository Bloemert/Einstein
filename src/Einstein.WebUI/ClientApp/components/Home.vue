<template>
  <div v-html="rawHtml">
  </div>
</template>


<script>
export default {
  data() {
    return {
      rawHtml: '<h1>Hello, world!</h1>' +
      '<p> Welcome to your new single- page application, built with:</p>' +
       '<ul>' +
       '<li> dotnet core 2.0</li>' +
       '<li> NancyFX as WebAPI / REST Service</li>' +
       '<li> ASP.NET Core 2.0 Serving WebUI</li>' +
       '<li> Vue.js Client Framework</li>' +
       '</ul>'
    }
  }
}
</script>