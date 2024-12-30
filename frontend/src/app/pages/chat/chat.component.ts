import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ChatService } from 'src/app/core/services/chat.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './chat.component.html',
})
export class ChatComponent implements OnInit, OnDestroy {
  public chatForm: FormGroup;
  public messages: string[] = [];

  constructor(private fb: FormBuilder, private chatService: ChatService) {
    // Initialize the form group
    this.chatForm = this.fb.group({
      newMessage: ['', Validators.required],
      userId: [''], // Optional field for private messages
      groupName: [''], // Optional field for group messages
    });
  }

  ngOnInit(): void {
    // React to message updates using signals
    this.chatService.messages().forEach((message) => this.messages.push(message));
  }

  sendMessage(): void {
    if (this.chatForm.invalid) {
      alert('Message cannot be empty');
      return;
    }

    const { newMessage, userId, groupName } = this.chatForm.value;

  
      this.chatService.sendMessage(newMessage, userId);
   

    this.chatForm.reset(); // Reset the form after sending
  }

  ngOnDestroy(): void {
    // Clean up the SignalR connection when the component is destroyed
    this.chatService.disconnect();
  }
}
