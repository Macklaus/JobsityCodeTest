using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class Message
    {
        public Message()
        {
            GuidId = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
        }

        public Message(string text, ApplicationUser user, Chatroom chat)
        {
            GuidId = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            Text = text;
            User = user;
            Chat = chat;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GuidId { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; }
        public ApplicationUser User { get; set; }
        public Chatroom Chat { get; set; }
    }
}
