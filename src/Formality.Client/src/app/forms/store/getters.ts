import { GetterTree } from 'vuex';
import { State as RootState } from '@/store/state';
import { mapStateGetters } from 'app/common/store';
import { getType } from '..';
import { FormDto, FormListDto } from '../models';
import { State } from './state';

export const error = (v: Vue): string | undefined =>
  v.$store.getters[getType(error)];

export const formId = (v: Vue): number => v.$store.getters[getType(formId)];

export const form = (v: Vue): FormDto | undefined =>
  v.$store.getters[getType(form)];

export const formHeader = (v: Vue): string =>
  v.$store.getters[getType(formHeader)];

export const forms = (v: Vue): FormListDto[] =>
  v.$store.getters[getType(forms)] || [];

export const loading = (v: Vue): boolean =>
  v.$store.getters[getType(loading)] || false;

const getters: GetterTree<State, RootState> = {
  ...mapStateGetters<State>([
    ['error', error],
    ['loading', loading],
    ['form', form],
    ['formId', formId],
    ['forms', forms],
  ]),
  [formHeader.name]: (state, _, __, rootGetters) => {
    const name = state.form?.name;
    const header = `${rootGetters.title} — Forms${''}`;
    return name ? `${header} — ${name}` : header;
  },
};

export default getters;
