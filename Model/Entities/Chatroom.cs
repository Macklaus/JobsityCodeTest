using Model.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Model.Entities
{
    public class Chatroom
    {

        public Chatroom() { }

        public Chatroom(string name, int cantMessageToShow, ChatTypeEnum type)
        {
            GuidId = Guid.NewGuid().ToString();
            Name = name;
            CantMessageToShow = cantMessageToShow;
            Type = type;
            Messages = Enumerable.Empty<Message>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GuidId { get; set; }
        public string Name { get; set; }
        public int CantMessageToShow { get; set; }
        public ChatTypeEnum Type { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
