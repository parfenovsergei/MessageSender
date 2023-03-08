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
  helper = new JwtHelperService();

  currentUser: User = {
    email: null!,
    role: null!
  };

  constructor(
    private http: HttpClient, 
    private snackBar: MatSnackBar,
    private router: Router) { }

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
    return !this.helper.isTokenExpired(token);
  }

  decodeToken(token: string){
    const decodeToken = this.helper.decodeToken(token);
    this.currentUser.email = decodeToken.email;
    this.currentUser.role = decodeToken.role;
  }

  showMessage(message: string, action: string){
    this.snackBar.open(message, action);
  }
}