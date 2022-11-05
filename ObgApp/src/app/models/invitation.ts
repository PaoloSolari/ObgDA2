export class Invitation {
    idInvitation: string;
    pharmacy: string;
    userRole: string;
    userName: string;
    userCode: number;
    wasUsed: boolean;

    constructor(idInvitation: string, pharmacy: string, userRole: string, userName: string, userCode: number, wasUsed: boolean){
        this.idInvitation = idInvitation;
        this.pharmacy = pharmacy;
        this.userRole = userRole;
        this.userName = userName;
        this.userCode = userCode;
        this.wasUsed = wasUsed;
    }
}
