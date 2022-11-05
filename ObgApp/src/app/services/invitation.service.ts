import { Injectable } from '@angular/core';
import { ICreateInvitation } from '../interfaces/create-invitation';
import { Invitation } from '../models/invitation';
import { Pharmacy } from '../models/pharmacy';

@Injectable({
    providedIn: 'root'
})
export class InvitationService {

    private _invitations: Invitation[] | undefined;

    constructor(
        // De momento nada...
    ) {
        this._invitations = this.initializeInvitations();
    }

    private initializeInvitations(): Invitation[] {
        return [
            new Invitation('ABCDEF', 'FarmaShop', 'Empleado', 'José', 123456, false),
            new Invitation('ABCDEF', 'San Roque', 'Dueño', 'José', 123456, true),
        ]
    }

    public getInvitations(): Invitation[] {
        return this._invitations ?? [];
    }

    public postInvitation(invitation: ICreateInvitation): string {
        if (!this._invitations) this._invitations = [];
        const idInvitation = this.makeString(); // Default.
        const userCode = 123456; // Default (en función de 'invitation.userName').
        const wasUsed = false; // Default.
        const invitationToAdd = new Invitation(idInvitation, invitation.pharmacy, invitation.userRole, invitation.userName, userCode, wasUsed);
        this._invitations.push(invitationToAdd);
        return idInvitation;
    }

    makeString(): string {
        let outString: string = '';
        let inOptions: string = 'abcdefghijklmnopqrstuvwxyz0123456789';

        for (let i = 0; i < 32; i++) {

            outString += inOptions.charAt(Math.floor(Math.random() * inOptions.length));

        }

        return outString;
    }

    result: string = this.makeString();

}
