import { Component, OnInit } from '@angular/core';
import { BUY_FORM_URL, BUY_LIST_URL } from '../../utils/routes';

@Component({
    selector: 'app-menu-buy',
    templateUrl: './menu-buy.component.html',
    styleUrls: ['./menu-buy.component.css']
})
export class MenuBuyComponent implements OnInit {

    public addPurchase = `/${BUY_FORM_URL}`;
    public purchaseList = `/${BUY_LIST_URL}`

    constructor() { }

    ngOnInit(): void {
    }

}
