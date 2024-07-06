import { router } from '../router/router';

export function KickUnauthorized(target: any, propertyKey: string, descriptor: PropertyDescriptor) {
  const originalMethod = descriptor.value;
  descriptor.value = async function (...args: any[]) {
    const result = await originalMethod.apply(this, args);
    if (result?.response?.status === 401) {
      await router.replace({ name: 'login' });
    }
    if (result?.response?.status === 404) {
      router.replace({ name: 'ServerDown' });
    }
    return result;
  };
  return descriptor;
}
