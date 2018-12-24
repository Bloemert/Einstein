<template>
  <q-layout view="lHh Lpr lFf">
    <q-layout-header>
      <q-toolbar color="primary"
                 :glossy="$q.theme === 'mat'"
                 :inverted="$q.theme === 'ios'">
        <q-btn flat
               dense
               round
               @click="leftDrawerOpen = !leftDrawerOpen"
               aria-label="Menu">
          <q-icon name="menu" />
        </q-btn>

        <q-btn flat
               dense
               round
               @click="doLogInOut()"
               :aria-label="authenticatedUserId!=null ? 'Login' : 'Logout'">
        </q-btn>

        <q-toolbar-title>
          Skill tracking, CV generator and more...
        </q-toolbar-title>
      </q-toolbar>
    </q-layout-header>
    <q-layout-drawer v-model="leftDrawerOpen"
                     :content-class="$q.theme === 'mat' ? 'bg-grey-2' : null">
      <q-item to="/">
        <img src="statics/einstein-logo.png" />
        <label class="text-weight-bold">Einstein</label>
      </q-item>

      <q-item to="/">
        <q-item-side icon="home" />
        <q-item-main label="Home" />
      </q-item>
      <q-list no-border
              link
              inset-delimiter>
        <q-list-header>Me as</q-list-header>
        <q-collapsible label="Me as" sublabel="Info about yourself...">

          <q-item to="/me/user">
            <q-item-side icon="account_box" />
            <q-item-main label="User" sublabel="Change user settings like password..." />
          </q-item>
          <q-item to="/me/employee">
            <q-item-side icon="work" />
            <q-item-main label="Employee" sublabel="Change employee settings like profile info..." />
          </q-item>
        </q-collapsible>
      </q-list>

      <q-list no-border
              link
              inset-delimiter>
        <q-list-header>Me as Manager of</q-list-header>
        <q-collapsible label="Me as Manager of" sublabel="Info about people working for you...">

          <q-item to="/manager/employees">
            <q-item-side icon="group_work" />
            <q-item-main label="Employee(s)" sublabel="Employee profiles, skills..." />
          </q-item>
        </q-collapsible>
      </q-list>

      <q-list no-border
              link
              inset-delimiter>
        <q-list-header>Me as HR Manager for</q-list-header>
        <q-collapsible label="Me as HR Manager for">

          <q-item to="/hrm/employees">
            <q-item-side icon="group" />
            <q-item-main label="Employee(s)" sublabel="Employee profiles, skills..." />
          </q-item>

        </q-collapsible>
      </q-list>

      <q-list no-border
              link
              inset-delimiter>
        <q-list-header>Me as Administrator for</q-list-header>
        <q-collapsible label="Me as Administrator of">

          <q-item to="/admin/users">
            <q-item-side icon="group" />
            <q-item-main label="User(s)" sublabel="Authentication and Authorisation..." />
          </q-item>

        </q-collapsible>
      </q-list>

    </q-layout-drawer>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>


<script lang="ts">
  import Vue from 'vue'
  import { mapState } from 'vuex'
  import Component from 'vue-class-component'
  import {
    State,
    Getter,
    Action,
    Mutation,
    namespace
  } from 'vuex-class'
  import { Guid } from "guid-typescript";
  import VueRouter from 'vue-router';
  import Quasar from 'quasar';
  import { Platform } from 'quasar';

  const userModule = namespace('user')

  Vue.use(VueRouter)
  Vue.use(Quasar)

  @Component({

    computed: mapState('user', ['activeUserId'])

  })
  export default class MainLayout extends Vue {
    // initial data
    name: string = 'MainLayout'

    leftDrawerOpen: boolean = Platform.is.desktop

    // computed
    activeUserId!: Guid


    // method(s)
    doLogInOut(): void {
      if (this.activeUserId == null || this.activeUserId === undefined) {
        this['router'].push('/user/login')
      }
      else {
        this['router'].push('/user/logout')
      }
    }
  }
</script>


<style>
</style>
