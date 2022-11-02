import { Component, Input, OnInit } from '@angular/core';
import { Medicine, PresentationMedicine } from '../../models/medicine';

@Component({
    selector: 'app-medicine-item',
    templateUrl: './medicine-item.component.html',
    styleUrls: ['./medicine-item.component.css']
})
export class MedicineItemComponent implements OnInit {

    @Input() public medicine: Medicine | undefined;

    constructor() { }

    public ngOnInit(): void {
    }

    public getPresentation(presentation: number | undefined): null | string {
        if(presentation == 0) return "Cápsulas"; // PresentationMedicine.capsulas;
        if(presentation == 1) return "Comprimidos"; // PresentationMedicine.comprimidos;
        if(presentation == 2) return "Solución soluble"; // PresentationMedicine.solucionSoluble;
        if(presentation == 3) return "Stick Pack"; // PresentationMedicine.stickPack;
        if(presentation == 4) return "Líquido"; // PresentationMedicine.liquido;
        return null;
    }

}
