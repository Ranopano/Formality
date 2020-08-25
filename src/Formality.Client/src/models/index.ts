// TODO: generate those types from the actual dtos

export type SearchQueryDto = {
  keyword?: string;
  orderBy?: OrderDto[];
  maxResults?: number;
};

export type OrderDto = {
  name: string;
  desc?: boolean;
}

export type NamedEntityDto = {
  id?: number;
  name?: string;
}

export type FormListDto = {
  id?: number;
  name: string;
}

export type FormDto = {
  id?: number;
  name: string;
  fields: FormFieldDto[];
}

export type FormFieldDto = {
  id?: number;
  name: string;
  label: string;
  placeholder?: string;
  type: FieldType;
  deleted: boolean;
  values: FormFieldValueDto[];
}

export type FormFieldValueDto = {
  id?: number;
  value: string;
}

export enum FieldType {
  Text = 1,
  Textarea = 2,
  Checkbox = 3,
  Radio = 4,
  Dropdown = 5,
  DateTime = 6,
  Number = 7,
}

export type SubmissionListDto = {
  id?: number;
  createDateTime?: Date;
  form: NamedEntityDto;
  values: SubmissionValueDto[];
};

export type SubmissionValueDto = {
  id: number;
  fieldId: number;
  type: FieldType;
  value: string;
};
