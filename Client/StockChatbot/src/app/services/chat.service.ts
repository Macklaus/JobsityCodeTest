import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { User } from '../models/user';
import { Chat } from '../models/chat';
import { Message } from '../models/message';
import { InsertMessageModel } from '../models/insert-message';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  userUrl = 'https://localhost:5001/User/signup';
  chatsUrl = 'https://localhost:5001/all';
  messagesUrl = 'https://localhost:5001/message-list?chatId=';
  insertMessageUrl = 'https://localhost:5001/message';

  constructor(private http: HttpClient) { }

  addUser(userName: string, email: string, password: string): Observable<HttpResponse<Object>> {
    const user: User = {userName: userName, email: email, passwordHash: password};
    return this.http.post(this.userUrl, user, { observe: 'response' });
  }

  getChatrooms(): Observable<Chat[]> {
    return this.http.get<Chat[]>(this.chatsUrl);
  }

  getMessages(chatId: string): Observable<Message[]> {
    return this.http.get<Message[]>(this.messagesUrl + chatId);
  }

  insertMessage(text: string, username: string, chatId: string): Observable<HttpResponse<Message>> {
    //var url = this.insertMessageUrl + 'Text=' + text;
    //url += url + '&OwnerName=' + username + '&ChatId=' + chatId;
    const msg: InsertMessageModel = {text: text, ownerName: username, chatId: chatId};
    return this.http.post<Message>(this.insertMessageUrl, msg, { observe: 'response' });
  }

}
