<template>
  <div class="home">
    <PageHeader
      :header="title"
      description="A dummy project to build web forms and stuff"
    />
    <div class="row no-gutters">
      <section class="col-sm-9 p-2">
        <SubmissionList header="Latest submissions" />
      </section>
      <section class="col-sm-3 p-2">
        <h4>
          <span>Forms</span>
        </h4>
        <FormList />
      </section>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import FormList from 'app/forms/components/FormList.vue';
import SubmissionList from 'app/submissions/components/SubmissionList.vue';
import PageHeader from 'app/common/components/headers/PageHeader.vue';
import { FormState } from 'app/forms/models';
import { searchForm } from 'app/forms/store/actions';
import { searchSubmission } from 'app/submissions/store/actions';
import { title } from '@/store/getters';
import { mapGetters } from 'vuex';

@Component({
  components: {
    PageHeader,
    FormList,
    SubmissionList,
  },
  computed: {
    ...mapGetters([title.name]),
  },
})
export default class HomePage extends Vue {
  private title!: string;

  private async created() {
    const query = {
      maxResults: 10,
      orderBy: [{ name: 'id', desc: true }],
    };

    const tasks = [
      this.$store.dispatch(searchForm({ ...query, stateId: FormState.Actual })),
      this.$store.dispatch(searchSubmission(query)),
    ];

    await Promise.all(tasks);
  }
}
</script>
