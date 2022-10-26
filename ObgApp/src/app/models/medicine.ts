export enum PresentationMedicine {
    capsulas,
    comprimidos,
    solucionSoluble,
    stickPack,
    liquido
}
export class Medicine {
    code: string;
    name: string;
    symtompsItTreats: string;
    presentation: PresentationMedicine;
    quantity: number;
    unit: string;
    price: number; // ¿double == number?
    stock: number;
    prescription: boolean;
    isActive: boolean;

    constructor(code: string, name:string, symtompsItTreats: string, presentation: PresentationMedicine, quantity: number, unit: string, price: number, stock: number, prescription: boolean, isActive: boolean){
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