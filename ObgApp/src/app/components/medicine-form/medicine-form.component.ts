import { Component, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INIT, MEDICINE_FORM_URL, MEDICINE_LIST_URL } from '../../utils/routes';
import { ICreateMedicine } from '../../interfaces/create-medicine';
import { MedicineService } from '../../services/medicine.service';
import { Globals } from '../../utils/globals';
import { NoSpace } from '../../validators/noEmptyString.validator';
import { PrescriptionMedicine, PresentationMedicine } from '../../models/medicine';

@Component({
    selector: 'app-medicine-form',
    templateUrl: './medicine-form.component.html',
    styleUrls: ['./medicine-form.component.css'],
    // encapsulation: ViewEncapsulation.ShadowDom,
})
export class MedicineFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public medicineForm = new FormGroup({
        
        // code: new FormControl(undefined, [Validators.required, NoSpace]),
        // name: new FormControl(undefined, [Validators.required, NoSpace]),
        // symtomps: new FormControl(undefined, [Validators.required, NoSpace]),
        // presentation: new FormControl(undefined, [Validators.required]),
        // quantity: new FormControl(undefined, [Validators.required, Validators.min(0)]),
        // unit: new FormControl(undefined, [Validators.required, NoSpace]),
        // price: new FormControl(undefined, [Validators.required, Validators.min(0)]),
        // stock: new FormControl(undefined, [Validators.required, Validators.min(0)]),
        // prescription: new FormControl(undefined, [Validators.required]),
        // isActive: new FormControl(undefined, [Validators.required])

        code: new FormControl(),
        name: new FormControl(),
        symtomps: new FormControl(),
        // presentation: new FormControl(),
        presentationControl: new FormControl<string | null>(null, Validators.required),
        quantity: new FormControl(),
        unit: new FormControl(),
        price: new FormControl(),
        // prescription: new FormControl()
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
        Globals.selectTab = 2;
    }
    public createMedicine(): void {
        if (this.medicineForm.valid) {
            const medicineFromForm: ICreateMedicine = {
                code: this.codeForm,
                name: this.nameForm,
                symtomps: this.symtompsForm,
                presentation: this.presentationForm,
                quantity: this.quantityForm,
                unit: this.unitForm,
                price: this.priceForm,
                prescription: this.prescriptionForm,
            };
            const medicineCode = this._medicineService.postMedicine(medicineFromForm);
            if (medicineCode) {
                alert('Presentación: ' + medicineFromForm.presentation + '. Receta: ' + medicineFromForm.prescription);
                this.clearForm();
                // this._router.navigateByUrl(MEDICINE_FORM_URL);
            }
        } else {
            alert('Debe ingresar todos los datos solicitados');
        }
    }

    public clearForm() {
        this.medicineForm.reset();
    }

    public onlySpace(input: string): boolean {
        if (input.trim()?.length === 0) {
            return true;
        }
        return false;
    }

}

