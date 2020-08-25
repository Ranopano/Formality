import { FormDto, FormListDto } from '@/models';
import { getType } from '..';

export const error = (v: Vue): string | undefined => v.$store.getters[getType(error)];

export const formId = (v: Vue): number => v.$store.getters[getType(formId)];

export const form = (v: Vue): FormDto | undefined => v.$store.getters[getType(form)];

export const formHeader = (v: Vue): string => v.$store.getters[getType(formHeader)];

export const forms = (v: Vue): FormListDto[] => v.$store.getters[getType(forms)] || [];

export const loading = (v: Vue): boolean => v.$store.getters[getType(loading)] || false;
