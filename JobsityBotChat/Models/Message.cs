using System;

namespace WebApi.Models
{
    public class Message
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? GuidId { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string Text { get; set; }
        public string OwnerName { get; set; }
        public string ChatId { get; set; } = null;
        public DateTime? Timestamp { get; set; }
    }
}
