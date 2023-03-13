import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

import { RegisterResponse } from 'src/app/response/registerResponse'
import { LoginResponse } from '../response/loginResponse';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  currentUser: User = {
    email: null!,
    role: null!
  };

  constructor(
    private http: HttpClient, 
    private snackBar: MatSnackBar,
    private router: Router,
    private jwtHelper: JwtHelperService) { }

  registration(email: string, password: string, confirmPassword: string) : Observable<RegisterResponse>{
    return this.http.post<RegisterResponse>(
      (`${environment.apiUrl}/user/register`),
      {
        Email: email,
        Password: password,
        ConfirmPassword: confirmPassword
      },
      this.httpOptions
    );
  }

  login(email: string, password: string) : Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      (`${environment.apiUrl}/user/login`),
      {
        Email: email,
        Password: password
      },
      this.httpOptions
    )
  }

  logout() {
    this.currentUser = {
      email: null!,
      role: null!,
    }; 
   localStorage.removeItem('Token');
   this.router.navigate(['login']);
 }

  loggedIn(): boolean {
    const token = localStorage.getItem('Token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  verifyCode(code: number, email: string) : Observable<RegisterResponse>{
    return this.http.post<RegisterResponse>(
      (`${environment.apiUrl}/user/verify`),
      {
        Email: email,
        VerifyCode: code
      },
      this.httpOptions
    )
  }

  getAndDecodeToken(token: string){
    localStorage.setItem("Token", token);
    const payload = this.jwtHelper.decodeToken(token);
    this.currentUser = payload as User;
  }

  showMessage(message: string, action: string){
    this.snackBar.open(message, action, {duration: 4000});
  }
}