using System.Collections.Generic;
using System.Threading.Tasks;
using login.Common.Models;
using Microsoft.AspNetCore.Http;
using Repository;

namespace Service
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<IEnumerable<Chat>> GetAllChatsAsync()
        {
            return await _chatRepository.getAllChatsAsync();
        }

        public async Task<IEnumerable<Chat>> getIndividualMessages(string senderId, string receiverId)
        {
            return await _chatRepository.getIndividualChats(senderId, receiverId);
        }

        public async Task SendMessageAsync(Chat message)
        {
            await _chatRepository.AddChatAsync(message);
        }

        public async Task SendMessageWithFileAsync(Chat message, IFormFile file)
        {
            // Save file to disk or database
            // Extract file information and store it in the Chat object


            // If a file is attached, populate file-related properties in the message object
            
            
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    message.FileContent = ms.ToArray();
                    message.FileName = (string)(file.FileName); 
                    message.FileType = file.ContentType;
                    message.FileSize = file.Length;
                }
            

            // Save the chat message (with file) to the database
            await _chatRepository.AddChatAsync(message);

            // Additional logic for sending messages via SignalR
        }
        // Other methods for getting chats by sender/receiver id can be implemented similarly
    }
}
