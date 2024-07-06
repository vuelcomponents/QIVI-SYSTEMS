import type { Store } from 'pinia';

export class StoreHandler {
  userStore: Store<string, Pick<any, any>>;
  settingStore: Store<string, Pick<any, any>>;
  constructor(reference: StoreHandlerReference) {
    this.userStore = reference.userStore;
    this.settingStore = reference.settingStore;
  }
}

type StoreHandlerReference = {
  userStore: Store;
  settingStore: Store;
};
