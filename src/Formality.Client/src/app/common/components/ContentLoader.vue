<template>
  <div class="content-loader">
    <transition-group :name="transition">
      <div v-if="loading" class="text-center" key="spinner">
        <slot name="spinner">
          <b-spinner small variant="primary" />
        </slot>
      </div>
      <div v-else-if="showContent" key="content">
        <slot name="content"></slot>
      </div>
      <div v-else key="no-content">
        <small class="text-muted d-block">
          <slot name="placeholder">Nothing yet.</slot>
        </small>
      </div>
    </transition-group>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class ContentLoader extends Vue {
  @Prop({ default: true, required: true })
  public loading!: boolean;

  @Prop({ default: false, required: true })
  public showContent!: boolean;

  @Prop({ default: 'fade', required: false })
  public transition!: 'fade';
}
</script>

<style lang="scss">
.fade-enter-active,
.fade-leave-active {
  transition: all 0.5s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
  height: 0;
}
</style>
