import { AuthService } from './AuthService';

import type { TinyEmitter } from 'tiny-emitter';
import type { StoreHandler } from '@/stores/StoreHandler';
import type { Service } from '@/services/Service';
import { UserService } from './UserService';
import { StatService } from '@/services/StatService';

interface Services {
  [key: string]: new (emitter: TinyEmitter, t: Function, storeHandler: StoreHandler) => any;
}

export class ServiceRoots {
  emitter: TinyEmitter;
  t: any;
  storeHandler: StoreHandler;
  services: Services = {
    AuthService,
    UserService,
    StatService,
  };

  constructor(emitter: TinyEmitter, t: Function, storeHandler: StoreHandler) {
    this.emitter = emitter;
    this.t = t;
    this.storeHandler = storeHandler;
  }

  create<T>(service: any): T | void {
    try {
      switch (typeof service) {
        case 'string':
          return new this.services[service](this.emitter, this.t, this.storeHandler) as T;
        default:
          return new service(this.emitter) as T;
      }
    } catch (e) {
      this.emitter.emit('error', 'No valid service has been found');
    }
  }
}
