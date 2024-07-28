import type { TinyEmitter } from 'tiny-emitter';

export const unfilledRequired = <T>(
  fields: Array<string>,
  obj: T,
  emitter?: TinyEmitter,
  t?: any,
  blink?: boolean
) => {
  const unfilledRequired = fields.filter(
    (_key: string) => (!obj[_key as keyof T] || obj[_key as keyof T] === '') && obj[_key as keyof T] !== 0
  );

  if (blink) {
    unfilledRequired.forEach((field) => {
      const el = document.getElementById(`hrt-form-${field}`);
      if (el) {
        const _t = el.style.transition;
        const _b = el.style.background;
        el.style.transition = 'all 0.5s';
        el.style.background = '#b5242f30';
        setTimeout(() => {
          el.style.background = _b;
          el.style.transition = _t;
        }, 500);
      }
    });
  }

  if (t) {
    unfilledRequired.forEach((_: string, i: number) => {
      unfilledRequired[i] = t(unfilledRequired[i]);
    });
  }

  if (unfilledRequired.length > 0) {
    emitter?.emit('error', {
      severity: 'error',
      summary: t ? t(`emptyRequiredFields`) : `Empty required fields`,
      detail: unfilledRequired.join(', '),
      life: 3000,
    });
  }
  return unfilledRequired.length > 0;
};
