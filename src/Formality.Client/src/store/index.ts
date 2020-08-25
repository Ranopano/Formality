import Vue from 'vue';
import Vuex from 'vuex';
import forms from '@/app/forms/store';
import submissions from '@/app/submissions/store';
import * as getters from './getters';

Vue.use(Vuex);

const store = {
  state: (): State => ({
    title: 'Formality',
  }),
  mutations: {},
  getters: {
    [getters.title.name]: (state: State) => state.title,
  },
  actions: {},
  modules: {
    forms,
    submissions,
  },
};

type State = {
  title: string;
}

export default new Vuex.Store(store);
