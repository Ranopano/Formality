<template>
  <div class="form-field" v-if="item">
    <b-form-group v-if="isText" :label="item.label" :label-for="item.name">
      <b-form-input
        :id="item.name"
        :name="item.name"
        :placeholder="item.placeholder"
      />
    </b-form-group>
    <b-form-group v-if="isTextarea" :label="item.label" :label-for="item.name">
      <b-form-textarea
        :id="item.name"
        :name="item.name"
        :placeholder="item.placeholder"
      />
    </b-form-group>
    <b-form-group v-if="isDropdown" :label="item.label" :label-for="item.name">
      <b-form-select
        :id="item.name"
        :name="item.name"
        :options="dropdownOptions"
      />
    </b-form-group>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import draggable from 'vuedraggable';
import { Component, Prop } from 'vue-property-decorator';
import { FormFieldDto, FieldType } from '@/models';

@Component({
  components: {
    draggable,
  },
})
export default class FormField extends Vue {
  @Prop({ required: true })
  public item?: FormFieldDto;

  private get isText() {
    return this.item?.type === FieldType.Text;
  }

  private get isTextarea() {
    return this.item?.type === FieldType.Textarea;
  }

  private get isDropdown() {
    return this.item?.type === FieldType.Dropdown;
  }

  private get dropdownOptions() {
    const values = this.item?.values || [];
    // const placeholder = { value: null, text: this.item?.placeholder || 'Please select an option' };
    return [
      // placeholder,
      ...values.map(x => ({
        value: x.id,
        text: x.value,
      })),
    ];
  }
}
</script>

<style scoped lang="scss">
</style>
