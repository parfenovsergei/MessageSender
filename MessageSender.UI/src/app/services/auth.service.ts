import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

import { GeneralResponse } from 'src/app/response/generalResponse'
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

  email!: string;
  currentUser: User = {
    email: null!,
    role: null!
  };

  constructor(
    private http: HttpClient, 
    private snackBar: MatSnackBar,
    private router: Router,
    private jwtHelper: JwtHelperService) { }

  registration(email: string, password: string, confirmPassword: string) : Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(
      (`${environment.apiUrl}/user/registration`),
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

  verifyCode(code: number, email: string) : Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(
      (`${environment.apiUrl}/user/verify`),
      {
        Email: email,
        VerifyCode: code
      },
      this.httpOptions
    )
  }

  sendCode(email: string) : Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(
      (`${environment.apiUrl}/user/forgot-password`),
      {
        Email: email
      },
      this.httpOptions
    )
  }

  confirmCode(email: string, code: number) : Observable<GeneralResponse>{
    this.email = email;
    return this.http.post<GeneralResponse>(
      (`${environment.apiUrl}/user/confirm-code`),
      {
        Email: email,
        VerifyCode: code
      },
      this.httpOptions
    )
  }

  changePassword(password: string, confirmPassword: string) : Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(
      (`${environment.apiUrl}/user/reset-password`),
      {
        Email: this.email,
        Password: password,
        ConfirmPassword: confirmPassword
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