import { Component, Input, OnInit } from '@angular/core';
import { MedicineService } from '../../services/medicine.service';
import { Medicine, PresentationMedicine } from '../../models/medicine';
import { Router } from '@angular/router';
import { INIT, MEDICINE_LIST_URL } from '../../utils/routes';
import { Globals } from '../../utils/globals';
import { catchError, take, filter, of } from 'rxjs';
import { IDeleteResponse } from '../../interfaces/delete-response.interface';
import { Employee } from 'src/app/models/employee';
import { AuthService } from 'src/app/services/auth.service';
import { SessionService } from 'src/app/services/session.service';
import { Session } from 'src/app/models/session';

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
        private _authService: AuthService,
        private _sessionService: SessionService,
    ) { }

    public ngOnInit(): void {

        Globals.selectTab = 2;

        // [Obtengo el empleado actual]
        this._sessionService.getSessionByToken(this._authService.getToken()!)
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
        .subscribe((session: Session) => {            
            // [Obtenido el nombre del empleado, traigo desde el backend los medicamentos de su farmacia]
            this.actualEmployee.name = session.userName!;
            this.getMedicinesFromDB(this.actualEmployee.name);
        })        
    }

    private getMedicinesFromDB(userName: string): void{
        this._medicineService.getMedicines(userName)
        .pipe(
            take(1),
            catchError((err => {
                if (err.status != 200) {
                    // alert(`${err.error.errorMessage}`); // [Ya la pantalla principal me indica que no hay más medicamentos]
                    console.log(`Error: ${err.error.errorMessage}`)
                } else {
                    console.log(`Ok: ${err.error.text}`);
                }
                return of(err);
            }))
        )
        .subscribe((medicines: Medicine[]) => {
            this.setMedicines(medicines);
            // this.dataSource = medicines;
        })
    }

    private setMedicines = (medicines: Medicine[] | undefined) => {
        if (!medicines) {
            this.medicines = [];
        }
        else {
            this.medicines = medicines;
            this.dataSource = medicines;
        }
    }

    public deleteMedicine(medicineCode: string): void {
        this._medicineService.deleteMedicine(medicineCode, this._authService.getToken()!)
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
                // filter((response: IDeleteResponse) => response.success === true),
            )
            .subscribe((response: IDeleteResponse) => {
                this._medicineService.getMedicines(this.actualEmployee.name!)
                    .pipe(
                        take(1),
                        catchError((err => {
                            if (err.status != 200) {
                                // alert(`${err.error.errorMessage}`); // [Ya la pantalla principal me indica que no hay más medicamentos]
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

    public getPresentation(presentation: PresentationMedicine) {
        if (presentation == PresentationMedicine.capsulas) return 'Cápsulas';
        if (presentation == PresentationMedicine.comprimidos) return 'Comprimidos';
        if (presentation == PresentationMedicine.liquido) return 'Líquido';
        if (presentation == PresentationMedicine.solucionSoluble) return 'Solución soluble';
        return 'Stick Pack';
    }

}
