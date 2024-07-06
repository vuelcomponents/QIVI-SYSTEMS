<script setup lang="ts">
import type { User } from '@/types/User';
import RadioButton from 'primevue/radiobutton';
import InputNumber from 'primevue/inputnumber';
import { ref } from 'vue';
import UserSessionClock from '@/views/Users/UserSessionClock.vue';
import WeedCard from '@/components/reusable/cards/WeedCard.vue';
import { UseUserStore } from '@/stores/UserStore';

const props = defineProps<{
  user: User;
  showClock?: boolean;
  ownedUser?: boolean;
}>();
const user = UseUserStore().user;
const radioItems = ref<any>(
  [
    {
      title: 'newDeviceEmailVerification',
      description: 'newDeviceEmailVerificationDescription',
      model: 'claimDeviceVerification',
      icon: 'mdi mdi-tablet-cellphone',
    },
    {
      title: 'suppressTokenRefresh',
      description: 'suppressTokenRefreshDescription',
      model: 'suppressTokenRefresh',
      icon: 'mdi mdi-web-refresh',
    },
    props.ownedUser
      ? {
          title: 'suppressSelfSecurityChanges',
          description: 'suppressSelfSecurityChangesDescription',
          model: 'suppressSelfSecurityChanges',
          icon: 'mdi mdi-alert-minus-outline',
        }
      : null,
  ].filter((i) => i !== null)
);
</script>

<template>
  <!-- radios -->
  <section v-for="(item, index) in radioItems" :key="index">
    <WeedCard
      :title="item.title"
      :icon="item.icon"
      :disabled="user.suppressSelfSecurityChanges && !ownedUser && user.role < 2"
      :active="props.user[item.model as keyof User] as boolean"
    >
      <template #description>
        <h1 class="text-[0.7em] lg:text-sm">{{ $t(item.description) }}</h1>
      </template>
      <template #default>
        <div class="grid grid-cols-2">
          <div class="flex items-center justify-start">
            <RadioButton
              class="my-1 lg:my-0"
              v-model="props.user[item.model as keyof User]"
              :inputId="`${item.title}1`"
              :name="item.title"
              :value="true"
              :disabled="user.suppressSelfSecurityChanges && !ownedUser && user.role < 2"
            />
          </div>
          <div class="flex items-center justify-end">
            <label
              for="ingredient1"
              :class="{
                'hidden lg:block': true,
                'font-bold': props.user[item.model as keyof User],
              }"
              >{{ $t('turnedOn') }}</label
            >
          </div>
          <div class="flex items-center justify-start">
            <RadioButton
              class="my-1 lg:my-0"
              v-model="props.user[item.model as keyof User]"
              :inputId="`${item.title}0`"
              :name="item.title"
              :value="false"
              :disabled="user.suppressSelfSecurityChanges && !ownedUser && user.role < 2"
            />
          </div>
          <div class="flex items-center justify-end">
            <label
              for="ingredient2"
              :class="{
                'hidden lg:block': true,
                'font-bold': !props.user[item.model as keyof User],
              }"
              >{{ $t('turnedOff') }}</label
            >
          </div>
        </div>
      </template>
    </WeedCard>
  </section>
  <!-- customs -->
  <WeedCard
    :title="'sessionExpiry'"
    icon="mdi mdi-home-clock-outline"
    :active="true"
    :disabled="user.suppressSelfSecurityChanges && !ownedUser && user.role < 2"
  >
    <template #description>
      <div class="flex gap-2 text-[0.7em] lg:text-sm" v-if="showClock">
        <span class="hidden md:block">{{ $t('validTo') }}</span>
        <UserSessionClock :user="user" view="token" />
        <span class="hidden md:block">{{ $t('refreshIn') }}</span>
        <UserSessionClock :user="user" view="refresh" />
      </div>
      <div v-else>
        <h1 class="text-[0.7em] lg:text-sm">
          {{ $t("The session will refresh upon any activity if the user's online time exceeds") }}
          {{ Math.round(props.user.claimTokenExpiryMinutes * 0.7) }} {{ $t('minutes') }}
          {{ $t('and do not exceeds') }} {{ props.user.claimTokenExpiryMinutes }}
          {{ $t('minutes') }}
        </h1>
      </div>
    </template>
    <template #default>
      <div class="flex items-center justify-start">
        <input-number
          v-model="props.user.claimTokenExpiryMinutes"
          show-buttons
          :min="4"
          :disabled="user.suppressSelfSecurityChanges && !ownedUser && user.role < 2"
          :max="120"
          class="configured-input"
        />
      </div>
    </template>
  </WeedCard>
</template>
