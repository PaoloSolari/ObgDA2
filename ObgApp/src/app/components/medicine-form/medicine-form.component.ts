import { Component, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INIT, MEDICINE_FORM_URL, MEDICINE_LIST_URL } from '../../utils/routes';
import { ICreateMedicine } from '../../interfaces/create-medicine';
import { MedicineService } from '../../services/medicine.service';
import { Globals } from '../../utils/globals';
import { NoSpace } from '../../validators/noEmptyString.validator';
import { Medicine, PresentationMedicine } from '../../models/medicine';
import { catchError, of, take } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-medicine-form',
    templateUrl: './medicine-form.component.html',
    styleUrls: ['./medicine-form.component.css'],
    // encapsulation: ViewEncapsulation.ShadowDom,
})
export class MedicineFormComponent implements OnInit {


    public backUrl = `/${INIT}`;
    public hasError: boolean = false;

    public medicineForm = new FormGroup({

        code: new FormControl(),
        name: new FormControl(),
        symtomps: new FormControl(),
        presentationControl: new FormControl<string | null>(null, Validators.required),
        quantity: new FormControl(),
        unit: new FormControl(),
        price: new FormControl(),
        prescriptionControl: new FormControl<string | null>(null, Validators.required),
    })

    // selectFormControl = new FormControl('', Validators.required);
    presentationControl = new FormControl<string | null>(null, Validators.required);
    presentations: string[] = ['Cápsulas', 'Comprimidos', 'Solución soluble', 'Stick Pack', 'Líquido'];
    prescriptionControl = new FormControl<string | null>(null, Validators.required);
    prescriptions: string[] = ['Sí', 'No'];


    constructor(
        private _medicineService: MedicineService,
        private _router: Router,
        private _authService: AuthService,
    ) { }

    public get codeForm() { return this.medicineForm.value.code!; }
    public get nameForm() { return this.medicineForm.value.name!; }
    public get symtompsForm() { return this.medicineForm.value.symtomps!; }
    public get presentationForm() { return this.medicineForm.value.presentationControl!; }
    // public get presentationForm() { return this.medicineForm.value.presentation!; }
    public get quantityForm() { return this.medicineForm.value.quantity!; }
    public get unitForm() { return this.medicineForm.value.unit!; }
    public get priceForm() { return this.medicineForm.value.price!; }
    public get prescriptionForm() { return this.medicineForm.value.prescriptionControl!; }
    // public get prescriptionForm() { return this.medicineForm.value.prescription!; }
    // El '!' del final es para prometerle a TypeScript que no va a ser null.

    public ngOnInit(): void {
        Globals.selectTab = 0;
    }
    public createMedicine(): void {
        if (this.medicineForm.valid) {
            const medicineFromForm: ICreateMedicine = {
                Code: this.codeForm,
                Name: this.nameForm,
                SymtompsItTreats: this.symtompsForm,
                Presentation: this.getPresentation(this.presentationForm),
                Quantity: this.quantityForm,
                Unit: this.unitForm,
                Price: this.priceForm,
                Prescription: this.getPrescription(this.prescriptionForm),
            };
            this._medicineService.postMedicine(medicineFromForm, this._authService.getToken()!)
                .pipe(
                    take(1),
                    catchError((err => {
                        if (err.status != 200) {
                            if(err.error.errorMessage != undefined)
                                alert(`${err.error.errorMessage}`);
                            console.log(`Error: ${err.error.errorMessage}`)
                            this.hasError = true;
                        } else {
                            console.log(`Ok: ${err.error.text}`);
                        }
                        return of(err);
                    }))
                )
                .subscribe((medicine: Medicine) => {
                    if (!this.hasError) {
                        this.cleanForm();
                    }
                    this.hasError = false;
                })
        }
    }

    private getPresentation(presentation: string): PresentationMedicine {
        if (presentation == 'Cápsulas') return PresentationMedicine.capsulas;
        if (presentation == 'Comprimidos') return PresentationMedicine.comprimidos;
        if (presentation == 'Solución soluble') return PresentationMedicine.solucionSoluble;
        if (presentation == 'Stick Pack') return PresentationMedicine.stickPack;
        return PresentationMedicine.liquido;
    }

    private getPrescription(prescription: string): boolean {
        if (prescription == 'Sí') return true;
        else return false;
    }

    public cleanForm(): void{
        this.formReset(this.medicineForm);
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

}


