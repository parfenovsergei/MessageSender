import { Component } from '@angular/core';
import { FormGroup , FormControl, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from 'src/app/services/auth.service';
import { GeneralResponse } from 'src/app/response/generalResponse';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {
  registerForm: FormGroup;
  isRegistration: boolean;

  constructor(
    private authService: AuthService,
    private router: Router
    ){
      this.isRegistration = false;
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
        Code: new FormControl("")
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

  setRequiredCode(){
    this.registerForm.controls["Code"].setValidators([Validators.required]);
    this.registerForm.controls["Code"].updateValueAndValidity();
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
      .subscribe((response: GeneralResponse) => {
          this.isRegistration = true;
          this.setRequiredCode();
          this.authService.showMessage(response.message, "OK");
      },
      (err) => {
        this.authService.showMessage(err.error.message, "OK");
      })
  }

  verifyCode(code: number){
    this.authService
      .verifyCode(code, this.email.value)
      .subscribe((response: GeneralResponse) => {
          this.authService.showMessage(response.message, "OK");
          this.router.navigate(['login']);
      },
      (err) => {
        this.authService.showMessage(err.error.message, "OK");
      })
  }
}
