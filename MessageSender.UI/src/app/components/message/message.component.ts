import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup , FormControl, Validators , FormGroupDirective } from '@angular/forms';
import { max } from 'rxjs';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit{
  
  messageForm: FormGroup;
  public minDate: Date = new Date();
  public maxDate: Date = new Date();

  constructor(private messageService: MessageService){
    this.messageForm = new FormGroup({
      "MessageTheme": new FormControl("", [
        Validators.required
     ]),
      "MessageBody": new FormControl("", [
        Validators.required,
        Validators.maxLength(1000)
      ]),
      "SendDate": new FormControl("", [
        Validators.required
      ])
    })
  }

  ngOnInit() {
    this.maxDate.setDate(this.maxDate.getDate() + 365);
  }

  get messageTheme(){
    return this.messageForm.controls['MessageTheme'].value;
  }

  get MessageBody(){
    return this.messageForm.controls['MessageBody'].value;
  }

  get SendDate(){
    return this.messageForm.controls['SendDate'].value;
  }

  createMessage(){
    this.messageService.createMessage(
      this.messageTheme,
      this.MessageBody,
      this.SendDate)
      .subscribe((response: string) => {
        this.messageService.showMessage(response, "OK");
        this.messageForm.reset();
      });
  }
}