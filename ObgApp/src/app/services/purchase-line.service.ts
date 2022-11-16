import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUpdateLine } from '../interfaces/update-line';
import { PurchaseLine } from '../models/purchaseLine';

@Injectable({
    providedIn: 'root'
})
export class PurchaseLineService {

    private _linesBehaviorSubject$: BehaviorSubject<PurchaseLine[] | undefined>;

    constructor(
        private _http: HttpClient,
    ) {
        this._linesBehaviorSubject$ = new BehaviorSubject<PurchaseLine[] | undefined>(undefined);
    }

    public get lines$(): Observable<PurchaseLine[] | undefined> {
        return this._linesBehaviorSubject$.asObservable();
    }

    public getLines(idPurchase: string, userToken: string): Observable<PurchaseLine[]> {
        let headers = new HttpHeaders();
        headers = headers.append('token', userToken);
        // let params = new HttpParams();
        // params = params.append('idPurchase', idPurchase);
        headers = headers.append('idPurchase', idPurchase);
        return this._http.get<PurchaseLine[]>(`${environment.API_HOST_URL}/purchaseline`, { headers }).pipe(
            tap((lines: PurchaseLine[]) => this._linesBehaviorSubject$.next(lines)),
        );
    }

    public putLines(purchaseLine: IUpdateLine, userToken: string): Observable<PurchaseLine> {
        let headers = new HttpHeaders();
        headers = headers.append('idPurchaseLine', purchaseLine.IdPurchaseLine!);
        headers = headers.append('token', userToken);
        return this._http.put<PurchaseLine>(`${environment.API_HOST_URL}/purchaseline`, purchaseLine, { headers });
    }

}
