<template>
  <section>
    <nav class="tabs">
      <ul>
        <li>
          <router-link class="nav-item is-tab" to="/skills/skills">Skill(s)</router-link>
        </li>
        <li>
          <router-link class="nav-item is-tab" to="/skills/types">Type(s)</router-link>
        </li>
        <li>
          <router-link class="nav-item is-tab" to="/skills/categories">Category('s)</router-link>
        </li>
        <li>
          <router-link class="nav-item is-tab" to="/skills/rings">Rings</router-link>
        </li>
        <li>
          <router-link class="nav-item is-tab" to="/skills/sectors">Sectors</router-link>
        </li>
        <li>
          <router-link class="nav-item is-tab" to="/skills/spots">Spots</router-link>
        </li>
      </ul>
    </nav>

    <router-view name="skillsView" class="skills-content">
      Test
    </router-view>
  </section>
</template>

<script>
  export default {
    name: 'SkillsMenu',
    data() {
      return {
        id: this.$route.params.id
      }
    }
  }
</script>
