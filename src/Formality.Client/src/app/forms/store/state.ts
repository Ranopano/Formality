import { FormDto, FormListDto } from '../models';

export type State = {
  error?: string;
  formId: number;
  form?: FormDto;
  forms: FormListDto[];
  loading: boolean;
};

const state = (): State => ({
  error: undefined,
  formId: 0,
  form: undefined,
  forms: [],
  loading: true,
});

export default state;
