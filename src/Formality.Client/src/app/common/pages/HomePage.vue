<template>
  <div class="home">
    <PageHeader :header="title" description="A dummy project to build web forms and stuff" />
    <div class="row no-gutters">
      <section class="col-sm-9 p-2">
        <SubmissionList
          header="Latest submissions"
          no-items="Nobody here but us chickens, chief."
        />
      </section>
      <section class="col-sm-3 p-2">
        <h4 class="forms-header">
          <span>Forms</span>
          <FormCreateNewButton size="sm" />
        </h4>
        <FormList />
      </section>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import FormList from 'app/forms/components/FormList.vue';
import FormCreateNewButton from 'app/forms/components/FormCreateNewButton.vue';
import SubmissionList from 'app/submissions/components/SubmissionList.vue';
import PageHeader from 'app/common/components/headers/PageHeader.vue';
import { searchForm } from '@/app/forms/store/actions';
import { searchSubmission } from '@/app/submissions/store/actions';
import { title } from '@/store/getters';

@Component({
  components: {
    PageHeader,
    FormList,
    FormCreateNewButton,
    SubmissionList,
  },
})
export default class HomePage extends Vue {
  private get title() {
    return title(this);
  }

  private async created() {
    const query = {
      maxResults: 10,
      orderBy: [
        { name: 'id', desc: true },
      ],
    };

    const tasks = [
      this.$store.dispatch(searchForm(query)),
      this.$store.dispatch(searchSubmission(query)),
    ];

    await Promise.all(tasks);
  }
}
</script>

<style lang="scss" scoped>
.forms-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
</style>
