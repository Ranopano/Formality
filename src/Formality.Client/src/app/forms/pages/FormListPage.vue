<template>
  <div class="submissions">
    <PageHeader :header="header" />
    <b-button-group class="mt-1 mb-3">
      <FormCreateNewButton />
    </b-button-group>
    <FormList :show-edit-button="true" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import PageHeader from 'app/common/components/headers/PageHeader.vue';
import FormList from 'app/forms/components/FormList.vue';
import { title } from '@/store/getters';
import { searchForm } from '../store/actions';
import FormCreateNewButton from '../components/FormCreateNewButton.vue';

@Component({
  components: {
    FormCreateNewButton,
    PageHeader,
    FormList,
  },
})
export default class FormListPage extends Vue {
  private get header() {
    return `${title(this)} — Forms`;
  }

  private async created() {
    await this.$store.dispatch(searchForm({ maxResults: 10 }));
  }
}
</script>

<style scoped lang="scss"></style>
