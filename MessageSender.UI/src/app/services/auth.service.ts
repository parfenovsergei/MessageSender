import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  registration(email: string, password: string) : Observable<any>{
    return this.http.post(
      (`${environment.apiUrl}/user/register`),
      {
        Email: email,
        Password: password
      },
      this.httpOptions
    );
  }
}
