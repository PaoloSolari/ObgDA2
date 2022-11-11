import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICreateEmployee, ICreateUser } from '../interfaces/create-user';
import { IDeleteResponse } from '../interfaces/delete-response.interface';
import { IUpdateUser } from '../interfaces/update-user';
import { Employee } from '../models/employee';
import { User } from '../models/user';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private _usersBehaviorSubject$: BehaviorSubject<User[] | undefined>;

    constructor(
        private _http: HttpClient,
    ) { 
        this._usersBehaviorSubject$ = new BehaviorSubject<User[] | undefined>(undefined);
    }

    public get users$(): Observable<User[] | undefined> {
        return this._usersBehaviorSubject$.asObservable();
    }

    public getUsers(): Observable<User[]> {
        const headers = new HttpHeaders();
        return this._http.get<User[]>(`${environment.API_HOST_URL}/user`, { headers }).pipe(
            tap((users: User[]) => this._usersBehaviorSubject$.next(users)),
        );
    }

    public getUserByName(name: string): Observable<User> {
        return this._http.get<User>(`${environment.API_HOST_URL}/user/${name}`);
    }

    public postUser(userToAdd: ICreateUser): Observable<User> {
        console.log(userToAdd);
        return this._http.post<User>(`${environment.API_HOST_URL}/user`, userToAdd);
    }

    public putUser(userToUpdate: IUpdateUser, userName: string): Observable<User> {
        return this._http.put<User>(`${environment.API_HOST_URL}/user/${userName}`, userToUpdate);
    }

    public deleteUser(userName: string | null): Observable<IDeleteResponse> {
        return this._http.delete<IDeleteResponse>(`${environment.API_HOST_URL}/user/${userName}`);
    }
}
