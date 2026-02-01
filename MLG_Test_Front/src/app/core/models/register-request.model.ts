export interface RegisterRequest {
  name: string;
  firstLastName: string;
  secondLastName?: string;
  roleId: number;
  email: string;
  password: string;
  addressName: string;
  postalCode: string;
  externalNumber: string;
  internalNumber?: string;
}
