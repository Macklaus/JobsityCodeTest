using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class Message
    {
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
