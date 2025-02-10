using Microsoft.EntityFrameworkCore;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DB;

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public virtual DbSet<Produk> Produks { get; set; }
        public virtual DbSet<Pesanan> Pesanans { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
