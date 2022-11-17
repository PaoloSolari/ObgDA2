import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, of, take } from 'rxjs';
import { Purchase } from '../../models/purchase';
import { PurchaseLine, PurchaseLineStatus } from '../../models/purchaseLine';
import { PurchaseLineService } from '../../services/purchase-line.service';
import { PurchaseService } from '../../services/purchase.service';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-buy-list',
    templateUrl: './buy-list.component.html',
    styleUrls: ['./buy-list.component.css']
})
export class BuyListComponent implements OnInit {

    public backUrl = `/${INIT}`;
    public exist: boolean = false;

    public purchaseForm = new FormGroup({
        idPurchase: new FormControl<string | null>(null, Validators.required)
    })
    public get idPurchase() { return this.purchaseForm.value.idPurchase!; }
    public lines: PurchaseLine[] = [];
    displayedColumns: string[] = ['medicineCode', 'quantity', 'status'];
    public dataSource = this.lines;

    constructor(
        private _purchaseService: PurchaseService,
        private _linesServices: PurchaseLineService,
    ) { }

    ngOnInit(): void {
        
        Globals.selectTab = 1;
        this.exist = false;
    }

    public onSubmit() {
        if(this.purchaseForm.valid) {
            if (this.idPurchase != undefined) {
                this._purchaseService.getPurchaseById(this.idPurchase)
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
                .subscribe((purchase: Purchase) => {
                    this.exist = true;
                    console.log(purchase);
                    this.showLines(purchase.purchaseLines!);
                })
            }
        }
    }

    private showLines = (lines: PurchaseLine[] | undefined) => {
        if(!lines) {
            this.lines = [];
        } else {
            this.lines = lines;
            this.dataSource = lines;
        }
    }

    public getStatus(status: PurchaseLineStatus) {
        if(status == PurchaseLineStatus.accepted) return 'Confirmado';
        if(status == PurchaseLineStatus.rejected) return 'Rechazado';
        return 'Pendiente';
    }

}