import { router } from '../router/router';
import { getAssociatedUrl } from '@/variables/urls';

export function KickUnauthorized(target: any, propertyKey: string, descriptor: PropertyDescriptor) {
  const originalMethod = descriptor.value;
  descriptor.value = async function (...args: any[]) {
    const result = await originalMethod.apply(this, args);
    console.log('REZULT', result);
    if (result?.response?.status === 401) {
     return location.href = getAssociatedUrl('auth-client')
    }
    if (result?.response?.status === 404) {
      router.replace({ name: 'ServerDown' });
    }
    return result;
  };
  return descriptor;
}
