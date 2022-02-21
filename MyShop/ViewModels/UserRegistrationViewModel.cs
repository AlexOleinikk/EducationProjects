namespace MyShop.ViewModels
{
    public class UserRegistrationViewModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string PasswordSecond { get; set; }

        public string Status { get; } = "User";
    }
}
