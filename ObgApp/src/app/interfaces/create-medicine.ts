import { PresentationMedicine } from "../models/medicine";

export interface ICreateMedicine {
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
}
