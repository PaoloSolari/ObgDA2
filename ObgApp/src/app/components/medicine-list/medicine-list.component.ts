import { Component, Input, OnInit } from '@angular/core';
import { MedicineService } from '../../services/medicine.service';
import { Medicine, PresentationMedicine } from '../../models/medicine';
import { Router } from '@angular/router';
import { INIT, MEDICINE_LIST_URL } from '../../utils/routes';
import { Globals } from '../../utils/globals';
import { catchError, take, filter, of} from 'rxjs';
import { IDeleteResponse } from '../../interfaces/delete-response.interface';
import { Employee } from 'src/app/models/employee';

@Component({
    selector: 'app-medicine-list',
    templateUrl: './medicine-list.component.html',
    styleUrls: ['./medicine-list.component.css']
})
export class MedicineListComponent implements OnInit {

    public backUrl = `/${INIT}`;
    public medicines: Medicine[] = [];
    public actualEmployee: Employee = new Employee(null, null, null, null, null, null, null, null);
    // public medicines: Medicine[] = this._medicineService.getMedicines();
    
    displayedColumns: string[] = ['code', 'name', 'price', 'presentation', 'stock', 'delete'];
    // dataSource = this._medicineService.getMedicines(); // (#)
    public dataSource = this.medicines;

    constructor(
        private _medicineService: MedicineService,
        private _router: Router,
    ) { }

    public ngOnInit(): void {

        Globals.selectTab = 2;
        
        // [Obtengo el empleado actual]
        this.actualEmployee.name = 'Paolo'; // (#)

        // [Me traigo los medicamentos de la farmacia del empleado]
        this._medicineService.getMedicines(this.actualEmployee.name)
        .pipe(
            take(1),
            catchError((err => {
                if(err.status != 200){
                    alert(`${err.error.errorMessage}`);
                    console.log(`Error: ${err.error.errorMessage}`)
                } else {
                    console.log(`Ok: ${err.error.text}`);
                }
                return of(err);
            }))
        )
            .subscribe((medicines: Medicine[]) => {
                this.setMedicines(medicines);
                this.dataSource = medicines; // (#)

            })

    }

    private setMedicines = (medicines: Medicine[] | undefined) => {
        if(!medicines) this.medicines = [];
        else this.medicines = medicines;
    }

    public deleteMedicine(medicineCode: string): void {
        this._medicineService.deleteMedicine(medicineCode)
            .pipe(
                take(1),
                catchError((err => {
                    if (err.status != 200) {
                        alert(`${err.error.errorMessage}`);
                        console.log(`Error: ${err.error.errorMessage}`)
                    } else {
                        console.log(`Ok: ${err.error.text}`);
                    }
                    return of(err);
                })),
                filter((response: IDeleteResponse) => response.success === true), // (#) ¿Cómo funca?
            )
            .subscribe((response: IDeleteResponse) => {
                this._medicineService.getMedicines(this.actualEmployee.name!)
                    .pipe(
                        take(1),
                        catchError((err => {
                            if (err.status != 200) {
                                alert(`${err.error.errorMessage}`);
                                console.log(`Error: ${err.error.errorMessage}`)
                            } else {
                                console.log(`Ok: ${err.error.text}`);
                            }
                            return of(err);
                        }))
                    )
                    .subscribe((medicines: Medicine[] | undefined) => {
                        this.setMedicines(medicines);
                    });
            });
    }

    public getPresentation(presentation: PresentationMedicine){
        if(presentation == PresentationMedicine.capsulas) return 'Cápsulas';
        if(presentation == PresentationMedicine.comprimidos) return 'Comprimidos';
        if(presentation == PresentationMedicine.liquido) return 'Líquido';
        if(presentation == PresentationMedicine.solucionSoluble) return 'Solución soluble';
        return 'Stick Pack';
    }


}
