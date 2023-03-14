import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup , FormControl, Validators } from '@angular/forms';

import { MessageService } from 'src/app/services/message.service';
import { AuthService } from 'src/app/services/auth.service';
import { GeneralResponse } from 'src/app/response/generalResponse';
const daysInAYear = 365;

@Component({
  selector: 'app-message-create',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})

export class MessageComponent implements OnInit{
  
  messageForm: FormGroup;
  public minDate: Date = new Date();
  public maxDate: Date = new Date();
  constructor(
    private messageService: MessageService,
    private router: Router,
    private authService: AuthService){
    this.messageForm = new FormGroup({
      MessageTheme: new FormControl(""),
      MessageBody: new FormControl("", [
        Validators.required,
        Validators.maxLength(1000)
      ]),
      SendDate: new FormControl("", [
        Validators.required
      ])
    })
  }

  ngOnInit() {
    if(!this.authService.loggedIn()){
      this.router.navigate(['login']);
    }
    this.maxDate.setDate(this.maxDate.getDate() + daysInAYear);
  }

  get messageTheme(){
    return this.messageForm.controls['MessageTheme'];
  }

  get MessageBody(){
    return this.messageForm.controls['MessageBody'];
  }

  get SendDate(){
    return this.messageForm.controls['SendDate'];
  }

  getBodyErrorMessage(){
    if(this.MessageBody.hasError('required')){
      return 'Message is required'
    }
    else if(this.MessageBody.hasError('maxlength')){
      return 'Max message length 1000 symbols'
    }
    return '';
  }

  getDateErrorMessage(){
    if(this.SendDate.hasError('required')){
      return 'Date is required';
    }
    return ''
  }

  createMessage(){
    this.messageService.createMessage(
      this.messageTheme.value,
      this.MessageBody.value,
      this.SendDate.value)
      .subscribe((response: GeneralResponse) => {
        if(response.flag){
        this.messageService.showMessage(response.message, "OK");
        this.messageForm = new FormGroup(null);
        }
        else{
          this.messageService.showMessage(response.message, "OK");
        }
      });
  }
}