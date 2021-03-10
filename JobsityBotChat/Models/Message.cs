using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class Message
    {
        public string Text { get; set; }
        public DateTime Timestamp { get; }
        public ApplicationUser User { get; set; }
        public Chatroom Chat { get; set; } = null;

        #region mappers

        public static Model.Entities.Message MapToEntityModel(Message entity)
        {
            if (entity == null)
                return null;

            return new Model.Entities.Message() {
                Id = Guid.NewGuid().ToString(),
                Text = entity.Text,
                User = ApplicationUser.MapToEntityModel(entity.User),
                Chat = Chatroom.MapToEntityModel(entity.Chat)
            };
        }

        public static Message MapToWebModel(Model.Entities.Message entity)
        {
            if (entity == null)
                return null;

            return new Message()
            {
                Text = entity.Text,
                User = ApplicationUser.MapToWebModel(entity.User),
                Chat = Chatroom.MapToWebModel(entity.Chat)
            };
        }

        public static IEnumerable<Model.Entities.Message> MapToEntityModelList(IEnumerable<Message> messages)
        {
            if (messages == null)
                return null;

            var responseList = new List<Model.Entities.Message>();
            foreach(var item in messages)
            {
                responseList.Add(MapToEntityModel(item));
            }
            return responseList;
        }

        public static IEnumerable<Message> MapToWebModelList(IEnumerable<Model.Entities.Message> messages)
        {
            if (messages == null)
                return null;

            var responseList = new List<Message>();
            foreach (var item in messages)
            {
                responseList.Add(MapToWebModel(item));
            }
            return responseList;
        }

        #endregion
    }
}
