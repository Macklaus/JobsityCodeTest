using System;

namespace Model.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; }
        public ApplicationUser User { get; set; }
        public Chatroom Chat { get; set; }
    }
}
