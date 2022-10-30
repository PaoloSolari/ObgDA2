import { Component, OnInit } from '@angular/core';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-invitation-list',
    templateUrl: './invitation-list.component.html',
    styleUrls: ['./invitation-list.component.css']
})
export class InvitationListComponent implements OnInit {

    public backUrl = `/${INIT}`;

    constructor() { }

    ngOnInit(): void {
    }

}
