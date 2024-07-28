import { getAssociatedUrl } from '@/Shared/Resources/urls';
import type { Ref } from 'vue';
import type { AuthService } from '@/Entities/User/AuthService';

export const openKivi = (
  intervalId: Ref<number>,
  proceed: Ref<boolean>,
  authService: AuthService
) => {
  const newWindow = openWindow();

  if (intervalId.value) {
    clearInterval(intervalId.value);
  }
  if (!proceed.value) {
    intervalId.value = setInterval(() => {
      if (newWindow!.closed) {
        clearInterval(intervalId.value);
        proceed.value = false;
        return;
      }

      authService.authorized().then((res) => {
        if (res.status === 200) {
          newWindow!.close();
          location.reload();
        }
      });
    }, 2000);
    proceed.value = true;
    return;
  }

  clearInterval(intervalId.value);

  proceed.value = true;
};
const openWindow = () => {
  const url = `${getAssociatedUrl('auth-client')}/login?api=true`;
  const width = window.screen.width * 0.4;
  const height = window.screen.height * 0.8;
  const left = window.screen.width / 2 - width / 2;
  const top = window.screen.height / 2 - height / 2;
  const newWindow = window.open(
    '',
    'kiviWindow',
    `width=${width},height=${height},top=${top},left=${left},resizable,scrollbars`
  );

  newWindow!.document.write(`
    <!DOCTYPE html>
    <html lang="en">
    <head>
      <meta charset="UTF-8">
      <title>Qivi Auth</title>
      <style>
        body, html {
          margin: 0;
          padding: 0;
          overflow: hidden;
          height: 100%;
        }
        iframe {
          border: none;
          width: 100%;
          height: 100%;
        }
      </style>
    </head>
    <body>
      <iframe src="${url}"></iframe>
    </body>
    </html>
  `);
  return newWindow!;
};
