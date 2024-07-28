import type { DefaultLocaleMessageSchema } from 'vue-i18n';
import { createI18n } from 'vue-i18n';
import { en } from './en';
import { pl } from './pl';
import { de } from '@/Shared/Translates/de';
import { nl } from '@/Shared/Translates/nl';

const messages = {
  en,
  pl,
  de,
  nl,
};
const options = {
  legacy: false,
  locale: 'pl',
  fallbackLocale: 'pl',
  messages,
};

export const i18n = createI18n(options);

type MessageSchema = typeof en & DefaultLocaleMessageSchema;

declare module 'vue-i18n' {
  export interface DefineLocaleMessage extends MessageSchema {}
}
