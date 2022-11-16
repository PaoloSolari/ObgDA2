import { Component, OnInit } from '@angular/core';
import { INIT } from 'src/app/utils/routes';

@Component({
    selector: 'app-buy-list',
    templateUrl: './buy-list.component.html',
    styleUrls: ['./buy-list.component.css']
})
export class BuyListComponent implements OnInit {
    
    public backUrl = `/${INIT}`;
    
    constructor() { }

    ngOnInit(): void {
    }

}
