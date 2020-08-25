<template>
  <div class="forms">
    <transition-group name="fade">
      <div v-if="loading" class="text-center" key="spinner">
        <b-spinner small variant="primary" />
      </div>
      <ul v-else-if="items.length" class="list-group"  key="items">
        <li class="list-group-item" v-for="item in items" :key="item.id">
          <router-link
            title="Submit this form"
            :to="{ name: formSubmitPage, params: { id: item.id } }">
            {{ item.name }}
          </router-link>
          <router-link
            v-if="showEditButton"
            class="edit-button"
            title="Edit this form"
            :to="{ name: formEditPage, params: { id: item.id } }"
          >
            <BIconPencilSquare />
          </router-link>
        </li>
      </ul>
      <small v-else class="text-muted" key="placeholder">
        Nothing yet.
      </small>
    </transition-group>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { Pages } from '@/router/types';
import { BIconPencilSquare } from 'bootstrap-vue';
import { forms, loading } from '../store/getters';

@Component({
  components: {
    BIconPencilSquare,
  },
})
export default class FormList extends Vue {
  @Prop({ default: false })
  public showEditButton!: boolean;

  private get formEditPage() {
    return Pages.FormEdit;
  }

  private get formSubmitPage() {
    return Pages.FormSubmit;
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
