<script setup lang="ts">
import LeftSideBar from '@/components/referenced/panels/LeftSideBar.vue';
import { useRoute } from 'vue-router';
import { router } from '@/router/router';
const route = useRoute();

if (route.query.err || route.query.inf) {
  setTimeout(() => {
    router.replace({ query: undefined });
  });
}
</script>

<template>
  <main class="flex w-full overflow-hidden">
    <LeftSideBar />
    <section class="-mt-[3px] w-full bg-west-2 pr-[1px] dark:bg-black/0">
      <router-view />
    </section>
  </main>
</template>

<style scoped></style>
