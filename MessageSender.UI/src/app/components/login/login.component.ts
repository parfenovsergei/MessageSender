import { Component, OnInit } from '@angular/core';
import { FormGroup , FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { LoginResponse } from 'src/app/response/loginResponse';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private authService: AuthService, private router: Router){
    this.loginForm = new FormGroup({
      Email: new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      Password: new FormControl("", [
        Validators.required
      ])
    });
  }

  get email(){
    return this.loginForm.controls["Email"];
  }

  get password(){
    return this.loginForm.controls["Password"];
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
        if(response.token != null){
          this.authService.getAndDecodeToken(response.token);
          this.authService.showMessage(response.message, "OK");
          this.router.navigate(['message']);
        }
        else
          this.authService.showMessage(response.message, "OK");
          this.loginForm = new FormGroup(null);
      });
  }
}
