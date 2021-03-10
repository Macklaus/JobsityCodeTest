using DataAccess.Interfaces;
using Model.DataContext;
using Model.Entities;

namespace DataAccess.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ChatDbContext context) : base(context)
        {
        }
    }
}
