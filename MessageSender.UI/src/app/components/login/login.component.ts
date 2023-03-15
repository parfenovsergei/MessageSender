import { Component, OnInit } from '@angular/core';
import { FormGroup , FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { LoginResponse } from 'src/app/response/loginResponse';
import { GeneralResponse } from 'src/app/response/generalResponse';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  isForgot: boolean;
  isSendCode: boolean;
  isConfirm: boolean;
  loginForm: FormGroup;

  constructor(private authService: AuthService, private router: Router){
    this.isForgot = false;
    this.isSendCode = false;
    this.isConfirm = false;
    this.loginForm = new FormGroup({
      Email: new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      Password: new FormControl("", [
        Validators.required
      ]),
      Code: new FormControl("")
    });
  }

  get email(){
    return this.loginForm.controls["Email"];
  }

  get password(){
    return this.loginForm.controls["Password"];
  }

  get code(){
    return this.loginForm.controls["Code"];
  }

  setRequiredCode(){
    this.loginForm.controls["Code"].setValidators([Validators.required]);
    this.loginForm.controls["Code"].updateValueAndValidity();
  }

  setUnrequiredPassword(){
    this.loginForm.controls["Password"].setValidators(null);
    this.loginForm.controls["Password"].updateValueAndValidity();
  }

  getCodeErrorMessage(){
    if(this.code.hasError('required')){
      return 'Enter code here';
    }
    return '';
  }

  getEmailErrorMessage(){
    if(this.email.hasError('required')){
      return 'Email is required';
    }
    else if(this.email.hasError('email')){
      return 'Incorrect email format';
    }
    return '';
  }

  getPasswordErrorMessage(){
    if(this.password.hasError('required')){
      return 'Password is required';
    }
    return '';
  }

  login(){
    this.authService
      .login(
        this.email.value,
        this.password.value)
      .subscribe((response: LoginResponse) => 
      {
          this.authService.getAndDecodeToken(response.token);
          this.authService.showMessage(response.message, "OK");
          this.router.navigate(['message']);
      },
      (err) => {
        this.authService.showMessage(err.error.message, "OK");
        this.loginForm = new FormGroup(null);
      });
  }

  forgot(){
    this.setUnrequiredPassword();
    this.isForgot = true;
  }

  sendCode(){
    this.authService
      .sendCode(this.email.value)
      .subscribe((response: GeneralResponse) => {
        this.authService.showMessage(response.message, "OK");
        this.setRequiredCode();
        this.isSendCode = true;
      },
      (err) => {
        this.authService.showMessage(err.error.message, "OK");
      });
  }

  confirmCode(){
    this.authService
      .confirmCode(
        this.email.value,
        this.code.value
      )
      .subscribe((response: GeneralResponse) => {
          this.authService.showMessage(response.message, "OK");
          this.isConfirm = true;
          this.router.navigate(['change-password']);
        },
        (err) => {
          this.authService.showMessage(err.error.message, "OK");
        }
      )
  }
}
