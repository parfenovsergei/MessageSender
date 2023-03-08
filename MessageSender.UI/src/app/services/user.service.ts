import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserSelect } from '../models/userSelect';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUsers() : Observable<UserSelect[]>{
    return this.http.get<UserSelect[]>(`${environment.apiUrl}/admin/users`);
  }
}
