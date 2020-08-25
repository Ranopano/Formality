import { SearchQueryDto } from '@/models';
import { getType } from '..';

export const getForm = (id: number) => (
  { type: getType(getForm), id }
);

export const searchForm = (query?: SearchQueryDto) => (
  { type: getType(searchForm), query }
);

export const createForm = (name: string) => (
  { type: getType(createForm), name }
);
