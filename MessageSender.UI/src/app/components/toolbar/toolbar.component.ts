import { Component } from '@angular/core';

import { AuthService } from 'src/app/services/auth.service';
import { Role } from 'src/app/enums/role';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent {
  constructor(private authService: AuthService){}

  isAdmin(){
    return this.authService.currentUser.role == Role.Admin; 
  }

  isAuth(){
    return this.authService.loggedIn();
  }

  logout(){
    this.authService.logout();
  }
}
