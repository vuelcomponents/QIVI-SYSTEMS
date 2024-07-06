import type { Role } from '@/types/Role';
import type { IdCode } from '@/types/IdCode';

export interface UserUpdate {
  [key: string]: any;
}

export type User = {
  id: number;
  email: string;
  password: string;
  licences: Licence[];
  hrtLicence: boolean;
  lombanditLicence: boolean;
  signedInDateTime: Date;
  role: Role;
  notifications: NotificationType[];
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  personalId: string;
  suppressSelfSecurityChanges: boolean;
  gender: IdCode;
  personalPhone: string;
  address: string;
  postCode: string;
  city: string;
  companyNip: string;
  companyName: string;
  companyAddress: string;
  companyPhone: string;
  website: string;
  users: Array<User>;
  notified:boolean;
  userId: number;
  claimDeviceVerification: boolean;
  lockedChoiceClaimDeviceVerification: boolean;
  claimTokenExpiryMinutes: number;
  lockedClaimTokenExpiryMinutes: boolean;
  suppressTokenRefresh: boolean;
  devices: Array<Device>;
  maxUsersCount: number;
};
export type NotificationType = {
  id: number;
  code: string;
  description: string;
  timeStamp: Date;
  seen: boolean;
};
export type Licence = {
  id: number;
  code: string;
  endTimeStamp: Date;
  startTimeStamp: Date;
  icon: string;
  host: string;
};
export type Device = {
  id: number;
  ip: string;
  userAgent: string;
  active: boolean;
  blocked: boolean;
  ipBlocked: boolean;
  verified: boolean;
  signedInDateTime: Date;
};
export const userFormStrings = [
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
export const userCreateFormStrings = [
  { field: 'email', label: 'First name', styles: 'lg:col-span-4 col-span-4', min: 0, max: 50 },
];
// export const userFormBooleans = [
//   "dataLicence",
//   "recruitLicence",
//   "hiringLicence",
//   "lombanditLicence",
// ]
// export const userFormObjects = [
//   "gender"
// ]
// export const userFormLists =[
//   "users"
// ]
// export const userFormInts = [
//   "role",
//   "userId",
// ]
export const userFormCreateRequired = ['email'];
export const userFormUpdateRequired = [];
export const userFormGeneralRequired = ['firstName', 'lastName', 'personalPhone'];
