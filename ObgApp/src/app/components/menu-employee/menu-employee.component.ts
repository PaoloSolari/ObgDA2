import { Component, OnInit } from '@angular/core';
import { DEMAND_FORM_URL, EXPORTER_FORM_URL, MEDICINE_FORM_URL, MEDICINE_LIST_URL, PURCHASE_LIST_URL } from '../../utils/routes';

@Component({
    selector: 'app-menu-employee',
    templateUrl: './menu-employee.component.html',
    styleUrls: ['./menu-employee.component.css']
})
export class MenuEmployeeComponent implements OnInit {

    public addMedicine =  `/${MEDICINE_FORM_URL}`;
    public medicineList = `/${MEDICINE_LIST_URL}`;
    public addDemand = `/${DEMAND_FORM_URL}`;
    public purchaseList = `/${PURCHASE_LIST_URL}`;
    public exportMedicines = `/${EXPORTER_FORM_URL}`;

    constructor() { }

    ngOnInit(): void {
    }

}
