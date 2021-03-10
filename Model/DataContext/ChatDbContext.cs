using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Utils;

namespace Model.DataContext
{
    public class ChatDbContext : DbContext
    {

        public ChatDbContext() { }

        public ChatDbContext(DbContextOptions options) : base(options) { }

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<Chatroom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .Property(m => m.Timestamp)
                .HasDefaultValueSql(Constants.SQLCurrentTimeStamp);
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase(Constants.InMemoryDatabaseName);
        }

    }
}
