<template>
  <div class="form-field" v-if="component">
    <component :is="component.name" v-bind="{ field, model }"></component>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import draggable from 'vuedraggable';
import { Component, Prop } from 'vue-property-decorator';
import { FormFieldDto } from '../models';
import SingleLineTextField from './fields/SingleLineTextField.vue';
import MultiLineTextField from './fields/MultiLineTextField.vue';
import DropdownField from './fields/DropdownField.vue';
import { FormFieldModel } from './fields';

@Component({
  components: {
    draggable,
    SingleLineTextField,
    MultiLineTextField,
    DropdownField,
  },
})
export default class FormField extends Vue {
  @Prop({ required: true })
  public field!: FormFieldDto;

  @Prop({ required: false })
  public model?: FormFieldModel;

  private readonly components: Component[] = [
    SingleLineTextField,
    MultiLineTextField,
    DropdownField,
  ];

  private get component(): Component | undefined {
    return this.components.find(x => x.match(this.field));
  }
}

type Component = {
  name: string;
  match: (field: FormFieldDto) => boolean;
};
</script>

<style scoped lang="scss">
</style>
