import { createApp } from 'vue';
import './style.css';
import App from './App.vue';
import PrimeVue from 'primevue/config';
//@ts-ignore
import Wind from '@/Assets/presets/wind';
import Tooltip from 'primevue/tooltip';
import Ripple from 'primevue/ripple';
import { router } from '@/Router/router';
import { i18n } from '@/Shared/Translates';
import ToastService from 'primevue/toastservice';
import { TinyEmitter } from 'tiny-emitter';
import { ServiceRoots } from '@/Shared/Services/ServiceRoots';
import { useGuard } from '@/Shared/Services/useGuard';
import type { AuthService } from '@/Entities/User/AuthService';
import VueCookies from 'vue-cookies';
import { createPinia } from 'pinia';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';
import { UseUserStore } from '@/Stores/UserStore';
import { StoreHandler } from '@/Stores/StoreHandler';
import Button from 'primevue/button';
import { UseSettingStore } from '@/Stores/SettingStore';
import { AgGridVue } from 'ag-grid-vue3';
import StatusRenderer from '@/Shared/Grid/renderers/StatusRenderer.vue';
import PreviewButtonRenderer from '@/Shared/Grid/renderers/PreviewButtonRenderer.vue';
import SignOutRenderer from '@/Shared/Grid/renderers/SignOutRenderer.vue';
import BlockRenderer from '@/Shared/Grid/renderers/BlockRenderer.vue';
import DeviceStatusRenderer from '@/Shared/Grid/renderers/DeviceStatusRenderer.vue';
import VerifyStatusRenderer from '@/Shared/Grid/renderers/VerifyStatusRenderer.vue';
import LicenceRenderer from '@/Shared/Grid/renderers/LicenceRenderer.vue';

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
app.provide('Services', serviceRoots);
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
app.mount('#app');

(i18n.global.locale as any).value = storeHandler.settingStore.language;
