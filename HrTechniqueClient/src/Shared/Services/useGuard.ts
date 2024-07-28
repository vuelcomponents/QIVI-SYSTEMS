import type { AuthService } from '../../Entities/User/AuthService';
import { i18n } from '@/Shared/Translates';
import { getAssociatedUrl } from '@/Shared/Resources/urls';

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
      const authResponse = await authService.authorized();
      if ((authResponse as any)?.response?.status === 401) {
        if (to.name !== 'Unauthorized') {
          await router.push({ name: 'Unauthorized' });
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
          case to.name === 'Unauthorized':
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
