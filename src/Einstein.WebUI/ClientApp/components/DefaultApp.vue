<template>
  <div class="columns"  style="width:100%;height:100%;">
    <main-menu></main-menu>

    <div class="container box is-fluid column">
      <!--<div class="container is-fluid">
        <nav-menu />
      </div>-->
      <div class="container is-fluid">
        <router-view class="main-content"></router-view>
      </div>
      <footer class="footer">
          <div class="content has-text-centered">
              <a href="https://github.com/Bloemert/Einstein"><strong class="source-link">Einstein - Skill Radar</strong></a> by <a class="contrib-link" href="https://www.bloemert.com/">Bloemert Team</a> and Community!
          </div>
      </footer>
    </div>
  </div>
</template>

<script>
    import VueRouter from 'vue-router';
  import MainMenu from './MainMenu.vue';

  export default Vue.extend({
    name: 'App',
    data: function () {
      return {
        currentView: 'Home'
      };
    },
    components: {
      'main-menu': MainMenu,
    },
    render(h) {
      return h(this);
    }
  });
</script>

<style lang="scss">
  @import '../styles/site.scss';

  html, body {
    height: 100%;
    padding: 0;
    margin: 0;
  }

  .footer {
    padding: 3px 3px 3px 3px;
  }

  .main-content {
    height: calc(100vh - 200px);
    padding: 15px 15px 15px 15px;
  }

  .source-link {
    color: $primary;
  }

  .contrib-link {
    color: $info;
  }

  .menu-back {
    background-color: $primary;
  }


</style>