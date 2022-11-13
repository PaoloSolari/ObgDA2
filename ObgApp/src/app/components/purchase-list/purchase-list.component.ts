import { Component, OnInit } from '@angular/core';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-purchase-list',
    templateUrl: './purchase-list.component.html',
    styleUrls: ['./purchase-list.component.css']
})
export class PurchaseListComponent implements OnInit {

    public backUrl = `/${INIT}`;

    constructor() { }

    ngOnInit(): void {
        Globals.selectTab = 2;
    }

}