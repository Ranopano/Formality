export const delay = (ms: number) => new Promise(success => {
  setTimeout(success, ms);
});

/**
 * Returns a function that executes an async action
 * and provided state mutations within a try/catch statement.
 */
export const tryActionWith = (mutations: {
  loading: (loading: boolean) => { type: string };
  error: (error?: string) => { type: string; error?: string };
}) => (async <T = void>(
  commit: (x: object, options?: { root: boolean }) => void,
  action: () => Promise<T>,
) => {
  const rootCommit = (x: object) => commit(x, { root: true });
  const { loading, error } = mutations;
  try {
    rootCommit(loading(true));
    rootCommit(error());
    await action();
  } catch (e) {
    rootCommit(error(e.message));
  } finally {
    rootCommit(loading(false));
  }
});
