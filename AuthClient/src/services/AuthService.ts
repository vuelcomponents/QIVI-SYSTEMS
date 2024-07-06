import { Service } from './Service';
import type { TinyEmitter } from 'tiny-emitter';
import type { StoreHandler } from '@/stores/StoreHandler';
import { KickUnauthorized } from '@/services/KickUnauthorized';
import type { NewPassword } from '@/types/NewPassword';
import type { EmailChange } from '@/views/Account/EditAccount/EditEmail.vue';
import type { AxiosResponse } from 'axios';
import type { NotificationType } from '@/types/User';
export class AuthService extends Service {
  name = 'auth';

  constructor(emitter: TinyEmitter, t: any, storeHandler: StoreHandler) {
    super(emitter, t, storeHandler);
  }

  async authorized(token?: string) {
    const res = await this.http.get(`${this.path}/${this.name}/authorized`);
    if (res.status === 200 && res.data) {
      // console.log('auth response', res.data)
      this.emitter.emit('logged-in', res.data);
    }
    return res;
  }
  async login(user: any, suppressReload?:boolean) {
    return await this.http.post(`${this.path}/${this.name}/login`, user).then((res: any) => {
      if (res.status === 200 && res.data) {
        this.emitter.emit('logged-in', res.data);
        if(!suppressReload) location.reload();
      }
      return res;
    });
  }
  @KickUnauthorized
  async getSelf() {
    return await this.http.get(`${this.path}/${this.name}/get`);
  }
  @KickUnauthorized
  async changePassword(newPassword: NewPassword) {
    return await this.http.post(`${this.path}/${this.name}/change-password`, newPassword);
  }
  async logout() {
    await this.http.get(`${this.path}/${this.name}/logout`);
    this.storeHandler.userStore.clear();
    this.clearCookieValue('hrtechniquetoken');
    location.reload();
  }
  @KickUnauthorized
  async verifyResetKeys(t: string) {
    return await this.http.get(`${this.path}/${this.name}/verify-reset-password-key` + `?&t=${t}`);
  }
  @KickUnauthorized
  async resetPassword(token: string, newPassword: string) {
    return await this.http.post(`${this.path}/${this.name}/reset-password-by-key`, {
      token,
      newPassword,
    });
  }
  async initResetPassword(email: string) {
    return await this.http.get(`${this.path}/${this.name}/reset-password?email=${email}`);
  }
  @KickUnauthorized
  async getDevices() {
    return await this.http.get(`${this.path}/${this.name}/devices`);
  }
  @KickUnauthorized
  async signOutDevice(id: number) {
    return await this.http.get(`${this.path}/${this.name}/device-signout/${id}`);
  }
  @KickUnauthorized
  async blockDevices(ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/device-block`, ids);
  }
  @KickUnauthorized
  async unlockDevices(ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/device-unlock`, ids);
  }
  @KickUnauthorized
  async blockIps(ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/ip-block`, ids);
  }
  @KickUnauthorized
  async unlockIps(ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/ip-unlock`, ids);
  }
  @KickUnauthorized
  async verifyDevices(ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/device-verify-grid`, ids);
  }
  @KickUnauthorized
  async changeEmail(data: EmailChange) {
    return await this.http.post(`${this.path}/${this.name}/change-email`, data);
  }
  @KickUnauthorized
  async getNotifications(): Promise<AxiosResponse<Array<NotificationType>>> {
    return await this.http.get(`${this.path}/${this.name}/notifications`);
  }
  @KickUnauthorized
  async seenNotification(id: number) {
    return await this.http.get(`${this.path}/${this.name}/notifications-seen/${id}`);
  }

  async verifyEmail(email: string) {
    return await this.http.get(`${this.path}/${this.name}/send-verification-to-email/${email}`);
  }
}
