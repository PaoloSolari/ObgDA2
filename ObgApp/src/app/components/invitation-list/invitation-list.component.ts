import { Component, OnInit } from '@angular/core';
import { InvitationService } from '../../services/invitation.service';
import { Invitation } from '../../models/invitation';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';
import { Router } from '@angular/router';

@Component({
    selector: 'app-invitation-list',
    templateUrl: './invitation-list.component.html',
    styleUrls: ['./invitation-list.component.css']
})
export class InvitationListComponent implements OnInit {

    public backUrl = `/${INIT}`;
    public invitations: Invitation[] = [];
    // public invitations: Invitation[] = this._invitationService.getInvitations();

    displayedColumns: string[] = ['pharmacy', 'userName', 'userRole', 'userCode', 'wasUsed', 'modify'];
    dataSource = this._invitationService.getInvitations();;

    constructor(
        private _invitationService: InvitationService,
        private _router: Router,
    ) { }

    ngOnInit(): void {
        Globals.selectTab = 0;
    }

}
