<template>
  <div class="submissions">
    <PageHeader :header="header" />
    <SubmissionList />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import PageHeader from 'app/common/components/headers/PageHeader.vue';
import { title } from '@/store/getters';
import { searchSubmission } from '../store/actions';
import SubmissionList from '../components/SubmissionList.vue';

@Component({
  components: {
    PageHeader,
    SubmissionList,
  },
})
export default class SubmissionsPage extends Vue {
  private get header() {
    return `${title(this)} — Submissions`;
  }

  async created() {
    await this.$store.dispatch(searchSubmission({
      maxResults: 10,
      orderBy: [
        { name: 'id', desc: true },
      ],
    }));
  }
}
</script>

<style scoped lang="scss">
</style>
