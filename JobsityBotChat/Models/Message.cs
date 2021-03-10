using System;

namespace WebApi.Models
{
    public class Message
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? GuidId { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string Text { get; set; }
        public DateTime Timestamp { get; }
        public ApplicationUser User { get; set; }
        public Chatroom Chat { get; set; } = null;
    }
}
