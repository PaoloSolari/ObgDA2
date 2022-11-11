export enum PurchaseLineStatus {
    accepted,
    rejected,
    unResolved
}

export class PurchaseLine {

    idPurchaseLine: string | null;
    medicineCode: string | null;
    medicineQuantity: number | null;
    tatus: PurchaseLineStatus | null;

    constructor(idPurchaseLine: string | null, medicineCode: string | null, medicineQuantity: number | null, status: PurchaseLineStatus | null) {
        this.idPurchaseLine = idPurchaseLine;
        this.medicineCode = medicineCode;
        this.medicineQuantity = medicineQuantity;
        this.tatus = status;
    }
}
