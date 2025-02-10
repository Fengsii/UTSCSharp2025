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

        //public bool Register(string username, string email, string password, string role)
        //{
        //    if (_context.Users.Any(u => u.Username == username || u.Email == email))
        //        return false;

        //    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        //    var user = new User
        //    {
        //        Username = username,
        //        Email = email,
        //        PasswordHash = hashedPassword,
        //        Role = role,
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    _context.Users.Add(user);
        //    _context.SaveChanges();

        //    return true;
        //}

        //public bool Register(string username, string email, string password, string role)
        //{
        //    try
        //    {
        //        // Cek apakah username atau email sudah ada
        //        if (_context.Users.Any(u => u.Username == username || u.Email == email))
        //        {
        //            return false; // Username atau Email sudah ada
        //        }

        //        // Hash password menggunakan BCrypt
        //        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        //        var user = new User
        //        {
        //            Username = username ?? throw new ArgumentNullException(nameof(username)),
        //            Email = email ?? throw new ArgumentNullException(nameof(email)),
        //            PasswordHash = hashedPassword,
        //            Role = role ?? "User", // Default role jika tidak disediakan
        //            CreatedAt = DateTime.UtcNow
        //        };

        //        _context.Users.Add(user);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.InnerException?.Message); // Debugging
        //        return false;
        //    }
        //}


        //public bool Register(string username, string email, string password, string role)
        //{
        //    try
        //    {
        //        Console.WriteLine($"Checking for existing user with username: {username} or email: {email}");

        //        // Cek apakah username atau email sudah ada
        //        if (_context.Users.Any(u => u.Username == username || u.Email == email))
        //        {
        //            Console.WriteLine("Duplicate username or email found.");
        //            return false; // Username atau Email sudah ada
        //        }

        //        // Hash password menggunakan BCrypt
        //        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        //        var user = new User
        //        {
        //            Username = username ?? throw new ArgumentNullException(nameof(username)),
        //            Email = email ?? throw new ArgumentNullException(nameof(email)),
        //            PasswordHash = hashedPassword,
        //            Role = role ?? "User", // Default role jika tidak disediakan
        //            CreatedAt = DateTime.UtcNow
        //        };

        //        _context.Users.Add(user);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error during registration: {ex.Message}");
        //        return false;
        //    }
        //}




        public bool Register(string username, string email, string password, string role)
        {
            try
            {
                // Cek apakah username atau email sudah ada
                if (_context.Users.Any(u => u.Username == username || u.Email == email))
                {
                    return false; // Username atau Email sudah ada
                }

                // Hash password menggunakan BCrypt
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var user = new User
                {
                    Username = username ?? throw new ArgumentNullException(nameof(username)),
                    Email = email ?? throw new ArgumentNullException(nameof(email)),
                    PasswordHash = hashedPassword,
                    Role = role ?? "User", // Default role jika tidak disediakan
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                _context.SaveChanges(); // Simpan perubahan ke database
                Console.WriteLine("User registered successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                return false;
            }
        }
















        public User Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
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
