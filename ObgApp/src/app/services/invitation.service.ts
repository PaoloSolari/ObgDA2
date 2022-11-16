import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICreateInvitation } from '../interfaces/create-invitation';
import { Invitation } from '../models/invitation';
import { Pharmacy } from '../models/pharmacy';

@Injectable({
    providedIn: 'root'
})
export class InvitationService {

    private _invitationsBehaviorSubject$: BehaviorSubject<Invitation[] | undefined>;

    constructor(
        private _http: HttpClient,
    ) {
        this._invitationsBehaviorSubject$ = new BehaviorSubject<Invitation[] | undefined>(undefined);
    }

    public get invitations$(): Observable<Invitation[] | undefined> {
        return this._invitationsBehaviorSubject$.asObservable();
    }

    public getInvitations(token: string): Observable<Invitation[]> {
        // return this._http.get<Invitation[]>(`${environment.API_HOST_URL}/invitation`, { headers }).pipe(
            //     tap((invitations: Invitation[]) => this._invitationsBehaviorSubject$.next(invitations)),
            // );
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        return this._http.get<Invitation[]>(`${environment.API_HOST_URL}/invitation`, { headers }).pipe(
            tap((invitations: Invitation[]) => this._invitationsBehaviorSubject$.next(invitations)),
        );
    }

    // public getInvitationById(invitationId: string): Observable<Invitation> {
    //     return this._http.get<Invitation>(`${environment.API_HOST_URL}/invitation/${invitationId}`);
    // }

    public getInvitationByName(name: string, userToken: string): Observable<Invitation> {
        let headers = new HttpHeaders();
        headers = headers.append('token', userToken);
        return this._http.get<Invitation>(`${environment.API_HOST_URL}/invitation/${name}`, { headers });
    }

    public postInvitation(invitationToAdd: ICreateInvitation, userToken: string): Observable<Invitation> {
        let headers = new HttpHeaders();
        headers = headers.append('pharmacyName', invitationToAdd.Pharmacy!.name!);
        headers = headers.append('token', userToken);
        // let headers = new HttpHeaders({
        //     'pharmacyName': 'invitationToAdd.Pharmacy!.name!',
        //     'token': 'userToken'
        // });
        return this._http.post<Invitation>(`${environment.API_HOST_URL}/invitation`, invitationToAdd, { headers });
    }

    public putInvitation(invitationToUpdate: Invitation, token: string): Observable<Invitation> {
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        return this._http.put<Invitation>(`${environment.API_HOST_URL}/invitation/${invitationToUpdate.idInvitation}`, invitationToUpdate, { headers });
    }

}
