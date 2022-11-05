import { PrescriptionMedicine, PresentationMedicine } from "../models/medicine";

export interface ICreateInvitation {
    // idInvitation: string; (Default)
    pharmacy: string;
    userRole: string;
    userName: string;
    // userCode: number; (Default = en función del 'userName')
    // wasUsed: boolean; (Default = false)
}

