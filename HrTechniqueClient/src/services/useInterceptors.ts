import type { AxiosError, AxiosInstance, AxiosResponse, InternalAxiosRequestConfig } from 'axios';

interface InterceptorParams {
  client: AxiosInstance;
  emitter: any;
  t: any;
}
let sent = false;
const constructErrorDetail = (error: any, t: any): any => {
  if (typeof error === 'string') {
    return t(error);
  }
  if (error?.errors) {
    return Array.from(Object.values(error.errors)).join(', ');
  }
  return '';
};

export default (params: InterceptorParams) => {
  params.client.interceptors.request.use(
    (req: InternalAxiosRequestConfig): InternalAxiosRequestConfig => {
      if (params.emitter) {
        params.emitter.emit('load', true);
      }
      sent = false;
      return req;
    }
  );
  params.client.interceptors.response.use(
    (res: AxiosResponse): AxiosResponse => {
      if (params.emitter) {
        params.emitter.emit('load', false);
      }
      switch (res?.status) {
        case 200:
          switch (res.config?.method) {
            case 'patch':
              if (!sent) {
                params.emitter.emit('info', {
                  severity: 'info',
                  summary: params.t('rowHasBeenUpdated'),
                  detail: '',
                  life: 3000,
                });
                sent = true;
              }
              break;
          }
          break;
        default:
          break;
      }
      return res;
    },
    (error: AxiosError) => {
      params.emitter.emit('load', false);
      if (params.emitter) {
        switch (error?.response?.status) {
          case 403:
            params.emitter.emit('error', {
              severity: 'error',
              summary: `privileges`,
              detail: `unsufficientPrivileges`,
              life: 3000,
            });
            break;
          case 303:
            params.emitter.emit('info', {
              severity: 'info',
              summary: params.t(`${error.message}`),
              detail: constructErrorDetail(error.response?.data, params.t),
              life: 3000,
            });
            break;
          case 401:
            break;
          default:
            params.emitter.emit('error', {
              severity: 'error',
              summary: params.t(`${error.message}`),
              detail: constructErrorDetail(error.response?.data, params.t),
              life: 3000,
            });
        }
      }

      return error;
    }
  );
};
