import { ICreatePurchaseLine } from "./create-purchase-line";

export interface ICreatePurchase {
    PurchaseLines: ICreatePurchaseLine[] | null;
}
