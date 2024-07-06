import { defineStore } from 'pinia';
import { i18n } from '@/variables/translate';

const getBrowserLanguage = () => {
  const navigatorLanguage = navigator.language?.substr(0, 2);
  if (['pl', 'en'].includes(navigatorLanguage)) {
    return navigatorLanguage;
  }
  return 'en';
};

export const UseSettingStore = defineStore('setting', {
  state: () => ({
    theme: 'dark' as 'light' | 'dark',
    themes: ['light', 'dark'],
    language: getBrowserLanguage(),
    currency:'pln'
  }),
  actions: {
    setTheme(theme: 'light' | 'dark') {
      this.theme = theme;
      if (document.body.classList.contains('dark')) {
        document.body.classList.remove('dark');
      }
      if (document.body.classList.contains('light')) {
        document.body.classList.remove('light');
      }
      document.body.classList.add(theme);
    },
    switchTheme() {
      this.theme === 'light' ? this.setTheme('dark') : this.setTheme('light');
    },
    loadTheme() {
      document.body.classList.add(this.theme);
    },
    setLanguage(lang: string) {
      this.language = lang;
      (i18n.global.locale as any).value = lang;
    },
  },
  persist: true,
});
