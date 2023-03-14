import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GeneralResponse } from 'src/app/response/generalResponse';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
  passwordForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router
    ){
      this.passwordForm = new FormGroup({
        Password: new FormControl("", [
          Validators.required,
          Validators.minLength(6),
          (control) => this.passwordMatcher(control, 'ConfirmPassword')
        ]),
        ConfirmPassword: new FormControl("", [
          Validators.required,
          Validators.minLength(6),
          (control) => this.passwordMatcher(control, 'ConfirmPassword')
        ])
      })
  }

  get password(){
    return this.passwordForm.controls["Password"];
  }

  get confirmPassword(){
    return this.passwordForm.controls["ConfirmPassword"];
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

  private passwordMatcher(control: AbstractControl, name: string){
    if(this.passwordForm === undefined ||
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

  changePassword(){
    this.authService
      .changePassword(
        this.password.value,
        this.confirmPassword.value
      )
      .subscribe((response: GeneralResponse) => {
        this.authService.showMessage(response.message, "OK");
        this.authService.email = '';
        this.router.navigate(['login']);
      },
      (err) => {
        this.authService.showMessage(err.error.message, "OK");
      })
  }
}
