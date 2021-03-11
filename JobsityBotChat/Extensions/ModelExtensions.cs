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
                Text = entity.Text
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
                ChatId = entity.ChatId,
                OwnerName = entity.OwnerName,
                Timestamp = entity.Timestamp
            };
        }
        #endregion

        #region users
        public static Model.Entities.ApplicationUser MapToEntityModel(this ApplicationUser user)
        {
            if (user == null)
                return null;

            return new Model.Entities.ApplicationUser()
            {
                Email = user.Email,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash
            };
        }

        public static ApplicationUser MapToWebModel(this Model.Entities.ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                return null;

            return new ApplicationUser()
            {
                Email = applicationUser.Email,
                PasswordHash = applicationUser.PasswordHash,
                UserName = applicationUser.UserName
            };
        }
        #endregion
    }
}
