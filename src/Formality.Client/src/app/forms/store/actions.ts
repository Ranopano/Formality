import { Action } from 'app/common/store';
import { Pages } from '@/router/pages';
import { tryActionWith } from 'app/utils';
import { getType } from '..';
import * as Api from '../api';
import { form } from './getters';
import { State } from './state';
import {
  setLoading, setError, setForm, setForms, setFormId,
} from './mutations';

export const getForm = (id: number) => (
  { type: getType(getForm), id }
);

export const tryGetForm = async (v: Vue, id?: number) => {
  const getFormAction = getForm(id || Number(v.$route.params.id));

  await v.$store.dispatch(getFormAction);

  if (form(v)) {
    return true;
  }

  await v.$router.replace({ name: Pages.NotFound });

  return false;
};

export const searchForm = (query?: Api.SearchFormQuery) => (
  { type: getType(searchForm), query }
);

export const createForm = (name: string) => (
  { type: getType(createForm), name }
);

type Actions = {
  getForm: Action<typeof getForm, State>;
  searchForm: Action<typeof searchForm, State>;
  createForm: Action<typeof createForm, State>;
};

const tryAction = tryActionWith({
  loading: setLoading,
  error: setError,
});

const actions: Actions = {
  getForm: async ({ commit }, { id }) => tryAction(
    commit,
    async () => {
      const item = await Api.getForm(id);
      commit(setForm(item), { root: true });
    },
  ),
  searchForm: async ({ commit }, { query }) => tryAction(
    commit,
    async () => {
      const items = await Api.searchForm(query);
      commit(setForms(items), { root: true });
    },
  ),
  createForm: async ({ commit }, { name }) => tryAction(
    commit,
    async () => {
      const result = await Api.createForm({ name });
      commit(setFormId(result), { root: true });
    },
  ),
};

export default actions;
