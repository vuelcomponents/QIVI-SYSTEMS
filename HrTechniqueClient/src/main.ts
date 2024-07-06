import { createApp } from 'vue';
import './style.css';
import App from './App.vue';
import PrimeVue from 'primevue/config';
//@ts-ignore
import Wind from './assets/presets/wind';
import Tooltip from 'primevue/tooltip';
import Ripple from 'primevue/ripple';
import { router } from './router/router';
import { i18n } from './variables/translate';
import ToastService from 'primevue/toastservice';
import { TinyEmitter } from 'tiny-emitter';
import { ServiceRoots } from './services/ServiceRoots';
import { useGuard } from './services/useGuard';
import type { AuthService } from './services/AuthService';
import VueCookies from 'vue-cookies';
import { createPinia } from 'pinia';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';
import { UseUserStore } from './stores/UserStore';
import { StoreHandler } from './stores/StoreHandler';
import Button from 'primevue/button';
import { UseSettingStore } from '@/stores/SettingStore';
import { AgGridVue } from 'ag-grid-vue3';
import StatusRenderer from '@/utils/grid/renderers/StatusRenderer.vue';
import PreviewButtonRenderer from '@/utils/grid/renderers/PreviewButtonRenderer.vue';
import SignOutRenderer from '@/utils/grid/renderers/SignOutRenderer.vue';
import BlockRenderer from '@/utils/grid/renderers/BlockRenderer.vue';
import ClickBrother from '@/utils/directives/ClickBrother';
import DeviceStatusRenderer from '@/utils/grid/renderers/DeviceStatusRenderer.vue';
import VerifyStatusRenderer from '@/utils/grid/renderers/VerifyStatusRenderer.vue';
import LicenceRenderer from '@/utils/grid/renderers/LicenceRenderer.vue';

const app = createApp(App);

const pinia = createPinia();
pinia.use(piniaPluginPersistedstate);
app.use(pinia);

const emitter = new TinyEmitter();
const storeHandler = new StoreHandler({
  userStore: UseUserStore(),
  settingStore: UseSettingStore(),
});

storeHandler.settingStore.loadTheme();

//setting lang

const serviceRoots = new ServiceRoots(emitter, i18n.global.t, storeHandler);

useGuard(router, serviceRoots.create('AuthService') as AuthService);

app.use(PrimeVue, { unstyled: true, pt: Wind });
app.use(VueCookies);

app.use(router);

app.use(i18n);
app.use(ToastService);

app.provide('emitter', emitter);
app.provide('services', serviceRoots);
app.provide('storeHandler', storeHandler);

app.directive('tooltip', Tooltip);
app.directive('ripple', Ripple);

app.component('Button', Button);
app.component('AgGrid', AgGridVue);
app.component('StatusRenderer', StatusRenderer);
app.component('PreviewButtonRenderer', PreviewButtonRenderer);
app.component('SignOutRenderer', SignOutRenderer);
app.component('BlockRenderer', BlockRenderer);
app.component('DeviceStatusRenderer', DeviceStatusRenderer);
app.component('VerifyStatusRenderer', VerifyStatusRenderer);
app.component('LicenceRenderer', LicenceRenderer);
app.directive('clickbrother', ClickBrother);
app.mount('#app');

(i18n.global.locale as any).value = storeHandler.settingStore.language;
