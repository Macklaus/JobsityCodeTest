using WebApi.Models;

namespace WebApi.Extensions
{
    public static class ModelExtensions
    {
        #region Chatroom
        public static Model.Entities.Chatroom MapToEntityModel(this Chatroom entity)
        {
            if (entity == null)
                return null;

            return new Model.Entities.Chatroom()
            {
                GuidId = entity.GuidId,
                Name = entity.Name,
                CantMessageToShow = entity.CantMessageToShow,
                Type = entity.Type,
                Messages = entity.Messages.MapToEntityModelList()
            };
        }

        public static Chatroom MapToWebModel(this Model.Entities.Chatroom entity)
        {
            if (entity == null)
                return null;

            return new Chatroom()
            {
                GuidId = entity.GuidId,
                Name = entity.Name,
                CantMessageToShow = entity.CantMessageToShow,
                Type = entity.Type,
                Messages = entity.Messages.MapToWebModelList()
            };
        }
        #endregion

        #region Message
        public static Model.Entities.Message MapToEntityModel(this Message entity)
        {
            if (entity == null)
                return null;

            return new Model.Entities.Message()
            {
                GuidId = entity.GuidId,
                Text = entity.Text,
                User = ApplicationUser.MapToEntityModel(entity.User),
                Chat = entity.Chat.MapToEntityModel()
            };
        }

        public static Message MapToWebModel(this Model.Entities.Message entity)
        {
            if (entity == null)
                return null;

            return new Message()
            {
                GuidId = entity.GuidId,
                Text = entity.Text,
                User = ApplicationUser.MapToWebModel(entity.User),
                Chat = entity.Chat.MapToWebModel()
            };
        }
        #endregion
    }
}
