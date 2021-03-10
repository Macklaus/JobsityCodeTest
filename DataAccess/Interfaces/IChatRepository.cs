using Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IChatRepository : IRepository<Chatroom>
    {
        public Task<IEnumerable<Message>> GetMessagesByChatIdAsync(string chatId);

        public Task<Chatroom> InsertNewMessageAsync(string chatId, Message message);

        public Task<bool> UpdateCantMessageToShowInChatroomAsync(string chatId, int newCantMessageToShow);
    }
}
