<template>
  <div>
    <draggable
      :list="items"
      :clone="onClone"
      :group="{ name: 'fields', pull: 'clone', put: false }"
      :disabled="false"
      class="list-group"
      ghost-class="ghost"
    >
      <transition-group name="no">
        <div v-for="item in items" :key="item" class="field-type">
            {{ getName(item) }}
        </div>
      </transition-group>
    </draggable>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import draggable from 'vuedraggable';
import { FieldType, FormFieldDto } from '@/models';

let globalId = 1; // TODO: move to the store

@Component({
  components: {
    draggable,
  },
})
export default class FormTypeList extends Vue {
  private get items(): FieldType[] {
    return [
      FieldType.Text,
      FieldType.Textarea,
      FieldType.Dropdown,
      // TODO: the rest
    ];
  }

  private getName(type: FieldType) {
    return FieldType[type];
  }

  private onClone(type: FieldType): FormFieldDto {
    const id = globalId++;
    const name = `${this.getName(type)}_${id}`;
    return {
      name,
      type,
      label: name,
      deleted: false,
      values: [],
    };
  }
}
</script>

<style scoped lang="scss">
.field-type {
  cursor: grab;
}
</style>
