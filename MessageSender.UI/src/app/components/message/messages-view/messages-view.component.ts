import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';
import { AuthService } from 'src/app/services/auth.service';
import { Role } from 'src/app/enums/role';
import { UserSelect } from 'src/app/models/userSelect';
import { UserService } from 'src/app/services/user.service';

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
    private userService: UserService){}

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
      .subscribe((result: string) => {
        this.messageService.showMessage(result, "OK");
        this.getMessages();
      });
  }

  isAdmin() : boolean{
    return this.authService.currentUser.role == Role.Admin;
  }

  changeSelectedUser(){
    this.messageService.getUserMessagesById(this.selectedUser)
      .subscribe(result => this.messages = result);
  }
}
