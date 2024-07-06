export type Stats = {
  userStats: Array<UserStats>;
  globalSecuritySettings: number;
};
export type UserStats = {
  id: number;
  monthlyActivities: number;
  email: string;
  devicesCount: number;
  blockedDevicesCount: number;
  blockedIpsCount: number;
  verified: boolean;
  monthlyVisits: number;
  claimTokenExpiryMinutes: number;
  claimDeviceVerification: boolean;
  suppressTokenRefresh: boolean;
  active: boolean;
};
