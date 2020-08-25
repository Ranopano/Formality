<template>
  <div class="form-submit" v-if="form">
    <PageHeader :header="header" />
    <div class="form p-3">
      <b-form @submit="onSubmit" @reset="onReset" :id="formId">
        <div v-for="field of form.fields" :key="field.id">
          <FormField :item="field" />
        </div>
      </b-form>
      <div class="buttons pt-3">
        <b-button variant="success" :form="formId" type="submit">
          Submit
        </b-button>
        <b-button variant="warning" :form="formId" type="reset">
          Reset
        </b-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { Pages } from '@/router/types';
import PageHeader from '@/app/common/components/headers/PageHeader.vue';
import { getForm } from '../store/actions';
import { form, formHeader } from '../store/getters';
import FormField from '../components/FormField.vue';

@Component({
  components: {
    PageHeader,
    FormField,
  },
})
export default class FormSubmitPage extends Vue {
  private get form() {
    return form(this);
  }

  private get formId() {
    return `form${this.form?.id}`;
  }

  private get header() {
    return formHeader(this);
  }

  private async created() {
    const id = Number(this.$route.params.id);

    if (!id) {
      return this.$router.replace({ name: Pages.NotFound });
    }

    await this.$store.dispatch(getForm(id));
  }

  private onSubmit(e: Event) {
    e.preventDefault();
  }

  private onReset() {
    // reset
  }
}
</script>

<style lang="scss" scoped>
.form {
  border: $border-width solid $border-color;
  border-radius: 5px;
}
.buttons {
  display: flex;
  justify-content: space-between;
}
</style>
