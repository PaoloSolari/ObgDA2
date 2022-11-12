import { Pharmacy } from "./pharmacy";
import { RoleUser } from "./user";

export class Invitation {
    idInvitation: string | null;
    pharmacy: Pharmacy | null;
    userRole: RoleUser | null;
    userName: string | null;
    userCode: number | null;
    wasUsed: boolean | null;

    constructor(idInvitation: string | null, pharmacy: Pharmacy | null, userRole: RoleUser | null, userName: string | null, userCode: number | null, wasUsed: boolean | null) {
        this.idInvitation = idInvitation;
        this.pharmacy = pharmacy;
        this.userRole = userRole;
        this.userName = userName;
        this.userCode = userCode;
        this.wasUsed = wasUsed;
    }
}
