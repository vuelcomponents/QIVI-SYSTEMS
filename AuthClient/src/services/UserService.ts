import { Service } from '@/services/Service';
import type { TinyEmitter } from 'tiny-emitter';
import { KickUnauthorized } from '@/services/KickUnauthorized';
import type { StoreHandler } from '@/stores/StoreHandler';
import type { NotificationType, User } from '@/types/User';
export class UserService extends Service {
  name = 'user';

  constructor(emitter: TinyEmitter, t: any, storeHandler: StoreHandler) {
    super(emitter, t, storeHandler);
  }

  @KickUnauthorized
  async getUserDevices(userId: number) {
    return await this.http.get(`${this.path}/${this.name}/devices/${userId}`);
  }
  @KickUnauthorized
  async signOutUserDevice(userId: number, deviceId: number) {
    return await this.http.get(`${this.path}/${this.name}/device-signout/${userId}/${deviceId}`);
  }
  @KickUnauthorized
  async blockUserDevices(userId: number, ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/device-block/${userId}`, ids);
  }
  @KickUnauthorized
  async unlockUserDevices(userId: number, ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/device-unlock/${userId}`, ids);
  }
  @KickUnauthorized
  async blockUserIps(userId: number, ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/ip-block/${userId}`, ids);
  }
  @KickUnauthorized
  async unlockUserIps(userId: number, ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/ip-unlock/${userId}`, ids);
  }
  @KickUnauthorized
  async verifyDevices(userId: number, ids: Array<{ id: string }>) {
    return await this.http.post(`${this.path}/${this.name}/device-verify-grid/${userId}`, ids);
  }
  @KickUnauthorized
  async sendNotification(users: Array<User>, notification: NotificationType) {
    return await this.http.post(`${this.path}/${this.name}/notify`, {
      users,
      notification,
    });
  }
  @KickUnauthorized
  async blockSelected(usersIds: Array<{ id: number }>) {
    return await this.http.post(`${this.path}/${this.name}/block`, usersIds);
  }
  @KickUnauthorized
  async unlockSelected(usersIds: Array<{ id: number }>) {
    return await this.http.post(`${this.path}/${this.name}/unlock`, usersIds);
  }
}
