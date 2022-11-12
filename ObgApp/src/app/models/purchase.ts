import { PurchaseLine } from "./purchaseLine";

export class Purchase {

    idPurchase: string | null;
    amount: number | null;
    buyerEmail: string | null;
    purchaseLines: PurchaseLine[] | null;
    isConfirmed: boolean | null;

    constructor(idPurchase: string | null, amount: number | null, buyerEmail: string | null, purchaseLines: PurchaseLine[] | null, isConfirmed: boolean | null) {
        this.idPurchase = idPurchase;
        this.amount = amount;
        this.buyerEmail = buyerEmail;
        this.purchaseLines = purchaseLines;
        this.isConfirmed = isConfirmed;
    }
}