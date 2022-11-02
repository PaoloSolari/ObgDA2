import { PrescriptionMedicine, PresentationMedicine } from "../models/medicine";

export interface ICreateMedicine {
    code: string;
    name: string;
    symtomps: string;
    presentation: string;
    quantity: number;
    unit: string;
    price: number;
    // stock: number; (Default = 0)
    prescription: string;
    // isActive = boolean; (Default = false)
}
