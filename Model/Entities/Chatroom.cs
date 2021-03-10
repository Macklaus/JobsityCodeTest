using Model.Utils;
using System.Collections.Generic;

namespace Model.Entities
{
    public class Chatroom
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CantMessageToShow { get; set; }
        public ChatTypeEnum Type { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
