using Model.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class Chatroom
    {
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
