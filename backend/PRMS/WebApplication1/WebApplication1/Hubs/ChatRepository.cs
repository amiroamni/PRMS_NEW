using PRMS_BackendAPI.Models;
using System.Data.Entity;

namespace PRMS_BackendAPI.Hubs
{
    public interface IChatRepository
    {
        Task<IEnumerable<ChatMessage>> GetMessagesForUser(string userId);
        Task AddMessage(ChatMessage message);
        Task SaveChangesAsync();
    }
    public class ChatRepository : IChatRepository
    {
        private readonly PRMS_DatabaseContext _context;

        public ChatRepository(PRMS_DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesForUser(string userId)
        {
            return await _context.ChatMessages
                .Where(m => m.ReceiverId == userId || m.SenderId == userId)
                .ToListAsync();
        }

        public async Task AddMessage(ChatMessage message)
        {
            await _context.ChatMessages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
