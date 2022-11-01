export enum PurchaseLineStatus {
    accepted,
    rejected,
    unResolved
}

export class PurchaseLine {

    idPurchaseLine: string;
    medicineCode: string;
    medicineQuantity: number;
    status: PurchaseLineStatus;

    constructor(idPurchaseLine: string, medicineCode:string, medicineQuantity: number, status: PurchaseLineStatus) {
        this.idPurchaseLine = idPurchaseLine;
        this.medicineCode = medicineCode;
        this.medicineQuantity = medicineQuantity;
        this.status = status;
    }
}