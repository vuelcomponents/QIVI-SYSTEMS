<script setup lang="ts"></script>

<template>
  <section
    class="ext-[0.9em] relative w-full rounded-lg ring-1 ring-surface-200 dark:bg-[#0f172a] dark:ring-surface-700"
  >
    <header
      class="h-[30]px flex items-center gap-2 rounded-t-lg border-b-[1px] border-surface-200 bg-light-700 px-4 py-3 dark:border-surface-700 dark:bg-surface-800"
    >
      <slot name="header" />
    </header>
    <div class="min-h-[200px] w-full">
      <slot name="content" />
    </div>
    <footer class="absolute bottom-0 h-[30px] w-full">
      <slot name="footer" />
    </footer>
  </section>
</template>

<style scoped></style>
