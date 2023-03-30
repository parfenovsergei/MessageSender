import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Message } from '../models/message';
import { GeneralResponse } from '../response/generalResponse';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  @ViewChild('picker') picker: any;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  createMessage(
    messageTheme: string,
    messageBody: string,
    sendDate: Date) : Observable<GeneralResponse>
    {
      return this.http.post<GeneralResponse>(
        (`${environment.apiUrl}/messages`),
        {
          MessageTheme: messageTheme,
          MessageBody: messageBody,
          SendDate: sendDate
        },
        this.httpOptions
      );
  }

  myMessages() : Observable<Message[]>{
    return this.http.get<Message[]>(`${environment.apiUrl}/messages`);
  }

  getUserMessagesById(id: number) : Observable<Message[]>{
    return this.http.get<Message[]>(`${environment.apiUrl}/users/${id}/messages`);
  }

  deleteMessage(id: number) : Observable<GeneralResponse>{
    return this.http.delete<GeneralResponse>(`${environment.apiUrl}/messages/${id}`);
  }

  find(id: number) : Observable<Message>{
    return this.http.get<Message>(`${environment.apiUrl}/messages/${id}`);
  }

  editMessage(
    id: number,
    messageTheme: string,
    messageBody: string,
    sendDate: Date) : Observable<GeneralResponse>{
      return this.http.put<GeneralResponse>(
        (`${environment.apiUrl}/messages/${id}`),
        {
          MessageTheme: messageTheme,
          MessageBody: messageBody,
          SendDate: sendDate
        },
        this.httpOptions
      );
  }

  showMessage(message: string, action: string){
    this.snackBar.open(message, action, {duration: 4000});
  }
}