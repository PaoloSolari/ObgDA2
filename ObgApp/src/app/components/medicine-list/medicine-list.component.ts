import { Component, OnInit } from '@angular/core';
import { MedicineService } from '../../services/medicine.service';
import { Medicine } from '../../models/medicine';
import { Router } from '@angular/router';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-medicine-list',
    templateUrl: './medicine-list.component.html',
    styleUrls: ['./medicine-list.component.css']
})
export class MedicineListComponent implements OnInit {

    public backUrl = `/${INIT}`;
    public medicines: Medicine[] = [];

    constructor(
        private _medicinesService: MedicineService,
        private _router: Router,
    ) { }

    public ngOnInit(): void {
        // cuando inicia el componente llamo al servicio para obtener los medicamentos
        this.medicines = this._medicinesService.getMedicines();
    }

    // Cuando el empleado le da click al botón de "Alta de medicamento".
    public navigateToAddMedicine() {
        this._router.navigateByUrl('/medicine/new');
    }

}
