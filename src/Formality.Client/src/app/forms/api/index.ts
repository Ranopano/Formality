import { httpClient } from 'app/utils/api';
import { SearchQueryDto } from 'app/common/models';
import { FormDto, FormListDto, FormState } from '../models';

const endpoint = '/api/forms';

export const getForm = (id: number) => {
  return httpClient.get<FormDto>(`${endpoint}/${id}/`);
};

export type SearchFormQuery = SearchQueryDto & {
  stateId?: FormState;
};

export const searchForm = (query?: SearchFormQuery) => {
  return httpClient.get<FormListDto[]>(endpoint, { query });
};

export type CreateFormCommand = { name: string };

export const createForm = (command: CreateFormCommand) => {
  return httpClient.post<number>(endpoint, {
    data: command,
  });
};
