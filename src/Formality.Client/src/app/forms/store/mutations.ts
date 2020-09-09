import { Mutations, mapStateSetters } from 'app/common/store';
import { getType } from '..';
import { FormDto, FormListDto } from '../models';
import { State } from './state';

export const setError = (error?: string) => (
  { type: getType(setError), error }
);

export const setFormId = (formId: number) => (
  { type: getType(setFormId), formId }
);

export const setForm = (form: FormDto) => (
  { type: getType(setForm), form }
);

export const setForms = (forms: FormListDto[]) => (
  { type: getType(setForms), forms }
);

export const setLoading = (loading: boolean) => (
  { type: getType(setLoading), loading }
);

export const mutations: Mutations<State> = {
  ...mapStateSetters([
    ['error', setError],
    ['loading', setLoading],
    ['form', setForm],
    ['formId', setFormId],
    ['forms', setForms],
  ]),
};

export default mutations;
