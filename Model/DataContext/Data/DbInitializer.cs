using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Model.Entities;
using Model.Utils;
using System;
using System.Linq;

namespace Model.DataContext.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new ChatDbContext(serviceProvider.GetRequiredService<DbContextOptions<ChatDbContext>>()))
            {
                
                if (!_context.ApplicationUsers.Any())
                {
                    PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>(
                        new OptionsWrapper<PasswordHasherOptions>(
                            new PasswordHasherOptions()
                            {
                                CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
                            })
                    );

                    var userBot = new ApplicationUser { Id = 1, Email = "bot@hotmail.com", UserName = Constants.StockChatBotUserName };
                    userBot.PasswordHash = hasher.HashPassword(userBot, Constants.StockChatBotUserName.Trim());

                    var userTest = new ApplicationUser { Id = 2, Email = "test@hotmail.com", UserName = "test" };
                    userTest.PasswordHash = hasher.HashPassword(userTest, "test");

                    _context.ApplicationUsers.AddRange(userBot, userTest);
                    _context.SaveChanges();
                }

                if (!_context.ChatRooms.Any())
                {
                    var chatroom = new Chatroom()
                    {
                        GuidId = Guid.NewGuid().ToString(),
                        Name = "Main chatroom",
                        CantMessageToShow = 50,
                        Type = ChatTypeEnum.Public,
                        Messages = Enumerable.Empty<Message>()
                    };

                    _context.ChatRooms.Add(chatroom);
                    _context.SaveChanges();
                }
            }
        }
    }
}
