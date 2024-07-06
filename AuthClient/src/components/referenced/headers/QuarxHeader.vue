<script setup lang="ts">
import logo from '../../../assets/quartz-light4.svg';
import darklogo from '../../../assets/quartz-dark.svg';
import { computed, inject, nextTick, onBeforeMount, onMounted, ref } from 'vue';
import SmallDialog from '@/components/reusable/dialogs/SmallDialog.vue';
import UserDialog from '../dialogs/UserDialog.vue';
import type { StoreHandler } from '@/stores/StoreHandler';
import NotificationDialog from '@/components/referenced/dialogs/NotificationDialog.vue';
import type { ServiceRoots } from '@/services/ServiceRoots';
import type { AuthService } from '@/services/AuthService';
import LanguageDialog from '@/components/referenced/dialogs/LanguageDialog.vue';
import MainAppChooser from '@/components/referenced/headers/MainAppChooser.vue';
import { useRoute } from 'vue-router';
import { router } from '@/router/router';
import type { TinyEmitter } from 'tiny-emitter';

const storeHandler: StoreHandler = inject('storeHandler')!;

defineProps<{
  loggedIn?: boolean;
}>();

const services: ServiceRoots = inject<ServiceRoots>('services')!;
const emitter: TinyEmitter = inject<TinyEmitter>('emitter')!;
const authService: AuthService = services.create<AuthService>('AuthService')!;

const appDialog = ref(false);
const userDialog = ref(false);
const notifiesDialog = ref(false);
const languageDialog = ref(false);
const route = useRoute();
const docs = computed(() => route.matched.some((record) => record.name === 'Docs'));
const toggleMenu = () => {
  emitter.emit('toggleMenu', true);
};
</script>

<template>
  <header
    class="rounded-xl border-b-[1px] border-light-600/40 bg-west-1 p-2 text-west-3 shadow-sm ring-surface-200/20 dark:border-surface-500/20 dark:bg-surface-1000 dark:text-surface-0 xl:ring-2"
  >
    <div class="flex items-center justify-center pl-4 pr-4 font-dms xl:justify-between">
      <div>
        <div class="flex gap-8 text-lg text-west-3 dark:text-surface-200">
          <div class="relative" v-show="loggedIn">
            <div
              @click="toggleMenu"
              :class="[
                'text-trueGreen-500 cursor-pointer  transition-all' + 'rounded-xl pl-2 pr-2 ',
              ]"
            >
              <i class="mdi mdi-menu-open text-west-3 dark:text-surface-200" />
            </div>
          </div>
          <div class="relative" v-show="loggedIn">
            <div
              @click="appDialog = !appDialog"
              :class="[
                'appDialog text-trueGreen-500 cursor-pointer  transition-all' +
                  'rounded-xl pl-2 pr-2 ',
              ]"
            >
              <i class="mdi mdi-rhombus-split text-west-3 dark:text-surface-200" />
            </div>
            <div v-if="appDialog">
              <SmallDialog
                name="appDialog"
                @shut="appDialog = false"
                icon="mdi mdi-home"
                :label="$t('services')"
              >
                <MainAppChooser />
              </SmallDialog>
            </div>
          </div>
          <div class="relative" v-if="!loggedIn && docs">
            <div
              @click="appDialog = !appDialog"
              :class="[
                'appDialog text-trueGreen-500 cursor-pointer  transition-all' +
                  'rounded-xl pl-2 pr-2 ' +
                  ' hover:text-xl   ',
                appDialog ? ' text-xl ' : '',
              ]"
            >
              <i
                class="mdi mdi-cloud-lock text-west-3 dark:text-surface-200"
                v-if="docs"
                @click="router.push({ name: 'Dashboard' })"
              />
              <i
                class="mdi mdi-book text-west-3 dark:text-surface-200"
                v-else
                @click="router.push({ name: 'Docs' })"
              />
            </div>
          </div>
          <div class="relative" @click="authService.storeHandler.settingStore.switchTheme()">
            <div
              class="absolute right-0 top-[5px] h-[6px] w-[6px] rounded-full bg-yellow-500 dark:bg-trueGreen-400"
            />
            <i
              :class="`notifiesDialog mdi mdi-theme-light-dark cursor-pointer text-west-3 dark:text-surface-200 ${authService.storeHandler.settingStore.theme === 'dark' ? 'text-mintSplash-100' : 'text-light-100'}`"
            />
          </div>
          <div class="relative">
            <i
              class="languageDialog mdi mdi-translate-variant cursor-pointer text-west-3 dark:text-surface-200"
              @click="languageDialog = !languageDialog"
            />
            <div class="absolute left-[-200px] w-[200px]" v-if="languageDialog">
              <SmallDialog
                @shut="languageDialog = false"
                name="languageDialog"
                icon="mdi mdi-translate-variant"
                :label="$t('language')"
              >
                <LanguageDialog />
              </SmallDialog>
            </div>
          </div>
          <div class="relative" v-show="loggedIn">
            <div
              v-if="loggedIn && authService.storeHandler.userStore.user?.notifications?.length > 0"
              class="absolute right-0 top-[5px] h-[6px] w-[6px] rounded-full bg-west-3 dark:bg-forest-400"
            />
            <i
              class="notifiesDialog mdi mdi-bell-ring-outline cursor-pointer text-west-3 dark:text-surface-200"
              @click="notifiesDialog = !notifiesDialog"
            />
            <div class="absolute left-[-200px] w-[200px]" v-if="notifiesDialog">
              <SmallDialog
                @shut="notifiesDialog = false"
                name="notifiesDialog"
                icon="mdi mdi-bell-ring-outline"
                :label="$t('notifications')"
              >
                <Suspense>
                  <NotificationDialog :auth-service="authService" />
                </Suspense>
              </SmallDialog>
            </div>
          </div>
          <div class="relative" v-show="loggedIn">
            <div
              class="absolute right-0 top-[5px] h-[6px] w-[6px] rounded-full bg-primary-400 dark:bg-forest-400"
            />
            <i
              class="userDialog mdi mdi-account-circle-outline cursor-pointer text-west-3 dark:text-surface-200"
              @click="userDialog = !userDialog"
            />
            <div class="absolute left-[-200px] w-[200px]" v-if="userDialog">
              <SmallDialog
                @shut="userDialog = false"
                name="userDialog"
                icon="mdi mdi-account"
                :label="$t('profile')"
              >
                <UserDialog :auth-service="authService" />
              </SmallDialog>
            </div>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>
