import { FormListDto, FormDto } from '@/models';
import { Mutations } from '@/app/common/store';
import { getType } from '..';

export const setError = (error?: string) => ({
  type: getType(setError),
  error,
});

export const setFormId = (formId: number) => ({
  type: getType(setFormId),
  formId,
});

export const setForm = (form: FormDto) => ({
  type: getType(setForm),
  form,
});

export const setForms = (forms: FormListDto[]) => ({
  type: getType(setForms),
  forms,
});

export const setLoading = (loading: boolean) => ({
  type: getType(setLoading),
  loading,
});

export type State = {
  error?: string;
  formId: number;
  form?: FormDto;
  forms: FormListDto[];
  loading: boolean;
};

export const mutations: Mutations<State> = {
  [setLoading.name]: (state, { loading }) => {
    state.loading = loading;
  },
  [setFormId.name]: (state, { formId }) => {
    state.formId = formId;
  },
  [setForm.name]: (state, { form }) => {
    state.form = form;
  },
  [setForms.name]: (state, { forms }) => {
    state.forms = forms;
  },
  [setError.name]: (state, { error }) => {
    state.error = error;
  },
};

export default mutations;
