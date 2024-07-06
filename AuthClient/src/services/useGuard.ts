import type { AuthService } from './AuthService';
import { i18n } from '@/variables/translate';

export const useGuard = async (router: any, authService: AuthService) => {
  router.beforeEach(async (to: any, from: any) => {
    try {
      if (to.query.err) {
        authService.emitter.emit('error', {
          severity: 'error',
          summary: i18n.global.t(to.query.err as string),
          life: 3000,
        });
      }
      if (to.query.inf) {
        authService.emitter.emit('error', {
          severity: 'info',
          summary: i18n.global.t(to.query.inf as string),
          life: 3000,
        });
      }
      if (to.name === 'Docs' || to.fullPath.split('/').includes('docs')) {
        return;
      }
      if (to.name === 'ResetPassword') {
        const rpResponse = await authService.verifyResetKeys(to.params.t);
        if (rpResponse.status !== 200) {
          authService.emitter.emit('error', {
            severity: 'error',
            summary: i18n.global.t('verificationFailed'),
            life: 3000,
          });
          await router.push({ name: 'Dashboard' });
        }
        return;
      }
      const authResponse = await authService.authorized();
      // if(to.name === 'Dashboard' && authService.storeHandler.userStore.user.role < 1){
      //        return await router.push({name: "EditAccount"})
      // }
      if ((authResponse as any)?.response?.status === 401) {
        if (to.name !== 'login') {
          await router.push({ name: 'login' });
        }
      }
      if ((authResponse as any)?.response?.status === 404) {
        if (to.name !== 'ServerDown') {
          await router.push({ name: 'ServerDown' });
        }
      }
      if ((authResponse as any)?.code === 'ERR_NETWORK') {
        if (to.name !== 'ServerDown') {
          await router.push({ name: 'ServerDown' });
        }
      }
      if ((authResponse as any)?.status === 200) {
        switch (true) {
          case to.name === 'login':
            if(to.query.api){
              return window.close();
            }
            return await router.replace({ name: 'home' });
          case to.name === 'ServerDown':
            return await router.replace({ name: 'home' });
        }
        authService.emitter.emit('logged-in', authResponse.data);
      }
    } catch (e) {
      return await router.push({ name: 'ServerDown' });
    }
  });
};
