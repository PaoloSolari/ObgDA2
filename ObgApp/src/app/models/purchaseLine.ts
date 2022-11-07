export enum PurchaseLineStatus {
    Accepted,
    Rejected,
    UnResolved
}

export class PurchaseLine {

    IdPurchaseLine: string | null;
    medicineCode: string | null;
    MedicineQuantity: number | null;
    Status: PurchaseLineStatus | null;

    constructor(IdPurchaseLine: string | null, medicineCode: string | null, MedicineQuantity: number | null, Status: PurchaseLineStatus | null) {
        this.IdPurchaseLine = IdPurchaseLine;
        this.medicineCode = medicineCode;
        this.MedicineQuantity = MedicineQuantity;
        this.Status = Status;
    }
}
