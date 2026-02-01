export interface Item extends ItemBase {
  id: number;
}

export interface ItemBase {
  code: string;
  description: string;
  image?: string;
}
