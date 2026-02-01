export interface StoreItem extends StoreItemBase {
  id: number;
  storeName: string;
  itemCode: string;
  itemName: string;
  itemImage: string;
}

export interface StoreItemBase {
  storeId: number;
  itemId: number;
  price: number;
  stock: number;
}
