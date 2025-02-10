//using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DB;
//using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models;
//using BCrypt.Net;
//namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Services
//{
//    public class UserService
//    {
//        private readonly ApplicationContext _context;

//        public UserService(ApplicationContext context)
//        {
//            _context = context;
//        }

//        public bool Register(string username, string email, string password, string role)
//        {
//            if (_context.Users.Any(u => u.Username == username || u.Email == email))
//                return false;

//            var hashedPassword = BCrypt.HashPassword(password);

//            var user = new User
//            {
//                Username = username,
//                Email = email,
//                PasswordHash = hashedPassword,
//                Role = role,
//                CreatedAt = DateTime.UtcNow
//            };

//            _context.Users.Add(user);
//            _context.SaveChanges();

//            return true;
//        }

//        public User Login(string username, string password)
//        {
//            var user = _context.Users.FirstOrDefault(u => u.Username == username);

//            if (user == null || !BCrypt.Verify(password, user.PasswordHash))
//                return null;

//            return user;
//        }

//    }
//}
