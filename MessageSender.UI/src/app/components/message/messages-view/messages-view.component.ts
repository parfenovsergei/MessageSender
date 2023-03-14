import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';

import { DialogComponent } from '../../dialog/delete-dialog/dialog.component';
import { Role } from 'src/app/enums/role';
import { UserSelect } from 'src/app/models/userSelect';
import { Message } from 'src/app/models/message';
import { UserService } from 'src/app/services/user.service';
import { MessageService } from 'src/app/services/message.service';
import { AuthService } from 'src/app/services/auth.service';
import { GeneralResponse } from 'src/app/response/generalResponse';

@Component({
  selector: 'app-messages-view',
  templateUrl: './messages-view.component.html',
  styleUrls: ['./messages-view.component.css']
})
export class MessagesViewComponent implements OnInit{
  messages: Message[] = [];
  users: UserSelect[] = [];
  selectedUser!: number;

  constructor(
    private messageService: MessageService, 
    private router: Router,
    private authService: AuthService,
    private userService: UserService,
    private matDialog: MatDialog){}

  ngOnInit(){
    if(!this.authService.loggedIn()){
      this.router.navigate(['login']);
    }
    if(this.isAdmin()){
      this.userService.getUsers()
        .subscribe((result: UserSelect[]) => this.users = result);
    }
    this.getMessages();
  }

  getMessages() {
    this.messageService.myMessages()
      .subscribe(result => this.messages = result);
  }

  deleteMessage(id: number){
    this.messageService.deleteMessage(id)
      .subscribe((response: GeneralResponse) => {
        if(response.flag){
          this.messageService.showMessage(response.message, "OK");
          this.getMessages();
        }
        else{
          this.messageService.showMessage(response.message, "OK");
        }
      });
  }

  isAdmin() : boolean{
    return this.authService.currentUser.role == Role.Admin;
  }

  changeSelectedUser(){
    this.messageService.getUserMessagesById(this.selectedUser)
      .subscribe(result => this.messages = result);
  }

  openDeleteDialog(id: number){
    let dialogRef =  this.matDialog.open(DialogComponent, {
      width: '200px',
      height: '120px',
      data: id
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        this.deleteMessage(result);
    })
  }
}
