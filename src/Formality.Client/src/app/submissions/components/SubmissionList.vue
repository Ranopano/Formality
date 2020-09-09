<template>
  <div class="submissions">
    <h4 v-if="header">{{ header }}</h4>
    <SubmissionSearch v-if="searchable" />
    <ContentLoader :showContent="items.length" :loading="loading">
      <template #spinner>
        <b-spinner small variant="secondary" />
      </template>
      <template #content>
        <b-table
          bordered
          :items="items"
          :fields="fields"
        >
          <template #cell(id)="data">
            <router-link :to="submissionView(data.value)" class="btn btn-primary btn-sm">
              <b>Open</b>
            </router-link>
          </template>
        </b-table>
      </template>
      <template #placeholder>{{ placeholder }}</template>
    </ContentLoader>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { BvTableField } from 'bootstrap-vue';
import { Pages } from '@/router/pages';
import { utcLocal } from '@/app/utils';
import ContentLoader from 'app/common/components/ContentLoader.vue';
import { submissions, loading } from '../store/getters';
import { SubmissionListDto } from '../models';
import SubmissionSearch from './SubmissionSearch.vue';

type TableField = BvTableField & {
  key: string;
}

@Component({
  components: {
    ContentLoader,
    SubmissionSearch,
  },
})
export default class SubmissionList extends Vue {
  @Prop()
  public header?: string;

  @Prop({ default: 'Nobody here but us chickens, chief.' })
  public placeholder!: string;

  @Prop({ default: false })
  public searchable!: boolean;

  private submissionView(id: number) {
    return { name: Pages.SubmissionView, params: { id } };
  }

  private get fields(): TableField[] {
    return [
      {
        key: 'createDateTime',
        label: 'Date',
        formatter: utcLocal,
      },
      {
        key: 'form.name',
        label: 'Form',
      },
      {
        key: 'id',
        label: 'Actions',
      },
    ];
  }

  private get items(): SubmissionListDto[] {
    return submissions(this);
  }

  private get loading(): boolean {
    return loading(this);
  }
}
</script>

<style scoped lang="scss"></style>
