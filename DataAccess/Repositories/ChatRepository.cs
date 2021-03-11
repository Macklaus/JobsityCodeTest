using DataAccess.Interfaces;
using DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using Model.DataContext;
using Model.Entities;
using Model.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ChatRepository : GenericRepository<Chatroom>, IChatRepository
    {
        private readonly IStockService _stockService;

        public ChatRepository(ChatDbContext context, IStockService stockService) : base(context) 
        {
            _stockService = stockService;
        }

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

        public async Task<Message> InsertNewMessageAsync(string chatId, string messageText, string userName)
        {
            var chatroom = await FindByGuidId(chatId);

            if (chatroom != null)
            {
                Message message;
                if (messageText.StartsWith(Constants.StockCommandTitle))
                {
                    var command = messageText.Split(Constants.StockCommandSeparator)[1];
                    messageText = await _stockService.SendRequestAsync(command);
                    message = new Message(messageText, Constants.StockChatBotUserName, chatId);
                } else
                {
                    message = new Message(messageText, userName, chatId);
                }

                if (chatroom.Messages != null)
                {
                    chatroom.Messages = chatroom.Messages.Append(message);
                } else
                {
                    var newMessageList = new List<Message>();
                    newMessageList.Add(message);
                    chatroom.Messages = newMessageList;
                }

                Update(chatroom);
                return message;
            } else
            {
                return null;
            }
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
