import { Injectable, signal } from '@angular/core';
import * as signalR from '@microsoft/signalr';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  public hubConnection!: signalR.HubConnection; // Use definite assignment
  public messages = signal<string[]>([]);
  private readonly apiUrl: string = environment.baseUrl;

  constructor() {
    this.initializeConnection();
    this.registerMessageHandlers();
  }

  private initializeConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/ChatHub`, {
        withCredentials: true,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR connection established'))
      .catch((err) => console.error('SignalR connection error:', err));

    this.hubConnection.onclose(() => {
      console.warn('SignalR connection closed. Attempting to reconnect...');
    });
  }

  private registerMessageHandlers() {
      this.hubConnection.on('ReceivePrivateMessage', (userId: string, message: string) => {
      console.log(`Private message from ${userId}:`, message);
      this.messages.update((currentMessages) => [
        ...currentMessages,
        `Private from ${userId}: ${message}`,
      ]);
    });
  }

  public sendMessage(message: string, userId?: string) {
   
      console.log('Sending private message:', { userId, message });
      this.hubConnection
        .invoke('SendPrivateMessage', userId, message)
        .catch((err) => console.error('Error sending private message:', err));
  }

  public disconnect() {
    if (this.hubConnection) {
      this.hubConnection
        .stop()
        .then(() => console.log('SignalR connection stopped'))
        .catch((err) => console.error('Error stopping SignalR connection:', err));
    }
  }
}
