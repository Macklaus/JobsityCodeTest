using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.DataContext;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ChatRepository : GenericRepository<Chatroom>, IChatRepository
    {

        public ChatRepository(ChatDbContext context) : base(context) { }

        public async Task<Chatroom> FindByGuidId(string guid)
        {
            return await _context.ChatRooms.Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.GuidId == guid);
        }

        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(string chatId)
        {
            var messages = Enumerable.Empty<Message>();

            var chatroom = await FindByGuidId(chatId);
            if(chatroom != null)
            {
                if(chatroom.Messages != null)
                {
                    messages = chatroom.Messages
                        .Take(chatroom.CantMessageToShow)
                        .OrderByDescending(x => x.Timestamp);
                }
            }

            return messages;
        }

        public async Task<Message> InsertNewMessageAsync(string chatId, Message message)
        {
            var chatroom = await _context.ChatRooms.Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.GuidId == chatId);

            if (chatroom != null)
            {
                var newMessage = new Message(message.Text, message.User, message.Chat);
                if(chatroom.Messages != null)
                {
                    chatroom.Messages.Append(newMessage);
                } else
                {
                    var newMessageList = new List<Message>();
                    newMessageList.Add(newMessage);
                    chatroom.Messages = newMessageList;
                }
            }

            return message;
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
