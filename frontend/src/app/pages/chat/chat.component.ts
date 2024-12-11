import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ChatService } from 'src/app/core/services/chat.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { messages } from 'src/app/layouts/topbar/data';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './chat.component.html', // Use the external HTML file for better readability
})
export class ChatComponent implements OnInit {
  ChatForm!: FormGroup; // Define the form group for reactive forms

  constructor(
    private fb: FormBuilder, // FormBuilder for creating the form
    public chatService: ChatService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Initialize the reactive form
    this.ChatForm = this.fb.group({
      messageContent: ['', Validators.required],
      userId: ['', Validators.required], // User ID is required for one-to-one chat
    });

    // Load messages from the chat service (this will be updated in real-time)
    this.loadMessages();
  }

  // Fetch messages from the chat service
  private loadMessages(): void {
    this.chatService.hubConnection.on('ReceiveMessage', (message: string) => {
      this.chatService.messages.update((messages) => [...messages, message]);

      console.log('here is the message ')
    });

   
  }

  // Send the message
  public sendMessage(): void {
    if (this.ChatForm.valid) {
      console.log(this.ChatForm.value);
      const { messageContent, userId } = this.ChatForm.value;

      // Send the message to the service
      this.chatService.sendMessage(messageContent, userId);

      // Reset the form after sending
      this.ChatForm.reset();
    } else {
      console.log('Empty or invalid');
    }
  }
}
