using Model.Utils;

namespace WebApi.Models.Requests
{
    public class ChatroomRequest
    {
        public string Name { get; set; }
        public ChatTypeEnum Type { get; set; }
        public int CantMessageToShow { get; set; }

        public Model.Entities.Chatroom ConvertToModelEntity()
        {
            return new Model.Entities.Chatroom(
                name: Name,
                cantMessageToShow: CantMessageToShow,
                type: Type
            );
        }

        public bool Validate()
        {
            if(string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name) 
                || CantMessageToShow < 1)
            {
                return false;
            }
            return true;
        }
    }
}
