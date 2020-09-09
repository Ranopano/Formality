export type State = {
  title: string;
  debug: boolean;
};

const state = (): State => ({
  title: 'Formality',
  debug: process.env.NODE_ENV !== 'production',
});

export default state;
