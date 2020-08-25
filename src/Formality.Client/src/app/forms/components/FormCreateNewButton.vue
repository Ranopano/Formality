<template>
  <span>
    <FormCreateNewModal :modal-id="modal" />
    <b-button
      v-b-modal="modal"
      variant="primary"
      :size="size"
      :disabled="loading"
    >
      <b>New</b>
    </b-button>
  </span>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import FormCreateNewModal from './FormCreateNewModal.vue';
import { loading } from '../store/getters';

@Component({
  components: {
    FormCreateNewModal,
  },
})
export default class FormCreateNewButton extends Vue {
  private readonly modal = 'create-new-form-modal';

  @Prop()
  public size?: 'sm' | 'lg';

  private get loading() {
    return loading(this);
  }
}
</script>
