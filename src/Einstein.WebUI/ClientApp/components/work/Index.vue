<template>
  <section>
    <nav class="tabs">
      <ul>
        <li>
          <router-link class="nav-item is-tab" to="/work/employees">Employees</router-link>
        </li>
      </ul>
    </nav>

    <router-view name="workView" class="work-content">
      Test
    </router-view>
  </section>
</template>

<script>
  export default {
    name: 'WorkMenu',
    data() {
      return {
        id: this.$route.params.id
      }
    }
  }
</script>
