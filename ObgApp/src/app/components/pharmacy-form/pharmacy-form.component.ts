import { Component, OnInit } from '@angular/core';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-pharmacy-form',
    templateUrl: './pharmacy-form.component.html',
    styleUrls: ['./pharmacy-form.component.css']
})
export class PharmacyFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    constructor() { }

    ngOnInit(): void {
    }

}
