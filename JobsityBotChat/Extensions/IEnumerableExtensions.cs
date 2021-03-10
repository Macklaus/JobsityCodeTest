using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// convert a list of Message from web model to a list of Model library Message
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static IEnumerable<Model.Entities.Message> MapToEntityModelList(this IEnumerable<Message> messages)
        {
            if (messages == null)
                return null;

            var responseList = new List<Model.Entities.Message>();
            foreach (var item in messages)
            {
                responseList.Add(item.MapToEntityModel());
            }
            return responseList;
        }

        /// <summary>
        /// convert a list of Message from Model library to a list of Web Message model
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static IEnumerable<Message> MapToWebModelList(this IEnumerable<Model.Entities.Message> messages)
        {
            if (messages == null)
                return null;

            var responseList = new List<Message>();
            foreach (var item in messages)
            {
                responseList.Add(item.MapToWebModel());
            }
            return responseList;
        }

        /// <summary>
        /// convert a list of Chatroom from Model library to a list of Web Chatroom model
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<Chatroom> MapToWebModelList(this IEnumerable<Model.Entities.Chatroom> list)
        {
            if (list == null)
                return null;

            var responseList = new List<Chatroom>();
            foreach (var item in list)
            {
                responseList.Add(item.MapToWebModel());
            }
            return responseList;
        }
    }
}
