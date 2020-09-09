import { Module } from 'vuex';
import { State as RootState } from '@/store/state';
import { mutations } from './mutations';
import actions from './actions';
import getters from './getters';
import state, { State } from './state';

export default {
  namespaced: true,
  state,
  mutations,
  getters,
  actions,
} as Module<State, RootState>;
