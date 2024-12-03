using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class UserChat
    {
        public UserChat()
        {
            ChatMessageReceivers = new HashSet<ChatMessage>();
            ChatMessageSenders = new HashSet<ChatMessage>();
        }

        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ChatMessage> ChatMessageReceivers { get; set; }
        public virtual ICollection<ChatMessage> ChatMessageSenders { get; set; }
    }
}
