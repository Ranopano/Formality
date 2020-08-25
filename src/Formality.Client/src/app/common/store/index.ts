export type Mutations<T> = {
  [x: string]: (state: T, newState: T) => void;
};
