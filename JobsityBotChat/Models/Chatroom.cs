using Model.Utils;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class Chatroom
    {
        public string? GuidId { get; set; }
        public string Name { get; set; }
        public ChatTypeEnum Type { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public int CantMessageToShow { get; set; }
    }
}
