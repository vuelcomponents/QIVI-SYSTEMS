export enum Urls {
  /* HRT AUTH */
  HOST = '192.168.1.77',
  AUTH_CLIENT_PROTOCOL = 'http',
  AUTH_CLIENT_HOST = '192.168.1.77',
  AUTH_CLIENT_PORT = '1810',

  AUTH_SERVER_PROTOCOL = 'http',
  AUTH_SERVER_HOST = '192.168.1.77',
  AUTH_SERVER_PORT = '1910',


  /* HRTECHNIQUE */
  HRTECHNIQUE_CLIENT_PROTOCOL = 'http',
  HRTECHNIQUE_CLIENT_HOST = '192.168.1.77',
  HRTECHNIQUE_CLIENT_PORT = '1830',

  HRTECHNIQUE_SERVER_PROTOCOL = 'http',
  HRTECHNIQUE_SERVER_HOST = '192.168.1.77',
  HRTECHNIQUE_SERVER_PORT = '1930',

  GITHUB_REPO = 'https://github.com/vuelcomponents/QIVI-SYSTEMS'
}
export type Name =
  | 'auth-client'
  | 'auth-server'
  | 'hrtechnique-client'
  | 'hrtechnique-server'
  ;
export const getAssociatedUrl = (name: Name): string => {
  switch (name) {
    case 'auth-client':
      return `${Urls.AUTH_CLIENT_PROTOCOL}://${Urls.AUTH_CLIENT_HOST}:${Urls.AUTH_CLIENT_PORT}`;
    case 'auth-server':
      return `${Urls.AUTH_SERVER_PROTOCOL}://${Urls.AUTH_SERVER_HOST}:${Urls.AUTH_SERVER_PORT}`;
    case 'hrtechnique-client':
      return `${Urls.HRTECHNIQUE_CLIENT_PROTOCOL}://${Urls.HRTECHNIQUE_CLIENT_HOST}:${Urls.HRTECHNIQUE_CLIENT_PORT}`;
    case 'hrtechnique-server':
      return `${Urls.HRTECHNIQUE_SERVER_PROTOCOL}://${Urls.HRTECHNIQUE_SERVER_HOST}:${Urls.HRTECHNIQUE_SERVER_PORT}`;
 }
};
