using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Model.Entities;
using System;
using System.Collections.Generic;
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

                    var user = new ApplicationUser { Id = 1, Email = "bot@hotmail.com", UserName = "RabbitMQ" };
                    user.PasswordHash = hasher.HashPassword(user, "RabbitMQ");

                    _context.ApplicationUsers.Add(user);
                    _context.SaveChanges();
                }

                if (!_context.ChatRooms.Any())
                {
                    var chatroom = new Chatroom()
                    {
                        GuidId = Guid.NewGuid().ToString(),
                        Name = "Main chatroom",
                        CantMessageToShow = 50,
                        Type = Utils.ChatTypeEnum.Public,
                        Messages = new List<Message>(),
                    };

                    _context.ChatRooms.Add(chatroom);
                    _context.SaveChanges();
                }
            }
        }
    }
}
