import { Mutations, mapStateSetters } from '@/app/common/store';
import { getType } from '..';
import { SubmissionListDto, SubmissionDto } from '../models';
import { State } from './state';

export const setError = (error?: string) => (
  { type: getType(setError), error }
);

export const setLoading = (loading: boolean) => (
  { type: getType(setLoading), loading }
);

export const setSubmission = (submission: SubmissionDto) => (
  { type: getType(setSubmission), submission }
);

export const setSubmissions = (submissions: SubmissionListDto[]) => (
  { type: getType(setSubmissions), submissions }
);

const mutations: Mutations<State> = {
  ...mapStateSetters([
    ['error', setError],
    ['loading', setLoading],
    ['submission', setSubmission],
    ['submissions', setSubmissions],
  ]),
};

export default mutations;
