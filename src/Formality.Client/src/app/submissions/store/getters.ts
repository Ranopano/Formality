import { SubmissionListDto } from '@/models';
import { getType } from '..';

export const loading = (v: Vue): boolean => v.$store.getters[getType(loading)] || false;

export const submissions = (
  v: Vue,
): SubmissionListDto[] => v.$store.getters[getType(submissions)] || [];
