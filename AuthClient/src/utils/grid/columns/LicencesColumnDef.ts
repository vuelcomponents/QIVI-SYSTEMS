import { licencesMap } from '@/utils/libs/licences';

export default (t: Function) => [
  {
    field: 'code',
    headerName: t('code'),
    flex: 1,
  },
  {
    field: 'host',
    headerName: t('host'),
    flex: 1,
  },
  {
    field: 'maxUsersQty',
    headerName: t('licenceUserQty'),
    flex: 1,
  },
  {
    field: 'software',
    headerName: t('software'),
    valueGetter: (params: any) => licencesMap[params.data.software as 0 | 1],
    flex: 1,
  },
  {
    field: 'startTimeStamp',
    headerName: t('licenceStart'),
    flex: 1,
  },
  {
    field: 'endTimeStamp',
    headerName: t('licenceEnd'),
    flex: 1,
  },
];
