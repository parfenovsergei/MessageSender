import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';
import { Message } from '../models/message';

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
    sendDate: Date) : Observable<string>
    {
      return this.http.post<string>(
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

  showMessage(message: string, action: string){
    this.snackBar.open(message, action);
  }
}