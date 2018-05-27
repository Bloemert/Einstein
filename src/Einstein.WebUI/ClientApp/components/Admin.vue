<template>
  <section>
    <nav class="tabs">
      <ul>
        <li>
          <router-link class="nav-item is-tab" to="/admin/users">User(s)</router-link>
        </li>
        <li>
          <router-link class="nav-item is-tab" to="/admin/logs">Log(s)</router-link>
        </li>
      </ul>
    </nav>

    <router-view name="adminView" class="admin-content">
      Test
    </router-view>
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
