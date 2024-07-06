import { KickUnauthorized } from '@/services/KickUnauthorized';
import type { TinyEmitter } from 'tiny-emitter';
import { Service } from '@/services/Service';
import type { StoreHandler } from '@/stores/StoreHandler';

export class StatService extends Service {
  name = 'stat';

  constructor(emitter: TinyEmitter, t: any, storeHandler: StoreHandler) {
    super(emitter, t, storeHandler);
  }
}
