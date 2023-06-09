import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, of, take } from 'rxjs';
import { ICreatePetition } from '../../interfaces/create-petition';
import { ICreatePurchase } from '../../interfaces/create-purchase';
import { ICreatePurchaseLine } from '../../interfaces/create-purchase-line';
import { Medicine } from '../../models/medicine';
import { Purchase } from '../../models/purchase';
import { AuthService } from '../../services/auth.service';
import { MedicineService } from '../../services/medicine.service';
import { PurchaseService } from '../../services/purchase.service';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-buy',
    templateUrl: './buy.component.html',
    styleUrls: ['./buy.component.css']
})
export class BuyComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public filterForm = new FormGroup({
        pharmacyName: new FormControl<string | null>(null, Validators.required)
    })
    public get pharmacyName() { return this.filterForm.value.pharmacyName!; }

    public medicines: Medicine[] = [];
    public purchase: ICreatePurchase = {
        PurchaseLines: [],
    }
    displayedColumns: string[] = ['code', 'name', 'stock', 'add'];
    public dataSource = this.medicines;
    public noMedicines: boolean = false;

    public purchaseForm = new FormGroup({
        quantityInput: new FormControl<number | null>(null, [Validators.required, Validators.min(1)]),
    })

    public emailForm = new FormGroup({
        email: new FormControl<string | null>(null, Validators.required),
    })

    constructor(
        private _medicineService: MedicineService,
        private _purchaseService: PurchaseService,
        private _authService: AuthService,
    ) { }

    ngOnInit(): void {

        // [Me traigo TODOS los medicamentos (que tienen stock > 0) del sistema]
        this._medicineService.getMedicines('allMedicines') // [Seteamos 'allMedicines' como un nombre de usuario reservado para tener todos los medicamentos]
            .pipe(
                take(1),
                catchError((err => {
                    if (err.status != 200) {
                        alert(`${err.error.errorMessage}`);
                        console.log(`Error: ${err.error.errorMessage}`)
                        this.noMedicines = true;
                    } else {
                        console.log(`Ok: ${err.error.text}`);
                    }
                    return of(err);
                }))
            )
            .subscribe((medicines: Medicine[]) => {
                // console.log(medicines);
                this.setMedicines(medicines);
                if (!this.noMedicines) {
                    this.dataSource = this.medicines; // (#)
                }
            })

        // (#) Aquí debería de hacer algo con la lista de 'ICreatePurchase'.
        this.purchase.PurchaseLines = [];

    }

    private setMedicines = (medicines: Medicine[] | undefined) => {
        if (!medicines) {
            this.medicines = [];
        } else {
            this.medicines = medicines;
        }
    }


    // -------------(a partir de acá son método basados en el formulario de demanda)----------------

    public addMedicineInPurchase(medicineCode: string, quantity: number): void {
        const purchaseLine: ICreatePurchaseLine = {
            MedicineCode: medicineCode,
            MedicineQuantity: quantity,
            // Status: 2, // [Se le setea por defecto el status]
        }
        this.purchase.PurchaseLines?.push(purchaseLine);
        this.showChangedMedicine();
    }

    public toNumber(str: string): number {
        return Number(str);
    }

    public showChangedMedicine() {
        // Bloquear medicamento, o poner algo gris.
        // Aunque hay que tener en cuenta, que se puede repetir un medicamente en dos peticiones distintas para una misma demanda.
    }

    public sendPurchase() {
        this._purchaseService.postPurchase(this.purchase, this.emailForm.value.email!)
            .pipe(
                take(1),
                catchError((err => {
                    if (err.status != 200) {
                        alert(`${err.error.errorMessage}`);
                        console.log(`Error: ${err.error.errorMessage}`)
                    } else {
                        alert(`${err.error.text}`);
                        console.log(`Ok: ${err.error.text}`);
                    }
                    return of(err);
                }))
            )
            .subscribe((p: Purchase) => {
                if (p) {
                    this.purchaseForm.reset();
                    this.formReset(this.emailForm);
                    this.purchase.PurchaseLines = [];
                }
            });
    }

    formReset(form: FormGroup) {

        form.reset();
    
        Object.keys(form.controls).forEach(key => {
          form.get(key)!.setErrors(null) ; // [El '!' no estaba, me lo exigía]
        });
    }

    public onlySpace(input: string): boolean {
        if (input.trim()?.length === 0) {
            return true;
        }
        return false;
    }

    public filterMedicines(){
        if(this.filterForm.valid) {
            if(this.pharmacyName != undefined) {
                this._medicineService.getMedicinesFilter(this.pharmacyName)
                .pipe(
                    take(1),
                    catchError((err => {
                        if (err.status != 200) {
                            // alert(`${err.error.errorMessage}`);
                            console.log(`Error: ${err.error.errorMessage}`)
                        } else {
                            console.log(`Ok: ${err.error.text}`);
                        }
                        return of(err);
                    }))
                )
                .subscribe((medicines: Medicine[]) => {
                    console.log(medicines);
                    this.setMedicines(medicines);
                })
            }
        } else {
            this.ngOnInit();
        }
    }

}
