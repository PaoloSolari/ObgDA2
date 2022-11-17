import { Component, Input, OnInit } from '@angular/core';
import { catchError, of, take } from 'rxjs';
import { Demand, DemandStatus } from 'src/app/models/demand';
import { Owner } from '../../models/owner';
import { DemandService } from 'src/app/services/demand.service';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';
import { IUpdateDemand } from 'src/app/interfaces/update-demand';
import { AuthService } from 'src/app/services/auth.service';
import { SessionService } from 'src/app/services/session.service';
import { Session } from 'src/app/models/session';

@Component({
    selector: 'app-demand-list',
    templateUrl: './demand-list.component.html',
    styleUrls: ['./demand-list.component.css']
})
export class DemandListComponent implements OnInit {

    public backUrl = `/${INIT}`;
    public demands: Demand[] = [];
    public dataSource = this.demands;
    displayedColumns: string[] = ['petition', 'accept', 'reject'];
    public actualOwner: Owner = new Owner(null, null, null, null, null, null, null, null);

    public hasError: boolean = false;

    constructor(
        private _demandService: DemandService,
        private _sessionService: SessionService,
        private _authService: AuthService,
    ) { }

    ngOnInit(): void {

        Globals.selectTab = 0;
        this.hasError = false;

        // [A través del token del dueño, traigo desde el backend las solicitudes de stock de su farmacia]
        this._demandService.getDemands(this._authService.getToken()!)
        .pipe(
            take(1),
            catchError((err => {
                if(err.status != 200){
                    // alert(`${err.error.errorMessage}`);
                    console.log(`Error: ${err.error.errorMessage}`)
                } else {
                    console.log(`Ok: ${err.error.text}`);
                }
                return of(err);
            }))
        )
        .subscribe((demands: Demand[]) => {
            this.setDemands(demands);
            this.dataSource = demands; // (#)
        })

        // [Obtengo el dueño actual]
        // this._sessionService.getSessionByToken(this._authService.getToken()!)
        // .pipe(
        //     take(1),
        //     catchError((err => {
        //         if (err.status != 200) {
        //             alert(`${err.error.errorMessage}`);
        //             console.log(`Error: ${err.error.errorMessage}`)
        //         } else {
        //             console.log(`Ok: ${err.error.text}`);
        //         }
        //         return of(err);
        //     }))
        // )
        // .subscribe((session: Session) => {            
        //     this.actualOwner.name = session.userName!;
        //     this.getDemandsFromDB(this.actualOwner.name);
        // }) 
    
    }

    // private getDemandsFromDB(userName: string): void { }

    private setDemands = (demands: Demand[] | undefined) => {
        if(!demands) this.demands = [];
        else this.demands = demands;
    }

    public updateDemand(idDemand: string, status: number){
        console.log(idDemand);
        const demand: IUpdateDemand = {
            IdDemand: idDemand,
            Status: this.getStatus(status), // (#) Capaz que aquí hay que obtener el status.
        }
        console.log(demand);
        this._demandService.putDemand(demand, this._authService.getToken()!)
        .pipe(
            take(1),
            catchError((err => {
                if(err.status != 200){
                    alert(`${err.error.errorMessage}`);
                    console.log(`Error: ${err.error.errorMessage}`)
                    this.hasError = true;
                } else {
                    console.log(`Ok: ${err.error.text}`);
                }
                return of(err);
            }))
        )
        .subscribe((demand: Demand) => {
            if(!this.hasError) {
                console.log('Confirmación de solicitud de stock realizada.');
                this.ngOnInit();
            }
            this.hasError = false;
        });
    }

    private getStatus(status: number): DemandStatus {
        if(status == 0) return DemandStatus.accepted;
        if(status == 1) return DemandStatus.rejected;
        return DemandStatus.inProgress;        
    }

}
