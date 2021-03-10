using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ChatController : Controller
    {

        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        public ChatController(IChatRepository chatRepository, IMessageRepository messageRepository)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(Chatroom chatroom)
        {
            if (chatroom == null)
                return BadRequest(chatroom);

            var entityModel = Chatroom.MapToEntityModel(chatroom);
            await _chatRepository.CreateAsync(entityModel);
            return Ok(chatroom);
        }

        [HttpGet("message-list")]
        public async Task<IEnumerable<Message>> GetMessageListByChat(string chatId)
        {
            var messages = await _chatRepository.GetMessagesByChatIdAsync(chatId);
            return Message.MapToWebModelList(messages);
        }

        [HttpPost("message")]
        public async Task<IActionResult> InsertMessage(string chatId, Message message)
        {
            if (message == null)
                return BadRequest(message);

            if (string.IsNullOrEmpty(chatId))
                return BadRequest(chatId);

            var messsageEntity = Message.MapToEntityModel(message);
            var chatroom = await _chatRepository.InsertNewMessageAsync(chatId, messsageEntity);

            if (chatroom == null)
                return NotFound(chatroom);

            messsageEntity.Chat = chatroom;
            await _messageRepository.CreateAsync(messsageEntity);

            return Ok();
        }

        [HttpPost("update-cant-message")]
        public async Task<IActionResult> UpdateCantMessageToShow(string chatId, int cantMessageToShow)
        {
            if (string.IsNullOrEmpty(chatId))
                return BadRequest(chatId);

            if(cantMessageToShow <= 0)
                return BadRequest(cantMessageToShow);

            var response = await _chatRepository.UpdateCantMessageToShowInChatroomAsync(chatId, cantMessageToShow);
            if (response)
            {
                return Ok();
            } else
            {
                return NotFound(chatId);
            }
        }
    }
}
