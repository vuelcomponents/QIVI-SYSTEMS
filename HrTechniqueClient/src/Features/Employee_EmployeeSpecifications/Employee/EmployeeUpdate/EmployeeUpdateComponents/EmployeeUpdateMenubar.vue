<script setup lang="ts">
import Menubar from 'primevue/menubar';
import { computed } from 'vue';
import { useI18n } from 'vue-i18n';
const { t } = useI18n();
const emit = defineEmits<{
  onSave:[];
  onClose:[];
}>()
const actionItems = computed(() => [
  {
    label: t('saveChanges'),
    icon: 'mdi mdi-content-save',
    command: () => emit('onSave'),
  },
  {
    label: t('actions'),
    icon: 'mdi mdi-wrench',
    items:[]
  },
  {
    label: t('close'),
    icon: 'mdi mdi-cancel',
    command: () => emit('onClose'),
  },
]);
</script>

<template>
  <Menubar :model="actionItems" class="mb-2 w-full" />
</template>
