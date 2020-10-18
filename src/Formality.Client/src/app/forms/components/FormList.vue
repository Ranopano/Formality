<template>
  <div class="forms">
    <ContentLoader :showContent="items.length" :loading="loading">
      <template #content>
        <ul class="list-group">
          <li class="list-group-item" v-for="item in items" :key="item.id">
            <router-link title="Submit this form" :to="formSubmitPage(item)">
              {{ item.name }}
            </router-link>
            <router-link
              v-if="showEditButton"
              class="edit-button"
              title="Edit this form"
              :to="formEditPage(item)"
            >
              <BIconPencilSquare />
            </router-link>
          </li>
        </ul>
      </template>
    </ContentLoader>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { Pages } from '@/router/pages';
import { BIconPencilSquare } from 'bootstrap-vue';
import ContentLoader from 'app/common/components/ContentLoader.vue';
import { forms, loading } from '../store/getters';
import { FormListDto } from '../models';

@Component({
  components: {
    BIconPencilSquare,
    ContentLoader,
  },
})
export default class FormList extends Vue {
  @Prop({ default: false, required: false })
  public showEditButton?: boolean;

  private formEditPage({ id }: FormListDto) {
    return { name: Pages.FormEdit, params: { id } };
  }

  private formSubmitPage({ id }: FormListDto) {
    return { name: Pages.FormSubmit, params: { id } };
  }

  private get items() {
    return forms(this);
  }

  private get loading() {
    return loading(this);
  }
}
</script>

<style scoped lang="scss">
.edit-button {
  float: right;
}
</style>
