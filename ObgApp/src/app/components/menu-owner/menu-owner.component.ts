import { Component, OnInit } from '@angular/core';
import { DEMAND_LIST_URL } from '../../utils/routes';

@Component({
    selector: 'app-menu-owner',
    templateUrl: './menu-owner.component.html',
    styleUrls: ['./menu-owner.component.css']
})
export class MenuOwnerComponent implements OnInit {

    public demandList = `/${DEMAND_LIST_URL}`;

    constructor() { }

    ngOnInit(): void {
    }

}
