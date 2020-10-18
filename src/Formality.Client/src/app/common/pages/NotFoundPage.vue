<template>
  <div class="page-not-found">
    <h1>404 - Not Found</h1>
    <p>{{ message }}</p>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class NotFoundPage extends Vue {
  @Prop({ default: 'Page not found.' })
  public message!: string;
}
</script>

<style scoped lang="scss"></style>
