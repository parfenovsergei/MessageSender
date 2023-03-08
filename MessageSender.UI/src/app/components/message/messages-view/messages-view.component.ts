import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-messages-view',
  templateUrl: './messages-view.component.html',
  styleUrls: ['./messages-view.component.css']
})
export class MessagesViewComponent implements OnInit{
  messages: Message[] = [];

  constructor(private messageService: MessageService){}

  ngOnInit(){
    this.getMessages();
  }

  getMessages() {
    this.messageService.myMessages()
  }
}
