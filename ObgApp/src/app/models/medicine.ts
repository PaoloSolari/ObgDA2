export enum PresentationMedicine {
    capsulas,
    comprimidos,
    solucionSoluble,
    stickPack,
    liquido
}

export class Medicine {
    code: string | null;
    name: string | null;
    symtompsItTreats: string | null;
    presentation: PresentationMedicine | null;
    quantity: number | null;
    unit: string | null;
    price: number | null;
    stock: number | null;
    prescription: boolean | null;
    isActive: boolean | null;

    constructor(code: string | null, name: string | null, symtompsItTreats: string | null, presentation: PresentationMedicine | null, quantity: number | null, unit: string | null, price: number | null, stock: number | null, prescription: boolean | null, isActive: boolean | null) {
        this.code = code;
        this.name = name;
        this.symtompsItTreats = symtompsItTreats;
        this.presentation = presentation;
        this.quantity = quantity;
        this.unit = unit;
        this.price = price;
        this.stock = stock;
        this.prescription = prescription;
        this.isActive = isActive;
    }
}