import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, of, take } from 'rxjs';
import { ICreateDemand } from 'src/app/interfaces/create-demand';
import { ICreatePetition } from 'src/app/interfaces/create-petition';
import { Demand } from 'src/app/models/demand';
import { Medicine } from 'src/app/models/medicine';
import { AuthService } from 'src/app/services/auth.service';
import { DemandService } from 'src/app/services/demand.service';
import { Employee } from '../../models/employee';
import { MedicineService } from '../../services/medicine.service';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-demand-form',
    templateUrl: './demand-form.component.html',
    styleUrls: ['./demand-form.component.css']
})
export class DemandFormComponent implements OnInit {

    public backUrl = `/${INIT}`;
    public medicines: Medicine[] = [];
    public actualEmployee: Employee = new Employee(null, null, null, null, null, null, null, null);
    // public demand: Demand = new Demand(null, null, null);
    public demand: ICreateDemand = {
        Petitions: [],
    }

    // displayedColumns: string[] = ['code', 'name', 'stock', 'newQuantity', 'add'];
    displayedColumns: string[] = ['code', 'name', 'stock', 'add'];
    // dataSource = this._medicineService.getMedicines(); // (#)
    // public dataSource: Medicine[] = [];
    public dataSource = this.medicines;
    public noMedicines: boolean = false;

    public demandForm = new FormGroup({
        newQuantityInput: new FormControl<number | null>(null, [Validators.required, Validators.min(1)]),
    })

    // public get quantityForm() { return this.newQuantityInput.value.newQuantityInput!; }
    // public get quantityForm() { return this.newQuantityInput.value! }

    constructor(
        private _medicineService: MedicineService,
        private _demandService: DemandService,
        private _authService: AuthService,
    ) { }

    ngOnInit(): void {

        Globals.selectTab = 2;

        // [Obtengo el empleado actual]
        this.actualEmployee.name = 'Paolo'; // (#) Cambiar

        // [Me traigo los medicamentos de la farmacia del empleado]
        this._medicineService.getMedicines(this.actualEmployee.name)
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

        // (#) Aquí debería de hacer algo con la lista de 'ICreateDemand'.
        this.demand.Petitions = [];

    }

    private setMedicines = (medicines: Medicine[] | undefined) => {
        if (!medicines) {
            this.medicines = [];
        } else {
            this.medicines = medicines;
        }
    }

    public addMedicineInDemand(medicineCode: string, newQuantity: number): void {
        const petition: ICreatePetition = {
            MedicineCode: medicineCode,
            NewQuantity: newQuantity,
        }
        console.log(petition);
        this.demand.Petitions?.push(petition);
        console.log(this.demand);
        this.showChangedMedicine();
    }

    public toNumber(str: string): number {
        return Number(str);
    }

    public showChangedMedicine() {
        // Bloquear medicamento, o poner algo gris.
        // Aunque hay que tener en cuenta, que se puede repetir un medicamente en dos peticiones distintas para una misma demanda.
    }

    public sendDemand() {
        this._demandService.postDemand(this.demand, this._authService.getToken()!)
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
            .subscribe((d: Demand) => {
                if (d) {
                    this.demandForm.reset();
                }
            });
    }

}
