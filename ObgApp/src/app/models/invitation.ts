import { Pharmacy } from "./pharmacy";

export class Invitation {
    IdInvitation: string | null;
    Pharmacy: Pharmacy | null;
    UserRole: string | null;
    UserName: string | null;
    UserCode: number | null;
    WasUsed: boolean | null;

    constructor(IdInvitation: string | null, Pharmacy: Pharmacy | null, UserRole: string | null, UserName: string | null, UserCode: number | null, WasUsed: boolean | null) {
        this.IdInvitation = IdInvitation;
        this.Pharmacy = Pharmacy;
        this.UserRole = UserRole;
        this.UserName = UserName;
        this.UserCode = UserCode;
        this.WasUsed = WasUsed;
    }
}
