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
      console.log('user', this.user);
    },
    updateUserNotifications(notifications: Array<NotificationType>) {
      this.user.notifications = notifications;
    },
    clear() {
      this.user = {} as User;
    },
  },
  persist: true,
});
