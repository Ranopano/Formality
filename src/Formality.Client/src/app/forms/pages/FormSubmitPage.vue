<template>
  <div class="form-submit" >
    <PageHeader :header="header" />
    <ContentLoader :showContent="form && submission" :loading="loading">
      <template #spinner>
        <b-spinner small variant="secondary" />
      </template>
      <template #content>
        <div class="form p-3">
          <ValidationObserver v-slot="{ handleSubmit }">
            <b-form
              v-if="form"
              :id="formId"
              @submit.prevent="handleSubmit(onSubmit)"
              @reset="setNewSubmission"
            >
              <div v-if="error" class="text-danger pb-3">{{ error }}</div>
              <FormField
                v-for="field of form.fields"
                :key="field.id"
                :field="field"
                :model="getModel(field)"
              />
            </b-form>
          </ValidationObserver>
          <b-collapse v-if="json" id="request">
            <pre>{{ json }}</pre>
          </b-collapse>
          <div class="buttons pt-3">
            <b-button variant="success" :form="formId" type="submit">
              Submit
            </b-button>
            <b-button v-if="json" v-b-toggle.request>
              Show request
            </b-button>
            <b-button variant="warning" :form="formId" type="reset">
              Reset
            </b-button>
          </div>
        </div>
      </template>
    </ContentLoader>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { Pages } from '@/router/pages';
import PageHeader from 'app/common/components/headers/PageHeader.vue';
import { SubmissionDto, SubmissionValueDto } from 'app/submissions/models';
import { addSubmission } from 'app/submissions/store/actions';
import ContentLoader from 'app/common/components/ContentLoader.vue';
import { debug } from '@/store/getters';
import { ValidationObserver } from 'vee-validate';
import { error as submissionError } from 'app/submissions/store/getters';
import { tryGetForm } from '../store/actions';
import { form, formHeader, loading } from '../store/getters';
import FormField from '../components/FormField.vue';
import { FormFieldDto } from '../models';
import { FormFieldModel } from '../components/fields';
import '../components/fields/validation';

@Component({
  components: {
    ContentLoader,
    PageHeader,
    FormField,
    ValidationObserver,
  },
})
export default class FormSubmitPage extends Vue {
  private submission: SubmissionDto | null = null;

  private get json() {
    return debug(this) ? JSON.stringify(this.submission || {}, null, '  ') : undefined;
  }

  private get error() {
    return submissionError(this);
  }

  private get form() {
    return form(this);
  }

  private get formId() {
    return `form_${this.form?.id}`;
  }

  private get header() {
    return formHeader(this);
  }

  private get loading() {
    return loading(this);
  }

  private async created() {
    await tryGetForm(this);
    this.setNewSubmission();
  }

  private getModel({ id }: FormFieldDto): FormFieldModel | undefined {
    const model = this.submission?.values.find(x => x.fieldId === id);

    if (!model) {
      return undefined;
    }

    return {
      value: model?.value || '',
      onUpdate: value => {
        model.value = value;
      },
    };
  }

  private async onSubmit() {
    if (this.submission === null) {
      return;
    }

    await this.$store.dispatch(addSubmission(this.submission));

    if (this.error) {
      return;
    }

    this.$router.push({ name: Pages.Submissions });
  }

  private setNewSubmission() {
    if (this.form?.id) {
      const { id, fields } = this.form;
      this.submission = {
        formId: id,
        values: fields.map((x): SubmissionValueDto => ({
          fieldId: x.id,
          type: x.type,
          value: '',
        })),
      };
    }
  }
}
</script>

<style lang="scss" scoped>
.buttons {
  display: flex;
  justify-content: space-between;
}
#request {
  @extend .p-3, .my-3;
  background: $app-color-gray-light;
}
</style>
