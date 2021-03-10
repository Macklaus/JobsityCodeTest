using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.DataContext;
using Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ChatRepository : GenericRepository<Chatroom>, IChatRepository
    {

        public ChatRepository(ChatDbContext context) : base(context) { }

        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(string chatId)
        {
            var chatroom = await _context.ChatRooms.Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.GuidId == chatId);

            if(chatroom != null)
            {
                return chatroom.Messages.Take(chatroom.CantMessageToShow);
            }

            return new List<Message>();
        }

        public async Task<Chatroom> InsertNewMessageAsync(string chatId, Message message)
        {
            var chatroom = await _context.ChatRooms.Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.GuidId == chatId);

            if (chatroom != null)
            {
                chatroom.Messages.Append(message);
            }

            return chatroom;
        }

        public async Task<bool> UpdateCantMessageToShowInChatroomAsync(string chatId, int newCantMessageToShow)
        {
            var chatroom = await _context.ChatRooms.Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.GuidId == chatId);

            if (chatroom == null)
                return false;

            chatroom.CantMessageToShow = newCantMessageToShow;
            Update(chatroom);
            return true;
        }
    }
}
