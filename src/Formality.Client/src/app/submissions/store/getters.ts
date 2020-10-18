import { State as RootState } from '@/store/state';
import { mapStateGetters } from '@/app/common/store';
import { GetterTree } from 'vuex';
import { getType } from '..';
import { SubmissionListDto, SubmissionDto } from '../models';
import { State } from './state';

export const error = (v: Vue): string | undefined =>
  v.$store.getters[getType(error)] || false;

export const loading = (v: Vue): boolean =>
  v.$store.getters[getType(loading)] || false;

export const submission = (v: Vue): SubmissionDto | undefined => {
  return v.$store.getters[getType(submission)] || [];
};

export const submissions = (v: Vue): SubmissionListDto[] => {
  return v.$store.getters[getType(submissions)] || [];
};

const getters: GetterTree<State, RootState> = {
  ...mapStateGetters<State>([
    ['error', error],
    ['loading', loading],
    ['submission', submission],
    ['submissions', submissions],
  ]),
};

export default getters;
