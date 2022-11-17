import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, map, of, take } from 'rxjs';
import { IExporter } from '../../interfaces/exporter';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';
import { AuthService } from '../../services/auth.service';
import { ExporterService } from '../../services/exporter.service';

@Component({
    selector: 'app-exporter-form',
    templateUrl: './exporter-form.component.html',
    styleUrls: ['./exporter-form.component.css']
})
export class ExporterFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public exporters: string[] = [];
    public exporterForm = new FormGroup({
        exporterSelect: new FormControl<string | null>(null, Validators.required)
    })
    public get exporterSelected() { return this.exporterForm.value.exporterSelect!; }

    public parameters: string[] = [];
    public parametersForm = new FormGroup({})

    constructor(
        private _exporterService: ExporterService,
        private _authService: AuthService,
    ) { }

    ngOnInit(): void {

        Globals.selectTab = 0;

        // [Traemos los tipos de exportadores]
        this._exporterService.getExporters(this._authService.getToken()!)
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
            .subscribe((exporters: string[] | undefined) => {
                this.exporters = exporters!;
            })

    }

    public getParameters() {
        // [Traigo los parámetros en función del exportador]
        this.cleanForm(this.parameters);
        if (this.exporterSelected != undefined) {
            this._exporterService.getParameters(this._authService.getToken()!, this.exporterSelected)
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
                .subscribe((parameters: string[] | undefined) => {
                    if (parameters) {
                        // this.parameters = parameters!;
                        // [https://dkreider.medium.com/how-to-build-a-dynamic-form-with-angular-a-simple-example-with-explanation-2057abaf9169]
                        for (const parameter of parameters) {
                            // this.parametersForm.addControl(parameter, new FormControl('',this.getValidator(parameter)));
                            this.parametersForm.addControl(parameter, new FormControl('', Validators.required));
                        }
                        this.parameters = parameters;
                    } else {
                        this.parameters = [];
                    }
                })
        }
    }

    cleanForm = (parameters: string[]) => {
        for (const parameter of parameters) {
            this.parametersForm.removeControl(parameter);
        }
    }

    public onSubmit() {
        if (this.parametersForm.valid) {
            // let data = new Map<String, string>();
            let data = new Map();
            let keys: string[] = this.parameters;
            keys.forEach(key => {
                let value = this.value(key)?.value;
                data.set(key, value);
            });
            // let dictionary = Object.assign({}, ...data.map((x) => ({[x.id]: x.country})));
            let dictionary = Object.fromEntries(data);
            // let dictionary = Object.assign({}, ...data);
            // console.log(dictionary);
            // let dictionary = Object.fromEntries(data.map(x => [x.id, x.country]));
            this._exporterService.postExporter(this._authService.getToken()!, this.exporterSelected, dictionary)
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
            .subscribe((typeOfExporter: string | undefined) => {
                console.log('Exportación a '+ typeOfExporter +' realizada con éxito');
            })
            // console.log(data);
            // let values = this.parametersForm.value;
            // console.log(keys);
            // console.log(values);
        }
    }

    public value(key: string) { return this.parametersForm.get(key); }

    // private getValue(key: string): string {
    //     this.parameters.forEach(element => {
    //         return this.parametersForm.value['key'];
    //     });
    // }

}
