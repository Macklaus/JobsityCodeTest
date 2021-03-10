namespace WebApi.Models
{
    public class ApplicationUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public static Model.Entities.ApplicationUser MapToEntityModel(ApplicationUser user)
        {
            if (user == null)
                return null;

            return new Model.Entities.ApplicationUser()
            {
                Email = user.Email,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash
            };
        }

        public static ApplicationUser MapToWebModel(Model.Entities.ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                return null;

            return new ApplicationUser()
            {
                Email = applicationUser.Email,
                PasswordHash = applicationUser.PasswordHash,
                UserName = applicationUser.UserName
            };
        }
    }
}
