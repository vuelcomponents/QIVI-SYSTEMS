
export default (t: Function) => [
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
    field: 'id',
    headerName: t('id'),
    width:150,
    headerClass: 'ag-right-aligned-header',
    cellStyle: { textAlign: 'right' },
    cellClass: 'ag-right-aligned-cell',
  },
  {
    headerName: '',
    minWidth: 70,
    field: 'updateEmployee',
    width: 70,
    headerClass: 'centered',
    cellStyle: { display: 'flex', justifyContent: 'center' },
    cellRenderer: 'PreviewButtonRenderer',
  },
  {
    field: 'firstName',
    headerName: t('firstName'),
    width:150
  },
  {
    field: 'lastName',
    headerName: t('lastName'),
    flex:1
  },
  {
    field: 'dateOfBirth',
    headerName: t('dateOfBirth'),
    width:150,
    headerClass: 'ag-right-aligned-header',
    cellStyle: { textAlign: 'right' },
    cellClass: 'ag-right-aligned-cell',
  },
  {
    field: 'personalId',
      headerName: t('personalId'),
    width:150
  },
  {
    field: 'personalPhone',
      headerName: t('personalPhone'),
    flex:1
  },
  {
    field: 'city',
      headerName: t('city'),
    width:150
  },
  {
    field: 'address',
      headerName: t('address'),
    flex:1
  },
  {
    field: 'postCode',
      headerName: t('postcode'),
    width:150,
    headerClass: 'ag-right-aligned-header',
    cellStyle: { textAlign: 'right' },
    cellClass: 'ag-right-aligned-cell',
  },
  {
    field: 'user',
    valueGetter:(params:any)=>params.data.user.id,
    headerName: t('userId'),
    headerClass: 'ag-right-aligned-header',
    cellStyle: { textAlign: 'right' },
    cellClass: 'ag-right-aligned-cell',
    width:150
  },

];
