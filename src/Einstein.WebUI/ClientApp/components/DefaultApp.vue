<template>
    <div class="container box is-fluid is-paddingless">
        <div class="container is-fluid is-marginless">
            <nav-menu />
        </div>
        <div class="container is-fluid is-marginless">
            <router-view></router-view>
        </div>
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
@import "~bulma/sass/utilities/_all";

$primary: #8c67ef;
$primary-invert: findColorInvert($primary);
$twitter: #4099FF;
$twitter-invert: findColorInvert($twitter);

$colors: (
    "white": ($white, $black),
    "black": ($black, $white),
    "light": ($light, $light-invert),
    "dark": ($dark, $dark-invert),
    "primary": ($primary, $primary-invert),
    "info": ($info, $info-invert),
    "success": ($success, $success-invert),
    "warning": ($warning, $warning-invert),
    "danger": ($danger, $danger-invert),
    "twitter": ($twitter, $twitter-invert)
);

$link: $primary;
$link-invert: $primary-invert;
$link-focus-border: $primary;

@import "~bulma";
@import "~buefy/src/scss/buefy";
</style>
