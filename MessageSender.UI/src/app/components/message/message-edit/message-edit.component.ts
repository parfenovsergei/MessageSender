import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { MessageService } from 'src/app/services/message.service';
import { AuthService } from 'src/app/services/auth.service';
import { Message } from 'src/app/models/message';
const daysInAYear = 365;

@Component({
  selector: 'app-message-edit',
  templateUrl: './message-edit.component.html',
  styleUrls: ['./message-edit.component.css']
})
export class MessageEditComponent implements OnInit{
  id!: number;
  message!: Message;
  messageForm!: FormGroup; 
  public minDate: Date = new Date();
  public maxDate: Date = new Date();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private messageService: MessageService,
    private authService: AuthService){}

  ngOnInit() : void{
    if(!this.authService.loggedIn()){
      this.router.navigate(['login']);
    }
    this.id = this.route.snapshot.params['id'];
    this.getMessageById();
    this.messageForm = new FormGroup({
      MessageTheme: new FormControl(""),
      MessageBody: new FormControl("", [
        Validators.required,
        Validators.maxLength(1000)
      ]),
      SendDate: new FormControl("", [
        Validators.required
      ])
    });
    this.maxDate.setDate(this.maxDate.getDate() + daysInAYear);
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
    return ''
  }

  editMessage(){
    this.messageService.editMessage(
      this.id,
      this.MessageTheme.value,
      this.MessageBody.value,
      this.SendDate.value)
      .subscribe((response: string) => {
        this.messageService.showMessage(response, "OK");
        this.router.navigate(['messages']);
      });
  }
}