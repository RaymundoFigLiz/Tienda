export interface Store extends StoreBase {
  id: number;
}

export interface StoreBase {
  name: string;
  address: string;
  postalCode: string;
  externalNumber: string;
  internalNumber?: string;
}
