import { PresentationMedicine } from "../models/medicine";

export interface ICreateMedicine {
    Code: string | null;
    Name: string | null;
    SymtompsItTreats: string | null;
    Presentation: PresentationMedicine | null;
    Quantity: number | null;
    Unit: string | null;
    Price: number | null;
    // Stock: number | null; (Default = 0)
    Prescription: boolean | null;
    // IsActive: boolean | null; (Default = false)
}
