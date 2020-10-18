import { Commit } from 'vuex';
import { ProblemDetails, ValidationProblemDetails } from './api';
import dayjs from 'dayjs';

export const delay = (ms: number) => new Promise(x => setTimeout(x, ms));

export const nameof = <T>(name: Extract<keyof T, string>): string => name;

export const utcLocal = (value: string) =>
  dayjs
    .utc(value)
    .local()
    .format('YYYY.MM.DD HH:mm:ss');

// TODO: clean up
// eslint-disable-next-line @typescript-eslint/no-explicit-any
const getErrorMessage = (e: any): string | undefined => {
  if (e instanceof Error) {
    return e.message;
  }

  if (e.errors) {
    const problem = e as ValidationProblemDetails;
    return Object.entries(problem.errors || {})
      .map(([key, values]) => `${key}: ${values.join('; ')}`)
      .join(' | ');
  }

  if (e.detail) {
    const problem = e as ProblemDetails;
    return problem.detail;
  }

  return undefined;
};

/**
 * Returns a function that executes an async action
 * and provided state mutations within a try/catch statement.
 */
export const tryActionWith = (mutations: {
  loading: (loading: boolean) => { type: string; loading: boolean };
  error: (error?: string) => { type: string; error?: string };
}) => async <T = void>(commit: Commit, action: () => Promise<T>) => {
  const { loading, error } = mutations;
  try {
    commit(loading(true), { root: true });
    commit(error(), { root: true });
    await action();
  } catch (e) {
    const message = getErrorMessage(e) || 'An unknown error occured.';
    commit(error(message), { root: true });
  } finally {
    commit(loading(false), { root: true });
  }
};
