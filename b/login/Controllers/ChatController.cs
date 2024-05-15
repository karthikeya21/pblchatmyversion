using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using login.Common.Models;
using Service;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace login.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        [Route("Get All Messages")]
        public async Task<IEnumerable<Chat>> GetAllChats()
        {
            return await _chatService.GetAllChatsAsync();
        }

        [HttpPost]
        [Route("Send Message")]
        public async Task<IActionResult> SendMessage( [FromForm] Chatform fd)
        {
            Chat message=new Chat();
            //message.Id = fd.Id;
            message.SenderId = fd.SenderId;
            message.ReceiverId = fd.ReceiverId;
            message.Message = fd.Message;
            
            if (fd.File != null)
            {
                await _chatService.SendMessageWithFileAsync(message,fd.File);
            }
            else
            {
                await _chatService.SendMessageAsync(message);
            }

            
            return Ok("Message sent successfully.");
        }

        [HttpGet]
        [Route("GetMessagesSenderIdUserId")]
        public async Task<IEnumerable<Chat>> GetIndividualMessages(String senderId,String receiverId)
        {
            return await _chatService.getIndividualMessages(senderId, receiverId);
        }

        [HttpPost]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile([FromBody] Chat chatMessage)
        {
            try
            {
                if (chatMessage == null)
                {
                    return BadRequest("Chat message data is missing.");
                }

                // Check if the message has file content
                if (chatMessage.FileContent == null)
                {
                    return BadRequest("Message does not contain file content.");
                }

                // Determine the content type based on the file type
                

                // Return the file content with the appropriate content type
                return File(chatMessage.FileContent, chatMessage.FileType, chatMessage.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        // Other methods for getting chats by sender/receiver id can be implemented similarly
    }
}   
