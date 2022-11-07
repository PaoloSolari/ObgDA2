import { Pharmacy } from "../models/pharmacy";

export interface ICreateInvitation {
    // IdInvitation: string | null; (Default = desde BackEnd)
    Pharmacy: Pharmacy | null;
    UserRole: string | null;
    UserName: string | null;
    // UserCode: number | null; (Default = desde BackEnd)
    // WasUsed: boolean | null; (Default = false)
}

