import { Component, OnInit } from '@angular/core';
import { setLines } from '@angular/material/core';
import { Router } from '@angular/router';
import { catchError, of, take } from 'rxjs';
import { IUpdateLine } from 'src/app/interfaces/update-line';
import { Employee } from 'src/app/models/employee';
import { Purchase } from 'src/app/models/purchase';
import { PurchaseLine, PurchaseLineStatus } from 'src/app/models/purchaseLine';
import { AuthService } from 'src/app/services/auth.service';
import { PurchaseLineService } from 'src/app/services/purchase-line.service';
import { PurchaseService } from 'src/app/services/purchase.service';
import { SessionService } from 'src/app/services/session.service';
import { Globals } from '../../utils/globals';
import { INIT, PURCHASE_LIST_URL } from '../../utils/routes';

@Component({
    selector: 'app-purchase-list',
    templateUrl: './purchase-list.component.html',
    styleUrls: ['./purchase-list.component.css']
})
export class PurchaseListComponent implements OnInit {

    public backUrl = `/${INIT}`;
    public actualEmployee: Employee = new Employee(null, null, null, null, null, null, null, null);
    public lines: PurchaseLine[] = [];
    public linesFromDB: PurchaseLine[] = [];

    public dataSource = this.lines;
    displayedColumns: string[] = ['code', 'quantity', 'accept', 'reject'];

    constructor(
        private _authService: AuthService,
        private _sessionService: SessionService,
        private _purchaseService: PurchaseService,
        private _purchaseLineService: PurchaseLineService,
        private _route: Router,
    ) { }

    ngOnInit(): void {

        Globals.selectTab = 0;

        // [Hago un GET PURCHASE que tenga todas las compras con al menos un medicamento de la farmacia del empleado]
        this._purchaseService.getPurchases(this._authService.getToken()!)
            .pipe(
                take(1),
                catchError((err => {
                    if (err.status != 200) {
                        // alert(`${err.error.errorMessage}`); // [Ya se muestra por interfaz cuando no hay compras pendientes]
                        console.log(`Error: ${err.error.errorMessage}`)
                    } else {
                        console.log(`Ok: ${err.error.text}`);
                    }
                    return of(err);
                }))
            )
            .subscribe((purchases: Purchase[]) => {
                // [Luego de obtener esa lista Purchase, recorro cada Purchase por separado y me traigo las líneas de compra que son del medicamento de la farmacia del empleado]
                this.setLines(purchases);
            })
    }

    private setLines = (purchases: Purchase[] | undefined) => {
        if (!purchases) {
            console.log('El empleado no tiene más (o no tiene) medicamentos para confirmar.');
        } else {
            purchases.forEach(element => {
                this._purchaseLineService.getLines(element.idPurchase!, this._authService.getToken()!)
                .subscribe((lines: PurchaseLine[]) => {
                    if(!lines){
                        console.log('No hay líneas de compras de la farmacia del empleado.');
                    } else {
                        lines.forEach(line => {
                            console.log(line);
                            this.dataSource.push(line);
                            this.dataSource = [...this.dataSource];
                        })
                    }
                })
            });
        }
    }

    private configureLines = (lines: PurchaseLine[] | undefined) => {
        console.log(lines);
        if(!lines) this.lines = [];
        else 
        {
            this.lines = lines;
            this.dataSource[1] = lines[1];
            // console.log(this.lines);
            // this.dataSource = lines;
            // console.log(this.dataSource);
        }
    }

    public updatePurchaseLine(line: PurchaseLine, status: number){
        console.log(line);
        const purchaseLine: IUpdateLine = {
            IdPurchaseLine: line.idPurchaseLine,
            MedicineCode: line.medicineCode,
            MedicineQuantity: line.medicineQuantity,
            Status: status, // this.getStatus(status), // (#)
        }
        this._purchaseLineService.putLines(purchaseLine, this._authService.getToken()!)
        .pipe(
            take(1),
            catchError((err => {
                if(err.status != 200){
                    alert('No hay stock suficiente del medicamento');
                    console.log(`Error: ${err.error.errorMessage}`)
                } else {
                    console.log(`Ok: ${err.error.text}`);
                }
                return of(err);
            }))
        )
        .subscribe((line: PurchaseLine) => {
            if(line) {
                // alert('Confirmación de compra realizada.');
                // this._route.navigateByUrl(PURCHASE_LIST_URL);
            }
        });
    }

    public checkStatus(status: PurchaseLineStatus): boolean {
        if(status == PurchaseLineStatus.accepted) return true;
        if(status == PurchaseLineStatus.rejected) return true;
        return false;
    }



}
