<script setup lang="ts">
import { UseUserStore } from '@/stores/UserStore';
const userStore = UseUserStore();
const user = userStore.user;
</script>

<template>
  <section class="flex h-[300px] w-full justify-between">
    <div>
      <h1>Twoje aplikacje</h1>
    </div>
    <div class="flex gap-2">
      <div v-for="licence in user.licences">
        <div
          class="cursor-pointer rounded-full px-4 py-3 ring-1 transition-colors hover:bg-light-500 dark:ring-mintSplash-200 dark:hover:bg-mintSplash-200"
        >
          <i :class="licence.icon" />
        </div>
      </div>
    </div>
    <div></div>
  </section>
</template>
