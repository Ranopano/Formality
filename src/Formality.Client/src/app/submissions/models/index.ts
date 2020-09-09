import { NamedEntityDto } from 'app/common/models';
import { FieldType } from 'app/forms/models';

export type SubmissionDto = {
  id?: number;
  formId: number;
  createDateTime?: string;
  values: SubmissionValueDto[];
};

export type SubmissionListDto = {
  id?: number;
  form: NamedEntityDto;
  createDateTime?: string;
  values: SubmissionValueDto[];
};

export type SubmissionValueDto = {
  id?: number;
  fieldId?: number;
  type?: FieldType;
  value?: string;
};
