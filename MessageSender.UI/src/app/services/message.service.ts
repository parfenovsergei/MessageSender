import { Injectable, ViewChild } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  @ViewChild('picker') picker: any;
  
  constructor() { }
}