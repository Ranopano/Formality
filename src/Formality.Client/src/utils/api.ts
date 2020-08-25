const API_URL = 'https://localhost:5001';

export type QueryString = Record<string, unknown>;

const handleResponse = async <T>(promise: Promise<Response>) => {
  const response = await promise;

  if (response.ok) {
    try {
      return response.json() as Promise<T>;
    } catch (e) {
      return Promise.reject(e);
    }
  } else {
    try {
      // TODO: return proper error
      const error = await response.json() as { statusCode: number; message: string };
      return Promise.reject(error);
    } catch {
      return Promise.reject(new Error('An unexpected error occurred.'));
    }
  }
};

const appendQuery = (basePath: string, path: string, query?: QueryString) => {
  const url = `${basePath}${path}`;
  return query ? `${url}?${JSON.stringify(query)}` : url;
};

type HttpClientOptions = {
  query?: QueryString;
  signal?: AbortSignal;
  headers?: Headers | Record<string, string>;
};

type GetOptions = HttpClientOptions & {};

type PostOptions = HttpClientOptions & {
  data: {};
};

export default class HttpClient {
  public constructor(private readonly basePath: string) {}

  public get<T>(url: string, options?: GetOptions): Promise<T> {
    const { query, headers, signal } = options || {};

    const fullUrl = appendQuery(this.basePath, url, query);

    const response = handleResponse<T>(
      fetch(fullUrl, {
        method: 'GET',
        signal,
        cache: 'default',
        mode: 'cors',
        headers: {
          Accept: 'application/json',
          ...headers,
        },
      }),
    );

    return response;
  }

  public post<T>(url: string, options?: PostOptions): Promise<T> {
    const {
      data, headers, query, signal,
    } = options || {};

    let body = '';

    if (data) {
      try {
        body = JSON.stringify(data);
      } catch (e) {
        return Promise.reject(e.message);
      }
    }

    return handleResponse<T>(
      fetch(appendQuery(this.basePath, url, query), {
        method: 'POST',
        signal,
        body,
        credentials: 'include',
        mode: 'cors',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          ...headers,
        },
      }),
    );
  }
}

export const httpClient = new HttpClient(API_URL);
