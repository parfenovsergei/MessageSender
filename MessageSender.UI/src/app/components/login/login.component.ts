import { Component, OnInit } from '@angular/core';
import { FormGroup , FormControl, Validators , FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(){
    this.loginForm = new FormGroup({
      "Email": new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      "Password": new FormControl("", [
        Validators.required
      ])
    });
  }
}
