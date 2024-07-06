import { i18n } from '@/variables/translate';

export default () => {
  return [
    {
      headerCheckboxSelection: true,
      checkboxSelection: true,
      showDisabledCheckboxes: true,
      filter: false,
      width: 55,
      editable: false,
      minWidth: 55,
    },
    {
      headerName: i18n.global.t('id'),
      field: 'id',
      valueGetter: (params: any) => `${params.data.blocked ? '🛇 ' : ''}# ${params.data.id}`,
      minWidth: 120,
      width: 120,
      headerClass: 'ag-right-aligned-header',
      cellStyle: { textAlign: 'right' },
      cellClass: (params: any) =>
        `ag-right-aligned-cell ${params.data.blocked ? 'text-red-500' : ''}`,
    },
    {
      headerName: '',
      minWidth: 70,
      field: 'updateUser',
      width: 70,
      headerClass: 'centered',
      cellStyle: { display: 'flex', justifyContent: 'center' },
      cellRenderer: 'PreviewButtonRenderer',
    },
    {
      headerName: i18n.global.t('email'),
      field: 'email',
      flex: 1,
    },
    {
      headerName: i18n.global.t('firstName'),
      field: 'firstName',
      flex: 1,
    },
    {
      headerName: i18n.global.t('lastName'),
      field: 'lastName',
      flex: 1,
    },
    {
      headerName: i18n.global.t('verified'),
      field: 'verified',
      minWidth: 150,
      width: 150,
      headerClass: 'centered',
      cellStyle: { display: 'flex', justifyContent: 'center', alignItems: 'center' },
      cellRenderer: 'StatusRenderer',
    },
  ];
};
