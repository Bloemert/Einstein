<template>
  <q-page class="flex flex-center">
    <div class="row">
      <div class="column">
        <img src="statics/einstein-logo.png" />
      </div>
      <div class="column">
        <div class="row">
          <q-input v-model="form.login" />
          <q-input v-model="form.password"
                   @keyup.enter="submit" />
          <q-btn color="primary" @click="submit">Login</q-btn>
        </div>
      </div>
    </div>
  </q-page>
</template>

<style>
</style>

<script>
export default {
  name: 'PageLogin'
}
</script>

