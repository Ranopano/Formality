import { Pages } from '@/router/pages';
import { Action } from 'app/common/store';
import { tryActionWith } from '@/app/utils';
import { getType } from '..';
import * as Api from '../api';
import { SubmissionDto } from '../models';
import * as getters from './getters';
import {
  setLoading, setError, setSubmission, setSubmissions,
} from './mutations';
import { State } from './state';

export const getSubmission = (id: number) => (
  { type: getType(getSubmission), id }
);

export const tryGetSubmission = async (v: Vue) => {
  const id = Number(v.$route.params.id);

  await v.$store.dispatch(getSubmission(id));

  if (getters.submission(v)) {
    return true;
  }

  await v.$router.replace({ name: Pages.NotFound });

  return false;
};

export const searchSubmission = (query?: Api.SearchSubmissionQuery) => (
  { type: getType(searchSubmission), query }
);

export const addSubmission = (submission: SubmissionDto) => (
  { type: getType(addSubmission), submission }
);

type Actions = {
  getSubmission: Action<typeof getSubmission, State>;
  searchSubmission: Action<typeof searchSubmission, State>;
  addSubmission: Action<typeof addSubmission, State>;
};

const tryAction = tryActionWith({
  loading: setLoading,
  error: setError,
});

const actions: Actions = {
  getSubmission: async ({ commit }, { id }) => tryAction(
    commit,
    async () => {
      const item = await Api.getSubmission(id);
      commit(setSubmission(item), { root: true });
    },
  ),
  searchSubmission: async ({ commit }, { query }) => tryAction(
    commit,
    async () => {
      const items = await Api.searchSubmission(query);
      commit(setSubmissions(items), { root: true });
    },
  ),
  addSubmission: async ({ commit }, { submission }) => tryAction(
    commit,
    async () => {
      const id = await Api.addSubmission(submission);
      if (!Number(id)) {
        commit(setError('Cannot add submission.'), { root: true });
      }
    },
  ),
};

export default actions;
