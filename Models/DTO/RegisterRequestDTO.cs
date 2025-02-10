namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO
{
    public class RegisterRequestDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin" atau "User"
    }
}
