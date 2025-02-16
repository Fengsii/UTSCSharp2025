using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DB;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models;
namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Services
{
    public class UserService
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public (bool Success, string Message) Register(string username, string email, string password, string role)
        {
            try
            {
                Console.WriteLine($"Attempting to register user: {username}, email: {email}");

                // Cek database terlebih dahulu
                var existingUser = _context.Users.FirstOrDefault(u =>
                    u.Username.ToLower() == username.ToLower() ||
                    u.Email.ToLower() == email.ToLower());

                if (existingUser != null)
                {
                    Console.WriteLine($"User exists - Username: {existingUser.Username}, Email: {existingUser.Email}");
                    return (false, "Username atau email sudah terdaftar");
                }






                //// Cek username sudah ada atau belum
                //if (_context.Users.Any(u => u.Username.ToLower() == username.ToLower()))
                //{
                //    return (false, "Username sudah digunakan");
                //}

                //// Cek email sudah ada atau belum
                //if (_context.Users.Any(u => u.Email.ToLower() == email.ToLower()))
                //{
                //    return (false, "Email sudah terdaftar");
                //}

                // Hash password menggunakan BCrypt
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var user = new User
                {
                    //?? throw new ArgumentNullException(nameof(username)),
                    // ?? throw new ArgumentNullException(nameof(email)),  ?? "User"
                    Username = username,
                    Email = email,
                    PasswordHash = hashedPassword,
                    Role = role,
                    CreatedAt = DateTime.UtcNow,
                    RefreshTokenExpiryTime = DateTime.UtcNow
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return (true, "Registrasi berhasil");
            }
            catch (Exception ex)
            {
                return (false, $"Error during registration: {ex.Message}");
            }
        }

        public User? Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;
            return user;
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User? GetUserByRefreshToken(string refreshToken)
        {
            return _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
