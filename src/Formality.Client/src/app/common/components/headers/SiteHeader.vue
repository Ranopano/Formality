<template>
  <nav class="navigation">
    <div class="container">
      <header class="header p-3">
        <router-link
          class="link mr-3"
          v-for="route of routes"
          :key="route.name"
          :to="{ name: route.name }"
        >
          <component v-if="route.icon" :is="route.icon.name" />
          <span v-else>{{ route.name }}</span>
        </router-link>
      </header>
    </div>
  </nav>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { BIconHouseDoorFill } from 'bootstrap-vue';
import { Pages } from '@/router/pages';

type RouteDefinition = {
  icon?: Vue.Component;
  name: Pages;
}

@Component({
  components: {
    [BIconHouseDoorFill.name]: BIconHouseDoorFill,
  },
})
export default class SiteHeader extends Vue {
  private get routes(): RouteDefinition[] {
    return [
      { name: Pages.Home, icon: BIconHouseDoorFill },
      { name: Pages.Forms },
      { name: Pages.Submissions },
    ];
  }
}
</script>

<style lang="scss" scoped>
.navigation {
  box-shadow: inset 0 4px $app-color-blue;
}
.header {
  .router-link {
    color: $app-color-blue;
  }

  .router-link-exact-active {
    color: $black;
  }
}
</style>
