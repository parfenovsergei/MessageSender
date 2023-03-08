import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RegisterResponse } from 'src/app/response/registerResponse'
import { LoginResponse } from '../response/loginResponse';
import { User } from '../models/user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

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

  decodeToken(token: string){
    const payload = this.jwtHelper.decodeToken(token);
    this.currentUser.email = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
    this.currentUser.role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  }

  showMessage(message: string, action: string){
    this.snackBar.open(message, action);
  }
}