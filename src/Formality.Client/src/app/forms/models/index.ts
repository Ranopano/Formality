export type FormListDto = {
  id?: number;
  name: string;
};

export type FormDto = {
  id?: number;
  name: string;
  fields: FormFieldDto[];
};

export type FormFieldDto = {
  id?: number;
  name: string;
  label: string;
  placeholder?: string;
  type: FieldType;
  deleted: boolean;
  rules: FormFieldRuleDto[];
  values: FormFieldValueDto[];
};

export type FormFieldRuleDto = {
  id: number;
  fieldId: number;
  type: FieldRule;
  data?: string;
};

export type FormFieldValueDto = {
  id?: number;
  value: string;
};

export enum FormState {
  New = 0,
  Actual = 1,
}

export enum FieldType {
  SingleLineText = 1,
  MultiLineText = 2,
  Checkbox = 3,
  Radio = 4,
  Dropdown = 5,
}

export enum FieldRule {
  Required = 1,
  Length = 2,
}

export type LengthData = {
  minLength?: number;
  maxLength?: number;
};
