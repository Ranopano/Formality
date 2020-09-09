import { Payload, ActionContext } from 'vuex';
import { upperFirst } from 'lodash';

export type Action<T extends (...args: never) => Payload, S = {}, R = {}> = (
  context: ActionContext<S, R>,
  payload: Payload & Omit<ReturnType<T>, 'type'>
) => Promise<void>;

export type Getter<T> = (state: T) => unknown;

export type Mutation<T> = (state: T, newState: T) => void;

export type Mutations<T> = {
  [x: string]: Mutation<T>;
};

const mapState = <TState, TMapper extends Function>(
  prefix: string,
  getMapper: (key: keyof TState) => TMapper,
  items: [keyof TState, Function?][],
) => {
  return items.reduce((result, [key, handler]) => {
    const stateKey = key.toString();
    const mapperKey = handler?.name || `${prefix}${upperFirst(stateKey)}`;
    result[mapperKey] = getMapper(key);
    return result;
  }, {} as Record<string, TMapper>);
};

export const mapStateGetters = <T>(getters: [keyof T, Function?][]) => {
  return mapState<T, Getter<T>>('get', key => x => x[key], getters);
};

export const mapStateSetters = <T>(setters: [keyof T, Function?][]) => {
  return mapState<T, Mutation<T>>('set', key => {
    return (currentState, newState) => {
      currentState[key] = newState[key];
    };
  }, setters);
};
