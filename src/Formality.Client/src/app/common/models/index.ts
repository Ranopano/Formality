export type SearchQueryDto = {
  keyword?: string;
  orderBy?: OrderDto[];
  maxResults?: number;
};

export type OrderDto = {
  name: string;
  desc?: boolean;
}

export type NamedEntityDto = {
  id?: number;
  name?: string;
}
