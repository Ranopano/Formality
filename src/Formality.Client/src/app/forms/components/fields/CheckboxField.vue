<template>
  <b-form-checkbox-group v-if="field" :label-for="field.name">
    <label>
      <b>{{ field.label }}</b>
    </label>
    <b-form-checkbox :id="field.name" :name="field.name" v-model="value" />
  </b-form-checkbox-group>
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
export default class CheckboxField extends FieldBase {
  public static match(field: FormFieldDto) {
    return field.type === FieldType.Checkbox;
  }
}
</script>
