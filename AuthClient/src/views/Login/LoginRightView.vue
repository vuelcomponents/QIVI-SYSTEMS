<script setup lang="ts">
import { computed, ref } from 'vue';
import { useI18n } from 'vue-i18n';
import type { AuthService } from '@/services/AuthService';
import type { Store } from 'pinia';
import Menubar from 'primevue/menubar';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import SomeStepper from '@/components/reusable/steppers/SomeStepper.vue';
import { unfilledRequired } from '@/utils/verification/unfilledRequired';
import { useRoute } from 'vue-router';
import { Urls } from '@/variables/urls';

const props = defineProps<{
  authService: AuthService;
  store: Store<any, any>;
}>();

const email = ref('');
const password = ref('');

const { t } = useI18n();

const initResetPassword = () => {
  if (
    unfilledRequired<any>(['email'], { email: email.value }, props.authService.emitter, t, true)
  ) {
    return;
  }
  props.authService.initResetPassword(email.value);
};
const verifyEmail = () => {
  if (
    unfilledRequired<any>(['email'], { email: email.value }, props.authService.emitter, t, true)
  ) {
    return;
  }
  props.authService.verifyEmail(email.value);
};

const stepperItems = [
  {
    label: 'login',
    id: 1,
    icon: 'mdi mdi-account',
  },
];

const loginMenu = computed(() => [
  {
    label: t('more'),
    icon: 'mdi mdi-gesture-tap',
    items: [
      {
        label: t('resetPassword'),
        icon: 'mdi mdi-lock-reset',
        command: initResetPassword,
      },
      {
        label: t('verifyEmail'),
        icon: 'mdi mdi-email-check',
        command: verifyEmail,
      },
    ],
  },
]);
const route = useRoute();
const query = route.query
const login = ()=>{
  props.authService.login({ email:email.value, password:password.value }, true).then(res=>{
    if(query.api){
     return window.close()
    }
    if(res.status === 200){
      location.reload();
    }
  })
}

const goToGithub = ()=>{
  window.open(Urls.GITHUB_REPO)
};
</script>

<template>
  <div class="flex h-full w-full flex-col items-center justify-start bg-west-2 dark:bg-surface-950">
    <div class="flex w-full items-center justify-center text-surface-100">
      <SomeStepper :items="stepperItems" no-predefined-container class="w-full">
        <template #before>
          <Menubar :model="loginMenu" class="w-full" />
        </template>
        <template #hrtStepper1>
          <div class="flex h-fit flex-col items-center justify-center px-[10%] pt-5 xl:pt-10">
            <h1 class="font-bolder mb-2 w-full px-0 text-4xl md:px-20 xl:px-96 flex justify-between items-center">
              <strong>
                <i class="mdi mdi-vector-triangle" />
                {{ $t('QIVI') }}
              </strong>
              <span class="flex gap-2 xl:px-6 px-4 xl:py-1 py-0 rounded-xl ring-1 dark:ring-surface-600/80 ring-forest-0 cursor-pointer hover:bg-forest-100 hover:text-surface-100 dark:hover:text-forest-100 dark:hover:bg-surface-100 transition-colors  "
                    @click="goToGithub()">
                  <i class="mdi mdi-github xl:text-[0.7em] text-[0.5em]" />
                <span class="xl:text-[0.5em] text-[0.4em]">github</span>
              </span>
            </h1>
            <div class="flex w-full flex-col gap-2 px-0 pt-5 md:px-20 xl:px-96 xl:pt-20">
              <label for="email"> {{ $t('email') }}* </label>
              <input-text v-model="email" />
              <label for="email"> {{ $t('password') }}* </label>
              <input-text v-model="password" type="password" />
              <button
                class="mt-2 rounded-xl px-8 py-2 ring-2 ring-mintSplash-200 transition-colors hover:bg-mintSplash-100"
                @click="login"
              >
                <i class="mdi mdi-account-check mr-2" />
                {{ $t('submit') }}
              </button>
            </div>
          </div>
        </template>
      </SomeStepper>
    </div>
  </div>
</template>
