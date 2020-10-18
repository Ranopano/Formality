<template>
  <div class="submission-view">
    <PageHeader :header="header" />
    <ContentLoader :showContent="form && submission" :loading="loading">
      <template #spinner>
        <b-spinner small variant="secondary" />
      </template>
      <template #content>
        <b-form v-if="submission" class="form p-3">
          <header class="form-header">
            <span v-if="form" class="form-name">{{ form.name }}</span>
            <span v-if="date" class="text-muted">{{ date }}</span>
          </header>
          <hr />
          <b-form-group v-for="value of submission.values" :key="value.id">
            <label>
              <b>{{ getLabel(value.fieldId) }}</b>
            </label>
            <b-input :value="value.value" plaintext />
          </b-form-group>
        </b-form>
      </template>
    </ContentLoader>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import PageHeader from 'app/common/components/headers/PageHeader.vue';
import { tryGetSubmission } from 'app/submissions/store/actions';
import { tryGetForm } from 'app/forms/store/actions';
import { form, loading as loadingForms } from 'app/forms/store/getters';
import { utcLocal } from 'app/utils';
import { title } from '@/store/getters';
import ContentLoader from 'app/common/components/ContentLoader.vue';
import { submission, loading as loadingSubmissions } from '../store/getters';

@Component({
  components: {
    ContentLoader,
    PageHeader,
  },
})
export default class SubmissionViewPage extends Vue {
  private get header() {
    return `${title(this)} — Submissions`;
  }

  private get form() {
    return form(this);
  }

  private get submission() {
    return submission(this);
  }

  private get date() {
    const submissionDate = this.submission?.createDateTime;
    return submissionDate ? utcLocal(submissionDate) : '';
  }

  private get loading() {
    return loadingForms(this) || loadingSubmissions(this);
  }

  private get fields() {
    return this.form?.fields || [];
  }

  private getLabel(fieldId?: number) {
    return this.fields.find(x => x.id === fieldId)?.label || '';
  }

  private async created() {
    await tryGetSubmission(this);
    await tryGetForm(this, this.submission?.formId);
  }
}
</script>

<style scoped lang="scss">
.form-header {
  @extend .d-flex, .justify-content-between, .align-items-center;
}
.form-name {
  font-size: 1.5rem;
  font-weight: bold;
}
</style>
