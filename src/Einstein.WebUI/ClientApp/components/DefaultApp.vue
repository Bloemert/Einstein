<template>
  <div class="container box is-fluid">
    <div class="container is-fluid">
      <nav-menu />
    </div>
    <div class="container is-fluid">
      <router-view id="main" class="main-content"></router-view>
    </div>
    <footer class="footer">
        <div class="content has-text-centered">
            <a href="https://github.com/Bloemert/Einstein"><strong>Einstein - Tech Radar</strong></a> by <a href="https://www.bloemert.com/">Bloemert Team</a> and Community!
        </div>
    </footer>
  </div>
</template>

<script>
    import Vue from 'vue';
    import VueRouter from 'vue-router';
    import NavMenu from './NavMenu.vue';

    export default Vue.extend({
        name: 'App',
        data: function() {
            return {
                currentView: 'Home'
            }
        },
        components: {
            'nav-menu': NavMenu
        },
        render(h) {
            return h(this)
        }
    })
</script>

<style lang="scss">
  .footer {
    padding: 3px 3px 3px 3px;
  }

  .main-content {
    height: calc(74vh);
    padding: 15px 15px 15px 15px;
  }

</style>