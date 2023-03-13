import { Component } from '@angular/core';
import { FormGroup , FormControl, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from 'src/app/services/auth.service';
import { RegisterResponse } from 'src/app/response/registerResponse';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {
  registerForm: FormGroup;
  confirmForm: FormControl;
  isRegistration: boolean;

  constructor(
    private authService: AuthService,
    private router: Router
    ){
      this.isRegistration = false;
      this.confirmForm = new FormControl();
      this.registerForm = new FormGroup({
        Email: new FormControl("", [
          Validators.required,
          Validators.email
        ]),
        Password: new FormControl("", [
          Validators.required,
          Validators.minLength(6),
          (control) => this.passwordMatcher(control, 'ConfirmPassword')
        ]),
        ConfirmPassword: new FormControl("", [
          Validators.required,
          Validators.minLength(6),
          (control) => this.passwordMatcher(control, 'ConfirmPassword')
        ]),
        Code: new FormControl(Number)
      })
  }

  get email(){
    return this.registerForm.controls["Email"];
  }

  get password(){
    return this.registerForm.controls["Password"];
  }

  get confirmPassword(){
    return this.registerForm.controls["ConfirmPassword"];
  }

  get code(){
    return this.registerForm.controls["Code"];
  }

  private passwordMatcher(control: AbstractControl, name: string){
    if(this.registerForm === undefined ||
      this.password.value === '' ||
      this.confirmPassword.value === ''){
      return null;
    } 
    else if(this.password.value === this.confirmPassword.value){
      if (name === 'Password' && this.confirmPassword.hasError('mismatch')) {
        this.password.setErrors(null);
        this.confirmPassword.updateValueAndValidity();
      } 
      else if (name === 'ConfirmPassword' && this.password.hasError('mismatch')) {
        this.confirmPassword.setErrors(null);
        this.password.updateValueAndValidity();
      }
      return null;
    } 
    else {
      return {mismatch : true};
    };
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
    else if(this.password.hasError('minlength')){
      return 'Min password length is 6 symbols';
    }
    return '';
  }

  getConfirmPasswordErrorMessage(){
    if(this.confirmPassword.hasError('required')){
      return 'Confirm password is required';
    }
    else if(this.confirmPassword.hasError('minlength')){
      return 'Min password length is 6 symbols';
    }
    else if(this.confirmPassword.hasError('mismatch')){
      return 'Passwords are mismatch';
    }
    return '';
  }

  registration(){
    this.authService
      .registration(
        this.email.value,
        this.password.value,
        this.confirmPassword.value)
      .subscribe((response: RegisterResponse) => {
        if(response.isRegister){
          this.isRegistration = true;
          this.authService.showMessage(response.message, "OK");
        }
        else{
          this.authService.showMessage(response.message, "OK");
        }
      })
  }

  verifyCode(code: number){
    this.authService
      .verifyCode(code, this.email.value)
      .subscribe((response: RegisterResponse) => {
        if(response.isRegister){
          this.authService.showMessage(response.message, "OK");
          this.router.navigate(['login']);
        }
      })
  }
}
