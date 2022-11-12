import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, of, take } from 'rxjs';
import { ICreateDemand } from 'src/app/interfaces/create-demand';
import { ICreatePetition } from 'src/app/interfaces/create-petition';
import { Demand } from 'src/app/models/demand';
import { Medicine } from 'src/app/models/medicine';
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
    public dataSource = this.medicines;

    public demandForm = new FormGroup({
        newQuantityInput: new FormControl<number | null>(null, [Validators.required, Validators.min(1)]),
    })

    // public get quantityForm() { return this.newQuantityInput.value.newQuantityInput!; }
    // public get quantityForm() { return this.newQuantityInput.value! }

    constructor(
        private _medicineService: MedicineService,
        private _demandService: DemandService,
    ) { }

    ngOnInit(): void {

        Globals.selectTab = 2;

        // [Obtengo el empleado actual]
        this.actualEmployee.name = 'Paolo';
    
        // [Me traigo los medicamentos de la farmacia del empleado]
        this._medicineService.getMedicines(this.actualEmployee.name)
        .pipe(
            take(1),
            catchError((err) => {
                console.log({ err });
                return of(err);
            }),
        )
        .subscribe((medicines: Medicine[]) => {
            this.setMedicines(medicines);
            this.dataSource = medicines; // (#)
        })

        // (#) Aquí debería de hacer algo con la lista de 'ICreateDemand'.
        this.demand.Petitions = [];

    }

    private setMedicines = (medicines: Medicine[] | undefined) => {
        if(!medicines) this.medicines = [];
        else this.medicines = medicines;
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

    public toNumber(str: string): number{
        return Number(str);
    }

    public showChangedMedicine(){
        // Bloquear medicamento, o poner algo gris.
        // Aunque hay que tener en cuenta, que se puede repetir un medicamente en dos peticiones distintas para una misma demanda.
    }

    public sendDemand() {
        this._demandService.postDemand(this.demand)
            .pipe(
                take(1),
                catchError((err) => {
                    console.log({ err });
                    return of(err);
                }),
            )
            .subscribe((d: Demand) => {
                if (d) {
                    this.demandForm.reset();
                }
            });
    }

}
