import { Component, Input, OnInit } from '@angular/core';
import { catchError, of, take } from 'rxjs';
import { Demand, DemandStatus } from 'src/app/models/demand';
import { Owner } from '../../models/owner';
import { DemandService } from 'src/app/services/demand.service';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';
import { IUpdateDemand } from 'src/app/interfaces/update-demand';
import { AuthService } from 'src/app/services/auth.service';

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

    constructor(
        private _demandService: DemandService,
        private _authService: AuthService,
    ) { }

    ngOnInit(): void {

        Globals.selectTab = 1;

        // [Obtengo el dueño actual]
        this.actualOwner.name = 'Juan';
        // const token = 'GGHHII';

        // [Me traigo las demandas de la farmacia del dueño]
        this._demandService.getDemands(this._authService.getToken()!)
        .pipe(
            take(1),
            catchError((err => {
                if(err.status != 200){
                    alert(`${err.error.errorMessage}`);
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
    }

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
                } else {
                    console.log(`Ok: ${err.error.text}`);
                }
                return of(err);
            }))
        )
        .subscribe((demand: Demand) => {
            if(demand) {
                alert('Confirmación de solicitud de stock realizada.');
            }
        });
    }

    private getStatus(status: number): DemandStatus {
        if(status == 0) return DemandStatus.accepted;
        if(status == 1) return DemandStatus.rejected;
        return DemandStatus.inProgress;        
    }

}
