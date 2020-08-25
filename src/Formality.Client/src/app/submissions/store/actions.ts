import { SearchQueryDto } from '@/models';
import { getType } from '..';

export const searchSubmission = (query?: SearchQueryDto) => (
  { type: getType(searchSubmission), query }
);
