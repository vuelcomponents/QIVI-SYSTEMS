import { defineStore } from 'pinia';
import type { NotificationType, User } from '@/types/User';

export const UseUserStore = defineStore('auth', {
  state: () => ({
    user: {} as User,
  }),
  actions: {
    updateAuthUser(user: Partial<User>) {
      this.user = {
        ...this.user,
        ...user,
      };
    },
    updateUserNotifications(notifications: Array<NotificationType>) {
      this.user.notifications = notifications;
      if(this.user.notifications?.length <1){
        this.user.notified = false;
      }
    },
    clear() {
      this.user = {} as User;
    },
  },
  persist: true,
});
