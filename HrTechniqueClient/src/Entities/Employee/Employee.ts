import type { User } from '@/Entities/User/User';

export type Employee = {
  id:number;
  firstName:string;
  lastName:string;
  dateOfBirth:Date;
  personalId:string;
  personalPhone:string;
  address:string;
  postCode:string;
  city:string;
  files:File[];
  user:User;
}

export const employeeFormStrings = [
  { field: 'firstName', label: 'First name', styles: 'lg:col-span-2 col-span-4', min: 0, max: 50 },
  { field: 'lastName', label: 'Last name', styles: 'lg:col-span-2 col-span-4', min: 0, max: 50 },
  {
    field: 'personalPhone',
    label: 'Personal phone',
    styles: 'lg:col-span-2 col-span-4',
    min: 2,
    max: 16,
  },
  {
    field: 'personalId',
    label: 'Personal ID',
    styles: 'lg:col-span-2 col-span-4',
    min: 2,
    max: 50,
  },
  { field: 'city', label: 'City', styles: 'lg:col-span-1 mb-2 col-span-4', min: 2, max: 50 },
  {
    field: 'postCode',
    label: 'Postcode',
    styles: 'lg:col-span-1 mb-2 col-span-4',
    min: 2,
    max: 20,
  },
  { field: 'address', label: 'Address', styles: 'lg:col-span-2 mb-2 col-span-4', min: 2, max: 100 },
  {
    field: 'companyNip',
    label: 'Company NIP',
    styles: 'lg:col-span-2 col-span-4',
    min: 2,
    max: 50,
  },
  {
    field: 'companyName',
    styles: 'lg:col-span-2 col-span-4',
    label: 'Company name',
    min: 2,
    max: 50,
  },
  {
    field: 'companyAddress',
    label: 'Company Address',
    styles: 'lg:col-span-2 col-span-4',
    min: 2,
    max: 50,
  },
  {
    field: 'companyPhone',
    label: 'Company Phone',
    styles: 'lg:col-span-1 col-span-4',
    min: 2,
    max: 50,
  },
  {
    field: 'website',
    label: 'Company Website',
    styles: 'lg:col-span-1 col-span-4',
    min: 2,
    max: 100,
  },
];
export const employeeCreateFormStrings = [
  { field: 'email', label: 'First name', styles: 'lg:col-span-4 col-span-4', min: 0, max: 50 },
];
export const employeeFormNumbers:Array<{field:string;label:string;styles?:string; min?:number; max?:number}> = [
  // { field: 'price', label: 'Code', styles: 'lg:col-span-2 col-span-4', min: 0, max: 50 },
];
export const employeeUpdateFormNumbers:Array<{field:string;label:string;styles?:string; min?:number; max?:number}> = [
  // { field: 'price', label: 'Code', styles: 'lg:col-span-2 col-span-4', min: 0, max: 50 },
];
export const employeeCreateFormNumbers:Array<{field:string;label:string;styles?:string; min?:number; max?:number}> = [
  // { field: 'price', label: 'Code', styles: 'lg:col-span-2 col-span-4', min: 0, max: 50 },
];
export const employeeFormDates:Array<{field:string;label:string;styles?:string; min?:number; max?:number}> = [
  { field: 'dateOfBirth', label: 'dateOfBirth', styles: 'lg:col-span-2 col-span-4', min: 0, max: 50 },
];
export const employeeUpdateFormDates:Array<{field:string;label:string;styles?:string; min?:number; max?:number}> = [
  // { field: 'price', label: 'Code', styles: 'lg:col-span-2 col-span-4', min: 0, max: 50 },
];
export const employeeCreateFormDates:Array<{field:string;label:string;styles?:string; min?:number; max?:number}> = [
  // { field: 'price', label: 'Code', styles: 'lg:col-span-2 col-span-4', min: 0, max: 50 },
];
export const employeeFormCreateRequired = ['email'];
export const employeeFormUpdateRequired = [];
export const employeeFormGeneralRequired = ['firstName', 'lastName', 'personalPhone'];
