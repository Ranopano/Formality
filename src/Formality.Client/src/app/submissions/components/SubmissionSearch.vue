<template>
  <div class="submission-search row pt-1 pb-3">
    <div class="col-sm-3">
      <b-input
        type="text"
        debounce="500"
        v-model="keyword"
        placeholder="Type to filter submissions..." />
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator';
import { searchSubmission } from '../store/actions';
import { loading } from '../store/getters';

@Component
export default class SubmissionSearch extends Vue {
  private keyword = '';

  private get loading() {
    return loading(this);
  }

  @Watch('keyword')
  private async onKeywordChange(value: string, previousValue: string) {
    if (value !== previousValue && !this.loading) {
      await this.$store.dispatch(searchSubmission({
        keyword: value,
        maxResults: 10,
        orderBy: [
          { name: 'id', desc: true },
        ],
      }));
    }
  }
}
</script>

<style scoped lang="scss"></style>
