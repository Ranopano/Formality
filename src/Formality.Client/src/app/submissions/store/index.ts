import { SubmissionListDto, SearchQueryDto } from '@/models';
import { httpClient } from '@/utils/api';
import { tryActionWith } from '@/utils';
import mutations, {
  setLoading, setError, setSubmissions, State,
} from './mutations';
import { searchSubmission } from './actions';
import * as getters from './getters';

const tryAction = tryActionWith({
  loading: setLoading,
  error: setError,
});

export default {
  namespaced: true,
  state: (): State => ({
    submissions: [],
    error: undefined,
    loading: true,
  }),
  mutations,
  getters: {
    [getters.loading.name]: (state: State) => state.loading,
    [getters.submissions.name]: (state: State) => state.submissions,
  },
  actions: {
    [searchSubmission.name]: async ({ commit }, { query }: { query?: SearchQueryDto }) => tryAction(
      commit,
      async () => {
        const items = await httpClient.get<SubmissionListDto[]>(
          '/api/submissions',
          { query },
        );
        commit(setSubmissions(items), { root: true });
      },
    ),
  },
  modules: {},
};
