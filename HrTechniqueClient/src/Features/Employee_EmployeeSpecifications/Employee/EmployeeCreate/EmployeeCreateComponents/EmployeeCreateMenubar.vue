<script setup lang="ts">

import Menubar from 'primevue/menubar';
import { computed } from 'vue';
import { useI18n } from 'vue-i18n';
const { t } = useI18n({});
const items = computed(()=>[
  {
    label: t('saveNewEmployee'),
    icon: 'mdi mdi-content-save',
    command: ()=> emit('onSave'),
  },
  {
    label: t('close'),
    icon: 'mdi mdi-cancel',
    command: () => emit('onClose'),
  },
]);

const emit = defineEmits<{
  onSave:[],
  onClose:[]
}>();
</script>

<template>
  <Menubar :model="items" class="mb-2 w-full" />
</template>