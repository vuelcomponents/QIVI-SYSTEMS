import { fileURLToPath, URL } from 'node:url';

import plugin from '@vitejs/plugin-vue';

import { defineConfig } from 'vite';
import { Urls } from './src/variables/urls';



// https://vitejs.dev/config/
export default defineConfig({
  plugins: [plugin()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port: Urls.AUTH_CLIENT_PORT
  }
});
