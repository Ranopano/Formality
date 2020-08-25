<template>
  <h2 class="logo p-3">
    <header>{{ header }}</header>
    <small v-if="description" class="text-muted">{{ description }}</small>
  </h2>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class PageHeader extends Vue {
  @Prop({ required: true })
  public header!: string;

  @Prop()
  public description?: string;
}
</script>

<style lang="scss" scoped>
.logo {
  background: #efefef;
  border-radius: 5px;
  text-shadow: 0 1px white;

  header {
    font-weight: bold;
  }
}
</style>
