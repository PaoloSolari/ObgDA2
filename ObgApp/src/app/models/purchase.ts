// [Key] public string IdPurchase { get; set; }
//         public double Amount { get; set; }
//         public string BuyerEmail { get; set; }
//         public List<PurchaseLine> PurchaseLines { get; set; }
//         public bool IsConfirmed { get; set; }

import { PurchaseLine } from "./purchaseLine";

export class Purchase {

    idPurchase: string;
    amount: number;
    buyerEmail: string
    purchaseLines: PurchaseLine[] | undefined;
    isConfirmed: boolean;

    constructor(idPurchase: string, amount: number, buyerEmail: string, purchaseLines: PurchaseLine[] | undefined, isConfirmed: boolean) {
        this.idPurchase = idPurchase;
        this.amount = amount;
        this.buyerEmail = buyerEmail;
        this.purchaseLines = purchaseLines;
        this.isConfirmed = isConfirmed;
    }
}