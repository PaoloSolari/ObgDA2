import { Component, OnInit } from '@angular/core';
import { DEMAND_FORM_URL, MEDICINE_FORM_URL, MEDICINE_LIST_URL, BUY_LIST_URL } from '../../utils/routes';

@Component({
    selector: 'app-menu-employee',
    templateUrl: './menu-employee.component.html',
    styleUrls: ['./menu-employee.component.css']
})
export class MenuEmployeeComponent implements OnInit {

    public addMedicine =  `/${MEDICINE_FORM_URL}`;
    public medicineList = `/${MEDICINE_LIST_URL}`;
    public addDemand = `/${DEMAND_FORM_URL}`;
    public purchaseList = `/${BUY_LIST_URL}`;

    constructor() { }

    ngOnInit(): void {
    }

}
