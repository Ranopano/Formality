import { SubmissionDto, SubmissionListDto } from '../models';

export type State = {
  error?: string;
  loading: boolean;
  submission?: SubmissionDto;
  submissions: SubmissionListDto[];
};

const state = (): State => ({
  submission: undefined,
  submissions: [],
  error: undefined,
  loading: true,
});

export default state;
