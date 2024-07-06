<script setup lang="ts">
import { onMounted, onUnmounted } from 'vue';

const emit = defineEmits<{
  shut: [];
}>();
const props = defineProps<{
  name: string;
  label: string;
  icon: string;
}>();

const neutralClose = (e: MouseEvent) => {
  let targetElement: HTMLElement = e.target as HTMLElement;
  while (targetElement) {
    if (targetElement.classList.contains(props.name)) {
      return;
    }
    targetElement = targetElement.parentElement as HTMLElement;
  }
  emit('shut');
};

onMounted(() => {
  document.addEventListener('click', neutralClose);
});

onUnmounted(() => {
  document.removeEventListener('click', neutralClose);
});
</script>

<template>
  <div class="xs:left-0 fixed left-0 z-10 mt-1 w-full sm:left-0 md:left-0 lg:absolute">
    <div
      :class="[
        props.name,
        'xs:w-[100vw] absolute w-full   text-wrap rounded-md bg-surface-0 dark:bg-surface-1111  sm:w-[100vw] md:w-[100vw] lg:w-full xl:w-full',
      ]"
    >
      <section :class="[props.name, '']">
        <section
          class="h-full rounded-xl bg-surface-0 pb-2 shadow-md dark:bg-surface-1111 sm:w-full md:w-full lg:w-[20vw] xl:w-[220px]"
        >
          <div
            class="text-md p-4 font-dms text-surface-700 shadow-sm ring-1 ring-surface-200/10 dark:text-surface-0 dark:ring-surface-100/5"
          >
            <h1>
              <strong>
                <i :class="icon"></i>
                {{ label }}
              </strong>
            </h1>
          </div>
          <slot> </slot>
          <div
            class="group relative mx-4 mt-2 flex cursor-pointer items-center justify-center rounded-xl bg-surface-700/5 px-4 py-2 font-dms text-sm text-surface-700/60 shadow-sm ring-1 ring-surface-200/10 dark:bg-surface-100/5 dark:text-surface-200/80 dark:ring-surface-100/60"
            @click="emit('shut')"
          >
            <div
              class="flex items-center justify-center text-[0.8em] transition-all group-hover:text-surface-700 dark:group-hover:text-surface-100"
            >
              <strong> <i class="mdi mdi-progress-close mr-2" />{{ $t('close') }} </strong>
            </div>
          </div>
        </section>
      </section>
    </div>
  </div>
</template>
