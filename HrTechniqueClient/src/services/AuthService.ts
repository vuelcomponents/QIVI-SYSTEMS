import { Service } from './Service';
import type { TinyEmitter } from 'tiny-emitter';
import type { StoreHandler } from '@/stores/StoreHandler';
import type { AxiosResponse } from 'axios';
import type { NotificationType } from '@/types/User';
import { KickUnauthorized } from '@/services/KickUnauthorized';
export class AuthService extends Service {
  name = 'auth';

  constructor(emitter: TinyEmitter, t: any, storeHandler: StoreHandler) {
    super(emitter, t, storeHandler);
  }

  async authorized(token?: string) {
    const res = await this.http.get(`${this.path}/${this.name}/authorized`);
    if (res.status === 200 && res.data) {
      this.emitter.emit('logged-in', res.data);
    }
    return res;
  }
  @KickUnauthorized
  async getNotifications(): Promise<AxiosResponse<Array<NotificationType>>> {
    return await this.http.get(`${this.path}/${this.name}/notifications`);
  }
  @KickUnauthorized
  async seenNotification(id: number) {
    return await this.http.get(`${this.path}/${this.name}/notifications-seen/${id}`);
  }
  async logout() {
    await this.http.get(`${this.path}/${this.name}/logout`);
    this.storeHandler.userStore.clear();
    this.clearCookieValue('hrtechniquetoken');
    location.reload();
  }

}
