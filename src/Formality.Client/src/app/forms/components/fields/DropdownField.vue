<template>
  <ValidationProvider :name="field.name" :rules="rules" v-slot="{ errors }">
    <b-form-group
      v-if="field"
      :label-for="field.name"
      :invalid-feedback="errors[0]"
      :state="errors[0] ? false : null"
    >
      <label>
        <b>{{ field.label }}</b>
      </label>
      <b-form-select
        :id="field.name"
        :name="field.name"
        :options="dropdownOptions"
        :state="errors[0] ? false : null"
        v-model="value"
      />
    </b-form-group>
  </ValidationProvider>
</template>

<script lang="ts">
import { Component } from 'vue-property-decorator';
import { ValidationProvider } from 'vee-validate';
import { FieldBase } from '.';
import { FieldType, FormFieldDto } from '../../models';

@Component({
  components: {
    ValidationProvider,
  },
})
export default class DropdownField extends FieldBase {
  public static match(field: FormFieldDto) {
    return field.type === FieldType.Dropdown;
  }

  private get dropdownOptions() {
    const values = this.field.values || [];
    return [
      this.emptyOption,
      ...values.map(x => ({
        value: x.id?.toString(),
        text: x.value,
      })),
    ];
  }

  private get emptyOption() {
    return {
      value: '',
      text: this.field.placeholder || 'Please select an option',
    };
  }
}
</script>
