using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Extensions;
using WebApi.Models.Requests;

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

            var entityModel = chatroom.MapToEntityModel();
            entityModel.GuidId = Guid.NewGuid().ToString();
            entityModel.Messages = new List<Model.Entities.Message>();
            await _chatRepository.CreateAsync(entityModel);
            return Ok(chatroom);
        }

        [HttpGet("message-list")]
        public async Task<IEnumerable<Message>> GetMessageListByChat(string chatId)
        {
            var messages = await _chatRepository.GetMessagesByChatIdAsync(chatId);
            return messages.MapToWebModelList();
        }

        [HttpGet("chat/{chatId}")]
        public async Task<Chatroom> GetChatroom(string chatId)
        {
            var chatroom = await _chatRepository.FindByGuidId(chatId);
            return chatroom.MapToWebModel();
        }

        [HttpPost("message")]
        public async Task<IActionResult> InsertMessage(InsertMessageModel messageRequest)
        {
            if (messageRequest == null)
                return BadRequest(messageRequest);

            if (string.IsNullOrEmpty(messageRequest.ChatId))
                return BadRequest(messageRequest.ChatId);

            if(messageRequest.OwnerName == null)
                return BadRequest(messageRequest.OwnerName);

            var messageResponse = await _chatRepository
                .InsertNewMessageAsync(messageRequest.ChatId, messageRequest.Text, messageRequest.OwnerName);

            if (messageResponse == null)
                return NotFound(messageRequest.ChatId);

            await _messageRepository.CreateAsync(messageResponse);

            return Ok(messageResponse);
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

        [HttpGet("all")]
        public async Task<IEnumerable<Chatroom>> GetAll()
        {
            var entityModelChatrooms = await _chatRepository.GetAllAsync();
            var webModelChatrooms = entityModelChatrooms.MapToWebModelList();
            return webModelChatrooms;
        }
    }
}
