import { Injectable, signal } from '@angular/core';
import * as signalR from '@microsoft/signalr';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  public hubConnection: signalR.HubConnection;
  public messages = signal<string[]>([]);
  apiUrl: string = environment.baseUrl;
  constructor() {

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.apiUrl + '/ChatHub', {
        withCredentials: true, // Ensure credentials are sent
      })
      .build();

    this.hubConnection.start().catch(err => console.error('SignalR error',err));

    this.hubConnection.on('ReceiveMessage', (message: string) => {
      console.log('Received message:', message);  // Log the received message
      this.messages.update((currentMessages) => [...currentMessages, message]);
    });
  }

  public sendMessage(message: string, userId?: string, groupName?: string) {
    if (userId) {
      console.log(userId, message,"send message func")
      this.hubConnection.invoke('SendPrivateMessage', userId, message)
        .catch(err => console.error(err));
    } else if (groupName) {
      this.hubConnection.invoke('SendGroupMessage', groupName, message)
        .catch(err => console.error(err));
    } else {
      this.hubConnection.invoke('SendBroadcastMessage', message)
        .catch(err => console.error(err));
    }
  }
}

