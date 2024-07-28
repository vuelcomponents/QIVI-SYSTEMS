import { Service } from '@/Shared/Services/Service';
import type { TinyEmitter } from 'tiny-emitter';
import type { StoreHandler } from '@/Stores/StoreHandler';

export class EmployeeService extends Service {
  name = 'employee';

  constructor(emitter: TinyEmitter, t: any, storeHandler: StoreHandler) {
    super(emitter, t, storeHandler);
  }
}