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

        public Message(string text, string ownerName, string chatId)
        {
            GuidId = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            Text = text;
            OwnerName = ownerName;
            ChatId = chatId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GuidId { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; }
        public string OwnerName { get; set; }
        public string ChatId { get; set; }
    }
}
