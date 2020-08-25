<template>
  <div class="form-edit">
    <PageHeader :header="header" />
    <div class="row no-gutters">
      <section class="col-sm-9 p-2 form">
        <FormFieldList :items="items" />
      </section>
      <section class="col-sm-3 p-2 fields">
        <FormTypeList />
      </section>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import draggable from 'vuedraggable';
import PageHeader from 'app/common/components/headers/PageHeader.vue';
import FormTypeList from 'app/forms/components/FormTypeList.vue';
import FormFieldList from 'app/forms/components/FormFieldList.vue';
import { FormFieldDto } from '@/models';
import { Pages } from '@/router/types';
import { getForm } from '../store/actions';
import { loading, form, formHeader } from '../store/getters';

@Component({
  components: {
    PageHeader,
    FormTypeList,
    FormFieldList,
    draggable,
  },
})
export default class FormEditPage extends Vue {
  private items: FormFieldDto[] = [];

  private get loading() {
    return loading(this);
  }

  private get form() {
    return form(this);
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
}
</script>

<style scoped lang="scss">
.form {
  border: $border-width solid $border-color;
  border-radius: 5px;
}
</style>
