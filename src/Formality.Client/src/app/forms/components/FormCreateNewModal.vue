<template>
  <div>
    <b-modal
      :id="modalId"
      :title="title"
      :cancel-disabled="loading"
      :ok-disabled="loading"
      :no-close-on-backdrop="loading"
      :no-close-on-esc="loading"
      :hide-header-close="loading"
      @ok="onSubmit"
      @show="clear"
      @hidden="clear">
      <div v-if="error" class="text-danger pb-3">{{ error }}</div>
      <form @submit.stop.prevent>
        <b-form-group
          :state="nameState"
          label="Name"
          label-for="name"
          description="Choose a new name for a form."
          invalid-feedback="Name is required"
        >
          <b-input
            id="name"
            type="text"
            :disabled="loading"
            v-model="name"
            :state="nameState"
          />
        </b-form-group>
      </form>
    </b-modal>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Pages } from '@/router/types';
import { createForm } from '../store/actions';
import { error, formId } from '../store/getters';
import { setError } from '../store/mutations';

@Component
export default class FormCreateNewModal extends Vue {
  @Prop({ required: true })
  public modalId!: string;

  private name = '';

  private nameState: boolean | null = null;

  private loading = false;

  private readonly title = 'New Form';

  private clear() {
    this.name = '';
    this.nameState = null;
    this.loading = false;
    this.$store.commit(setError());
  }

  private get error() {
    return error(this);
  }

  private async onSubmit(e: Event) {
    e.preventDefault();

    if (!this.isValid()) {
      return;
    }

    this.loading = true;

    await this.$store.dispatch(createForm(this.name));

    this.loading = false;

    if (this.error) {
      return;
    }

    const id = formId(this).toString();

    this.$router.push({ name: Pages.FormEdit, params: { id } });
  }

  private isValid() {
    this.nameState = this.name?.trim().length > 0;
    return this.nameState;
  }
}
</script>

<style lang="scss" scoped></style>
