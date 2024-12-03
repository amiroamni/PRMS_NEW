using System;
using System.Collections.Generic;

namespace PRMS_BackendAPI.Models
{
    public partial class ChatMessage
    {
        public int Id { get; set; }
        public string MessageText { get; set; } = null!;
        public string SenderId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public DateTime SentAt { get; set; }

        public virtual UserChat Receiver { get; set; } = null!;
        public virtual UserChat Sender { get; set; } = null!;
    }
}
