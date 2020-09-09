import { GetterTree } from 'vuex';
import { mapStateGetters } from 'app/common/store';
import { State } from './state';

export const title = (v: Vue): string => v.$store.getters[title.name];

export const debug = (v: Vue): string => v.$store.getters[debug.name];

const getters: GetterTree<State, State> = {
  ...mapStateGetters([
    ['title', title],
    ['debug', debug],
  ]),
};

export default getters;
