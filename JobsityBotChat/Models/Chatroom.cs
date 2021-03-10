using Model.Utils;
using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class Chatroom
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ChatTypeEnum Type { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public int CantMessageToShow { get; set; }

        public static Model.Entities.Chatroom MapToEntityModel(Chatroom entity)
        {
            if (entity == null)
                return null;

            return new Model.Entities.Chatroom()
            {
                Id = Guid.NewGuid().ToString(),
                Name = entity.Name,
                CantMessageToShow = entity.CantMessageToShow,
                Type = entity.Type,
                Messages = Message.MapToEntityModelList(entity.Messages)
            };
        }

        public static Chatroom MapToWebModel(Model.Entities.Chatroom entity)
        {
            return new Chatroom()
            {
                Id = entity.Id,
                Name = entity.Name,
                CantMessageToShow = entity.CantMessageToShow,
                Type = entity.Type,
                Messages = Message.MapToWebModelList(entity.Messages)
            };
        }
    }
}
