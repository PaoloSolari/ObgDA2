import { Component, OnInit } from '@angular/core';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-invitation-form',
    templateUrl: './invitation-form.component.html',
    styleUrls: ['./invitation-form.component.css']
})
export class InvitationFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    constructor() { }

    ngOnInit(): void {
    }

}
