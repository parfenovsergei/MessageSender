import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { UserSelect } from '../models/userSelect';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUsers() : Observable<UserSelect[]>{
    return this.http.get<UserSelect[]>(`${environment.apiUrl}/users`);
  }
}
