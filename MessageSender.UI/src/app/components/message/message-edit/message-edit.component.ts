import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { MessageService } from 'src/app/services/message.service';
import { AuthService } from 'src/app/services/auth.service';
import { Message } from 'src/app/models/message';
import { GeneralResponse } from 'src/app/response/generalResponse';
const daysInAYear = 365;

@Component({
  selector: 'app-message-edit',
  templateUrl: './message-edit.component.html',
  styleUrls: ['./message-edit.component.css']
})
export class MessageEditComponent implements OnInit{
  id!: number;
  message!: Message;
  messageForm: FormGroup; 
  public minDate: Date = new Date();
  public maxDate: Date = new Date();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private messageService: MessageService,
    private authService: AuthService){
      this.messageForm = new FormGroup({
        MessageTheme: new FormControl(''),
        MessageBody: new FormControl('', [
          Validators.required,
          Validators.maxLength(1000)
        ]),
        SendDate: new FormControl('', [
          Validators.required
        ])
      });
    }

  ngOnInit() : void{
    if(!this.authService.loggedIn()){
      this.router.navigate(['login']);
    }
    this.id = this.route.snapshot.params['id'];
    this.getMessageById();
    this.minDate = new Date();
    this.maxDate = new Date();
    this.maxDate.setDate(this.maxDate.getDate() + daysInAYear);
    setTimeout(() => { this.ngOnInit() }, 1000 * 60);
  }

  getMessageById(){
    this.messageService.find(this.id)
      .subscribe((result : Message) => {
        this.message = result
      });
  }

  get MessageTheme(){
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
    else if(this.SendDate.value < this.minDate){
      return 'Time is change, please select correct value';
    }
    return '';
  }

  editMessage(){
    this.messageService.editMessage(
      this.id,
      this.MessageTheme.value,
      this.MessageBody.value,
      this.SendDate.value)
      .subscribe((response: GeneralResponse) => {
        if(response.flag){
          this.messageService.showMessage(response.message, "OK");
          this.router.navigate(['messages']);
        }
        else{
          this.messageService.showMessage(response.message, "OK");
        }
      });
  }
}
