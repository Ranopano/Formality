<template>
  <div id="app">
    <SiteHeader />
    <div class="container">
      <router-view />
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import SiteHeader from 'app/common/components/headers/SiteHeader.vue';

@Component({
  components: {
    SiteHeader,
  },
})
export default class App extends Vue {}
</script>

<style lang="scss">
.form {
  border: $border-width solid $border-color;
  border-radius: 5px;
}
</style>
