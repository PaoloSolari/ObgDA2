import { Pharmacy } from "../models/pharmacy";
import { RoleUser } from "../models/user";

export interface ICreateInvitation {
    // IdInvitation: string | null; (Default = desde BackEnd)
    Pharmacy: Pharmacy | null;
    UserRole: RoleUser | null;
    UserName: string | null;
    // UserCode: number | null; (Default = desde BackEnd)
    // WasUsed: boolean | null; (Default = false)
}

