<template>
  <div class="order-last flex w-full flex-col p-8 text-surface-0 xl:order-first">
    <h1 class="font-bolder text-4xl text-surface-700 dark:text-surface-100">
      <strong> {{ $t('Welcome to') }} QUARX </strong>
    </h1>
    <h1 class="mt-4 text-2xl text-surface-700 dark:text-surface-100">
      {{ $t('Empower your company with the best solutions') }}.
    </h1>
    <div class="flex flex-col gap-2">
      <div
        @click="$router.push({ name: 'AboutQuarx' })"
        class="to mt-2 flex h-[50px] w-full cursor-pointer items-center justify-between gap-4 rounded-xl bg-gradient-to-r from-light-600 to-light-700 px-4 ring-2 ring-light-700 transition-colors hover:brightness-105 dark:from-trueGreen-400 dark:to-trueGreen-600 dark:ring-forest-400"
      >
        <span class="flex gap-2">
          <i class="mdi mdi-cloud-lock" />
          <span> QUARX </span>
        </span>
        <p class="text-[0.7em] xl:text-sm">
          {{ $t('Distribution, Access, and Integration System') }}
        </p>
      </div>
      <div
        @click="$router.push({ name: 'AboutHrt' })"
        class="mt-2 flex h-[50px] w-full cursor-pointer items-center justify-between gap-4 rounded-xl bg-mintSplash-200 px-4 text-surface-800 ring-2 ring-mintSplash-200 transition-colors hover:brightness-105"
      >
        <span class="flex gap-2"
          ><i class="mdi mdi-console-network" />
          <span> <strong>HR</strong>Technique </span>
        </span>
        <p class="text-[0.7em] xl:text-sm">
          {{ $t('System for Employment & Recruitment Agencies') }}
        </p>
      </div>
      <div
        @click="$router.push({ name: 'AboutLombandit' })"
        class="mt-2 flex h-[50px] w-full cursor-pointer items-center justify-between gap-4 rounded-xl bg-surface-600 px-4 ring-2 ring-surface-700 transition-colors hover:brightness-105"
      >
        <span class="flex gap-2">
          <i class="mdi mdi-account-cowboy-hat" />
          <span> Lombandit </span>
        </span>
        <p class="text-[0.7em] xl:text-sm">
          {{ $t('Pawn shop system & micro warehouse distribution') }}
        </p>
      </div>
    </div>
    <h1 class="mt-2 text-sm">We live in cloud <i class="mdi mdi-cloud" /></h1>
  </div>
</template>
<script setup lang="ts"></script>
