import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-messages-view',
  templateUrl: './messages-view.component.html',
  styleUrls: ['./messages-view.component.css']
})
export class MessagesViewComponent implements OnInit{
  messages: Message[] = [];
  
  constructor(private messageService: MessageService, private router: Router){}

  ngOnInit(){
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
}
