<template>
  <div class="submissions">
    <h4 v-if="header">{{ header }}</h4>
    <transition-group name="fade">
      <div v-if="loading" class="text-center" key="spinner">
        <b-spinner small variant="secondary" />
      </div>
      <div v-else-if="items.length" key="items">
        <b-table bordered :items="items" :fields="fields"></b-table>
      </div>
      <small v-else class="text-muted" key="placeholder">
        {{ placeholder }}
      </small>
    </transition-group>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { SubmissionListDto } from '@/models';
import { submissions, loading } from '../store/getters';

@Component
export default class SubmissionList extends Vue {
  @Prop()
  public header?: string;

  @Prop({ default: 'Nobody here but us chickens, chief.' })
  public placeholder!: string;

  private get fields() {
    return [
      {
        key: 'id',
        label: 'Submission',
      },
      {
        key: 'form.name',
        label: 'Form',
      },
      {
        key: 'createDateTime',
        label: 'Date',
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
