import Vue from 'vue';
import Vuex, { Module } from 'vuex';
import forms from 'app/forms/store';
import submissions from 'app/submissions/store';
import actions from './actions';
import getters from './getters';
import mutations from './mutations';
import state, { State } from './state';

Vue.use(Vuex);

const store = {
  strict: process.env.NODE_ENV !== 'production',
  state,
  mutations,
  getters,
  actions,
  modules: {
    forms,
    submissions,
  },
} as Module<State, State>;

export default new Vuex.Store(store);
