<script setup lang="ts">
import { UseSettingStore } from '@/stores/SettingStore';
import RadioButton from 'primevue/radiobutton';
import { ref, watch } from 'vue';
const settingStore = UseSettingStore();
const language = ref(settingStore.language);
watch(language, (lang) => {
  settingStore.setLanguage(lang);
});
</script>

<template>
  <main
    class="max-h-[400px] overflow-y-auto p-1 ring-1 ring-surface-400/20 dark:ring-surface-600 sm:max-h-screen"
  >
    <div class="flex flex-col gap-3">
      <div class="align-items-center flex items-center justify-start">
        <RadioButton v-model="language" inputId="language1" name="language" value="pl" />
        <label for="language1" class="ml-2">{{ $t('polish') }}</label>
      </div>
      <div class="align-items-center flex items-center justify-start">
        <RadioButton v-model="language" inputId="language2" name="language" value="en" />
        <label for="language2" class="ml-2">{{ $t('english') }}</label>
      </div>
      <div class="align-items-center flex items-center justify-start">
        <RadioButton v-model="language" inputId="language3" name="language" value="de" />
        <label for="language3" class="ml-2">{{ $t('german') }}</label>
      </div>
      <div class="align-items-center flex items-center justify-start">
        <RadioButton v-model="language" inputId="language4" name="language" value="nl" />
        <label for="language4" class="ml-2">{{ $t('dutch') }}</label>
      </div>
    </div>
  </main>
</template>
