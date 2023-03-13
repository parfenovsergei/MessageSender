import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { AuthService } from 'src/app/services/auth.service';
import { Role } from 'src/app/enums/role';
import { DialogComponent } from '../dialog/delete-dialog/dialog.component';
import { LogoutDialogComponent } from '../dialog/logout-dialog/logout-dialog.component';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent {
  constructor(private authService: AuthService, private matDialog: MatDialog){}

  isAdmin(){
    return this.authService.currentUser.role == Role.Admin; 
  }

  isAuth(){
    return this.authService.loggedIn();
  }

  logout(){
    let dialogRef =  this.matDialog.open(LogoutDialogComponent, {
      width: '240px',
      height: '140px',
      data: true
    });

    dialogRef.afterClosed()
      .subscribe((result: boolean) => {
        if(result){
          this.authService.logout()
        }
    })
  }
}