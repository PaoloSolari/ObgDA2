import { Component, OnInit } from '@angular/core';
import { ADD_INVITATION_URL, INVITATION_FORM_URL, INVITATION_LIST_URL, PHARMACY_FORM_URL } from '../../utils/routes';

@Component({
    selector: 'app-menu-administrator',
    templateUrl: './menu-administrator.component.html',
    styleUrls: ['./menu-administrator.component.css']
})
export class MenuAdministratorComponent implements OnInit {

    public addPharmacy = `/${PHARMACY_FORM_URL}`;
    public addInvitation = `/${ADD_INVITATION_URL}`;
    // public addInvitation = `/${INVITATION_FORM_URL}`;
    public invitationList = `/${INVITATION_LIST_URL}`;

    constructor() { }

    ngOnInit(): void {
    }

}
