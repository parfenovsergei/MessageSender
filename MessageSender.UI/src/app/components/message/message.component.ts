import { Component } from '@angular/core';
import { FormGroup , FormControl, Validators , FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent {
  messageForm: FormGroup;

  constructor(){
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

}
