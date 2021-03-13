import { Component } from '@angular/core';
import { ChatService } from '../app/services/chat.service';
import { Chat } from '../app/models/chat';
import { Message } from './models/message';
import { interval, Subscription } from 'rxjs';
import { ScrollToBottomDirective } from './directive/scroll-to-bottom.directive';
import { ViewChild } from '@angular/core';
import { ElementRef } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  username = '';
  email = '';
  password = '';
  userText = '';
  chatIdChoosen = '';
  chatrooms: Chat[] = [];
  messages: Message[] = [];
  hasUserChoosenChat: Boolean = false;
  isUserAdded: Boolean = false;

  subscription: Subscription;

  constructor(private service: ChatService) {
    const source = interval(1000);
    this.subscription = source.subscribe(val => this.getMessages(this.chatIdChoosen));
  }

  addUser(){
    this.service.addUser(this.username, this.email, this.password).subscribe(response => {
      if(response.status == 200){
        this.service.getChatrooms().subscribe((response: Chat[]) => {
          this.chatrooms = response;
          this.isUserAdded = true;
        });
      }
    });
  }

  getMessages(chatId: string){
    if(chatId !== ''){
      this.service.getMessages(chatId).subscribe((response: Message[]) => {
        this.messages = response;
        this.hasUserChoosenChat = true;
        this.chatIdChoosen = chatId;
      })
    }
  }

  sendMessage(){
    if(this.userText !== '') {
      this.service.insertMessage(this.userText, this.username, this.chatIdChoosen).subscribe(response => {
        if(response.ok){
          this.getMessages(this.chatIdChoosen);
          this.userText = '';
        }
      });
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
