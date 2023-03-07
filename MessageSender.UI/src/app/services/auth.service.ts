import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  registration(email: string, password: string, confirmPassword: string) : Observable<any>{
    return this.http.post(
      (`${environment.apiUrl}/user/register`),
      {
        Email: email,
        Password: password,
        ConfirmPassword: confirmPassword
      },
      this.httpOptions
    );
  }

  login(email: string, password: string) : Observable<any> {
    return this.http.post(
      (`${environment.apiUrl}/user/login`),
      {
        Email: email,
        Password: password
      },
      this.httpOptions
    )
  }

  showMessage(message: string, action: string){
    this.snackBar.open(message, action);
  }
}
