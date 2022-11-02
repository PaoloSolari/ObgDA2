import { Component, Input, OnInit } from '@angular/core';
import { MedicineService } from '../../services/medicine.service';
import { Medicine } from '../../models/medicine';
import { Router } from '@angular/router';
import { INIT } from '../../utils/routes';
import { Globals } from '../../utils/globals';

@Component({
    selector: 'app-medicine-list',
    templateUrl: './medicine-list.component.html',
    styleUrls: ['./medicine-list.component.css']
})
export class MedicineListComponent implements OnInit {

    public backUrl = `/${INIT}`;
    public medicines: Medicine[] = [];
    // public medicines: Medicine[] = this._medicinesService.getMedicines();
    
    displayedColumns: string[] = ['code', 'name', 'price', 'presentation', 'delete'];
    dataSource = this._medicinesService.getMedicines();;

    constructor(
        private _medicinesService: MedicineService,
        private _router: Router,
    ) { }

    public ngOnInit(): void {
        // cuando inicia el componente llamo al servicio para obtener los medicamentos
        // this.medicines = this._medicinesService.getMedicines();
        Globals.selectTab = 2;
    }

    // Cuando el empleado le da click al botón de "Alta de medicamento".
    public navigateToAddMedicine() {
        this._router.navigateByUrl('/medicine/new');
    }

}
