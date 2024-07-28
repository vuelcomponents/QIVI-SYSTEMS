import useInterceptors from './useInterceptors';
import type { AxiosInstance } from 'axios';
import axios from 'axios';
import type { TinyEmitter } from 'tiny-emitter';
import { KickUnauthorized } from './KickUnauthorized';
import { getAssociatedUrl } from '@/Shared/Resources/urls';
import type { StoreHandler } from '@/Stores/StoreHandler';

export class Service {
  emitter: TinyEmitter;
  t: any;
  host = getAssociatedUrl('hrtechnique-server');
  path = `${this.host}`;
  name?: string;
  http: AxiosInstance = axios;

  storeHandler: StoreHandler;
  constructor(emitter: TinyEmitter, t: any, storeHandler: StoreHandler) {
    this.http.defaults.withCredentials = true;
    this.emitter = emitter;
    this.storeHandler = storeHandler;
    useInterceptors({ client: this.http, emitter, t });
  }
  @KickUnauthorized
  async getAll() {
    return await this.http.get(`${this.path}/${this.name}/get`);
  }
  @KickUnauthorized
  async getById(id: number) {
    return await this.http.get(`${this.path}/${this.name}/get/${id}`);
  }
  @KickUnauthorized
  async create(object: any) {
    return await this.http.post(`${this.path}/${this.name}/create`, object);
  }
  @KickUnauthorized
  async update(object: any) {
    return await this.http.patch(`${this.path}/${this.name}/update`, object);
  }
  @KickUnauthorized
  async delete(id: number) {
    return await this.http.delete(`${this.path}/${this.name}/delete/${id}`);
  }
  @KickUnauthorized
  async deleteMany(list: Array<any>) {
    return await this.http.post(`${this.path}/${this.name}/delete-many`, list);
  }
  findCookieValue(cookieName: string): string | null {
    const cookies = document.cookie.split(';');
    for (let cookie of cookies) {
      const [name, value] = cookie.trim().split('=');
      if (name === cookieName) {
        return decodeURIComponent(value);
      }
    }

    return null;
  }
  clearCookieValue(cookieName: string): string | null {
    const cookies = document.cookie.split(';');
    for (let cookie of cookies) {
      const [name, value] = cookie.trim().split('=');
      if (name === cookieName) {
        const decodedValue = decodeURIComponent(value);
        // Ustaw czas życia ciasteczka na przeszłość, co spowoduje jego usunięcie
        document.cookie = `${cookieName}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
        return decodedValue;
      }
    }
    return null;
  }
}
