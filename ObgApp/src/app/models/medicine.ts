export enum PresentationMedicine {
    capsulas,
    comprimidos,
    solucionSoluble,
    stickPack,
    liquido
}

export enum PrescriptionMedicine {
    si,
    no
}

export class Medicine {
    code: string;
    name: string;
    symtomps: string;
    presentation: string;
    quantity: number;
    unit: string;
    price: number;
    stock: number;
    prescription: string;
    isActive: boolean;

    constructor(code: string, name:string, symtomps: string, presentation: string, quantity: number, unit: string, price: number, stock: number, prescription: string, isActive: boolean){
        this.code = code;
        this.name = name;
        this.symtomps = symtomps;
        this.presentation = presentation;
        this.quantity = quantity;
        this.unit = unit;
        this.price = price;
        this.stock = stock;
        this.prescription = prescription;
        this.isActive = isActive;
    }
}