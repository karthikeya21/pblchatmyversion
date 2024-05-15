using login.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IChatService
    {
        Task<IEnumerable<Chat>> GetAllChatsAsync();
        Task SendMessageAsync(Chat message);

        Task SendMessageWithFileAsync(Chat message, IFormFile file);

        // Other method signatures as needed
        Task<IEnumerable<Chat>> getIndividualMessages(String senderId, String receiverId);
    }
}
