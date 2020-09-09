import { httpClient } from 'app/utils/api';
import { SearchQueryDto } from 'app/common/models';
import { SubmissionDto, SubmissionListDto, SubmissionValueDto } from '../models';

const endpoint = '/api/submissions';

export const getSubmission = (id: number) => {
  return httpClient.get<SubmissionDto>(`${endpoint}/${id}/`);
};

export type SearchSubmissionQuery = SearchQueryDto & {};

export const searchSubmission = (query?: SearchSubmissionQuery) => {
  return httpClient.get<SubmissionListDto[]>(endpoint, { query });
};

export type AddSubmissionCommand = {
  formId: number;
  values: SubmissionValueDto[];
};

export const addSubmission = (command: AddSubmissionCommand) => {
  return httpClient.post<number>(endpoint, {
    data: command,
  });
};
