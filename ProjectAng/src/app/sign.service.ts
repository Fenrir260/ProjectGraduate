import { Injectable } from '@angular/core';
import { Environment } from '../environment/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, BehaviorSubject  } from 'rxjs';
import { Users } from '../models/users';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SignService {

  private apiUrl = `${Environment.apiUrl}/user`;

  constructor(private http: HttpClient) {
    const savedUser = localStorage.getItem('loginedUser');
    this._loginedUser = savedUser ? JSON.parse(savedUser) as Users : { id: 0, login: '', password: '', status: false };

    this._authenticationCheck = JSON.parse(localStorage.getItem('authenticationCheck') || 'false');
    this._statusCheck = JSON.parse(localStorage.getItem('statusCheck') || 'false');
  }
  
  private _loginedUser: Users = { id: 0, login: '', password: '', status: false };
  get loginedUser(): Users {
    return this._loginedUser;
  }
  set loginedUser(value: Users) {
    this._loginedUser = value;
    localStorage.setItem('loginedUser', JSON.stringify(value));
    console.log("value " + value);
  }

  private _authenticationCheck: boolean = false;
  get authenticationCheck(): boolean {
    return this._authenticationCheck;
  }
  set authenticationCheck(value: boolean) {
    this._authenticationCheck = value;
    localStorage.setItem('authenticationCheck', JSON.stringify(value));
  }

  private _statusCheck: boolean = false;
  get statusCheck(): boolean {
    return this._statusCheck;
  }
  set statusCheck(value: boolean) {
    this._statusCheck = value;
    localStorage.setItem('statusCheck', JSON.stringify(value));
  }

  createUser(user: Users): Observable<Users> {
    console.log("creating user");
    return this.http.post<Users>(`${this.apiUrl}/createUser`, user);
  }

  getAllUsers(): Observable<Users[]> {
    console.log("getting all users");
    return this.http.get<Users[]>(`${this.apiUrl}/getAllUsers`);
  }

  getUserById(id: number): Observable<Users> {
    console.log("getting user by id");
    return this.http.get<Users>(`${this.apiUrl}/${id}/getUserById`);
  }

  
  getUserByLogin(login: string): Observable<Users> {
    console.log("getting user by login");
    return this.http.get<Users>(`${this.apiUrl}/${login}/getUserByLogin`);
  }
  
  updateUser(user: Users, id: number): Observable<Users> {
    console.log("updating user");
    return this.http.put<Users>(`${this.apiUrl}/${id}/updateUser`, user);
  }
  
  deleteUserById(id: number): Observable<void> {
    console.log("deleting user by id");
    return this.http.delete<void>(`${this.apiUrl}/${id}/deleteUserById`);
  }

  deleteUserByLogin(login: string): Observable<void> {
    console.log("deleting user by login");
    return this.http.delete<void>(`${this.apiUrl}/${login}/deleteUserByLogin`);
  }

  verify(user: Users): Observable<boolean> {
    return this.http.post(`${this.apiUrl}/${user.login}/${user.password}/verify`, null)
      .pipe(
        map(() => true), 
        catchError((error: HttpErrorResponse) => {
          console.error('Помилка під час перевірки:', error);
          return of(false); 
        })
      );
  }

  
}
