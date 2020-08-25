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
.fade-enter-active, .fade-leave-active {
  transition: all 0.5s;
  height: auto;
}
.fade-enter, .fade-leave-to {
  opacity: 0;
  height: 0;
}
</style>
