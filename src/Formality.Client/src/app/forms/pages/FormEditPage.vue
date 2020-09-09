<template>
  <div class="form-edit">
    <PageHeader :header="header" />
    <div class="row no-gutters">
      <p>To be done.</p>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import draggable from 'vuedraggable';
import PageHeader from 'app/common/components/headers/PageHeader.vue';
import { tryGetForm } from '../store/actions';
import { formHeader } from '../store/getters';
import { FormFieldDto } from '../models';

@Component({
  components: {
    PageHeader,
    draggable,
  },
})
export default class FormEditPage extends Vue {
  private items: FormFieldDto[] = [];

  private get header() {
    return formHeader(this);
  }

  private async created() {
    await tryGetForm(this);
  }
}
</script>
