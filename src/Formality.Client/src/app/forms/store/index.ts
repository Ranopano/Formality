import { FormDto, FormListDto, SearchQueryDto } from '@/models';
import { httpClient } from '@/utils/api';
import { tryActionWith } from '@/utils';
import { title } from '@/store/getters';
import {
  mutations, setError, setForms, setLoading, State, setFormId, setForm,
} from './mutations';
import { getForm, searchForm, createForm } from './actions';
import * as getters from './getters';

const tryAction = tryActionWith({
  loading: setLoading,
  error: setError,
});

export default {
  namespaced: true,
  state: (): State => ({
    error: undefined,
    formId: 0,
    form: undefined,
    forms: [],
    loading: true,
  }),
  mutations,
  getters: {
    [getters.error.name]: (state: State) => state.error,
    [getters.formId.name]: (state: State) => state.formId,
    [getters.form.name]: (state: State) => state.form,
    [getters.formHeader.name]: (state: State, _, __, rootGetters) => {
      const name = state.form?.name;
      const header = `${rootGetters[title.name]} — Forms${''}`;
      return name ? `${header} — ${name}` : header;
    },
    [getters.forms.name]: (state: State) => state.forms,
    [getters.loading.name]: (state: State) => state.loading,
  },
  actions: {
    [getForm.name]: async ({ commit }, { id }: { id: number }) => tryAction(
      commit,
      async () => {
        const form = await httpClient.get<FormDto>(`/api/forms/${id}/`);
        commit(setForm(form), { root: true });
      },
    ),
    [searchForm.name]: async ({ commit }, { query }: { query?: SearchQueryDto }) => tryAction(
      commit,
      async () => {
        const items = await httpClient.get<FormListDto[]>('/api/forms', { query });
        commit(setForms(items), { root: true });
      },
    ),
    [createForm.name]: async ({ commit }, { name }: { name: string }) => tryAction(
      commit,
      async () => {
        const id = await httpClient.post<number>('/api/forms', {
          data: { name },
        });
        commit(setFormId(id), { root: true });
      },
    ),
  },
  modules: {},
};
