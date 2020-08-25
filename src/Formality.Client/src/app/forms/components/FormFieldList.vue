<template>
  <div>
    <draggable
      :list="items"
      group="fields"
      :disabled="false"
      class="list-group"
      ghost-class="ghost"
    >
      <transition-group name="no" class="list-group">
        <FormField v-for="item in items" :key="item.name" :item="item" />
      </transition-group>
    </draggable>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import draggable from 'vuedraggable';
import { Component, Prop } from 'vue-property-decorator';
import { FormFieldDto } from '@/models';
import FormField from 'app/forms/components/FormField.vue';

@Component({
  components: {
    draggable,
    FormField,
  },
})
export default class FormFieldList extends Vue {
  @Prop({ default: [], required: true })
  public items!: FormFieldDto[];
}
</script>

<style scoped lang="scss">
</style>
