import { PresentationMedicine } from "../models/medicine";

export interface ICreateMedicine {
    code: string;
    name: string;
    symtompsItTreats: string;
    presentation: PresentationMedicine;
    quantity: number;
    unit: string;
    price: number;
    // stock: number; (Default = 0)
    prescription: boolean;
    // isActive = boolean; (Default = false)
}
