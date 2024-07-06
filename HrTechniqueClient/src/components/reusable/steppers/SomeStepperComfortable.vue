<script setup lang="ts">
import type { SomeStepperItem } from '@/components/reusable/steppers/SomeStepperItem';
import { ref } from 'vue';

const props = defineProps<{
  items: Array<SomeStepperItem>;
  noPredefinedContainer?: boolean;
  suppressMinHeight?:boolean;
}>();
const emits = defineEmits<{
  onStepChange: [number];
}>();
const activeStep = ref<number>(props.items[0].id);
const changeActiveStep = (id:number) => activeStep.value = id;
defineExpose({
  changeActiveStep
})
</script>

<template>
  <main :class="{ 'h-full ':!suppressMinHeight ,'dark:text-surface-100': !noPredefinedContainer }">
    <section :class="{ 'rounded-lg bg-west-1 p-1 dark:bg-surface-950': !noPredefinedContainer }">
      <div
        class="grid h-full w-full overflow-hidden rounded-md font-mulish "
      >
        <div
          class="max-w-screen flex w-full select-none gap-4 rounded-xl bg-west-2 p-4 ring-1 ring-inset ring-west-3/5 drop-shadow-lg dark:bg-surface-2000"
        >
          <div
            v-for="(item, index) in items"
            :key="index"
            @click="
              () => {
                activeStep = item.id;
                emits('onStepChange', item.id);
              }
            "
            :class="[
              'flex w-full max-w-[20%] items-center justify-center gap-3 rounded-xl p-2 text-sm shadow-sm transition-all duration-150 dark:shadow-md lg:items-start',
              {
                'border-r-2 border-west-3/60 dark:border-r-surface-800':
                  index !== items.length - 1,
              },
              {
                'border-west-3  font-bold dark:border-t-forest-300 dark:text-surface-700':
                  activeStep === item.id,
              },
              activeStep === item.id
                ? 'border-b-2 border-black/20  bg-surface-200 text-west-3 dark:border-b-mintSplash-200/50 dark:text-surface-100'
                : 'cursor-pointer border-b-2 border-b-black/5 bg-west-1 text-light-700  hover:brightness-105 dark:border-b-mintSplash-200/20 dark:bg-surface-1111/50 dark:text-surface-200',
            ]"
          >
            <i :class="item.icon" />
            <span class="hidden lg:block">{{ $t(item.label) }}</span>
          </div>
        </div>
        <div
          class="bg-west-1 py-2 pb-6 font-mulish text-surface-600 dark:bg-surface-950 dark:text-surface-200"
          :style="{ minHeight: suppressMinHeight ? '' : 'calc(100vh - 150px)' }"
          v-for="(item, index) in items"
          :key="index"
          v-show="activeStep === item.id"
        >
          <section class="grid h-full w-full min-w-full"  v-if="activeStep === item.id">
            <slot :name="`hrtStepper${item.id}`" />
          </section>
        </div>
      </div>
    </section>
  </main>
</template>
