import { PurchaseLine } from "./purchaseLine";

export class Purchase {

    IdPurchase: string | null;
    Amount: number | null;
    BuyerEmail: string | null;
    PurchaseLines: PurchaseLine[] | null;
    IsConfirmed: boolean | null;

    constructor(IdPurchase: string | null, Amount: number | null, BuyerEmail: string | null, PurchaseLines: PurchaseLine[] | null, IsConfirmed: boolean | null) {
        this.IdPurchase = IdPurchase;
        this.Amount = Amount;
        this.BuyerEmail = BuyerEmail;
        this.PurchaseLines = PurchaseLines;
        this.IsConfirmed = IsConfirmed;
    }
}

// [Key] public string IdPurchase { get; set; }
// public double Amount { get; set; }
// public string BuyerEmail { get; set; }
// public List<PurchaseLine> PurchaseLines { get; set; }
// public bool IsConfirmed { get; set; }