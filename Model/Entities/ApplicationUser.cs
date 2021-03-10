using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        public override string UserName { get; set; } = "";
        [Required]
        public override string Email { get; set; } = "";
        [Required]
        public override string PasswordHash { get; set; } = "";


        [NotMapped]
        public override bool EmailConfirmed { get; set; }
        [NotMapped]
        public override bool LockoutEnabled { get; set; }
        [NotMapped]
        public override int AccessFailedCount { get; set; }
        [NotMapped]
        public override string PhoneNumber { get; set; }
        [NotMapped]
        public override string ConcurrencyStamp { get; set; }
        [NotMapped]
        public override string SecurityStamp { get; set; }
        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }
        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }
        [NotMapped]
        public override string NormalizedUserName
        {
            get
            {
                return UserName.ToUpper();
            }
        }
        [NotMapped]
        public override string NormalizedEmail
        {
            get
            {
                return Email.ToUpper();
            }
        }

        public void Update(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(this, password);
        }

        public void Update(ApplicationUser user)
        {
            UserName = user.UserName;
            Email = user.Email;
            PasswordHash = user.PasswordHash;
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PasswordHash))
            {
                return false;
            }
            return true;
        }
    }
}
