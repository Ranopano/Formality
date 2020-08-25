import { SubmissionListDto } from '@/models';
import { Mutations } from '@/app/common/store';
import { getType } from '..';

export const setError = (error?: string) => ({
  type: getType(setError),
  error,
});

export const setLoading = (loading: boolean) => ({
  type: getType(setLoading),
  loading,
});

export const setSubmissions = (submissions: SubmissionListDto[]) => ({
  type: getType(setSubmissions),
  submissions,
});

export type State = {
  error?: string;
  loading: boolean;
  submissions: SubmissionListDto[];
};

const mutations: Mutations<State> = {
  [setError.name]: (state, { error }) => {
    state.error = error;
  },
  [setLoading.name]: (state, { loading }) => {
    state.loading = loading;
  },
  [setSubmissions.name]: (state, { submissions }) => {
    state.submissions = submissions;
  },
};

export default mutations;
