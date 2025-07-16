using Microsoft.EntityFrameworkCore;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DB;

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Models
{
    public class ApplicationContext : DbContext
    {
        private readonly IWebHostEnvironment _env;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IWebHostEnvironment env) : base(options)
        {
            _env = env;
        }
        public virtual DbSet<Produk> Produks { get; set; }
        public virtual DbSet<Pesanan> Pesanans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produk>().HasData(
                new Produk
                {
                    Id = 1,
                    NameProduk = "Produk Sample 1",
                    Supplier = "Supplier A",
                    TanggalKadalwarsa = DateTime.Now.AddYears(1),
                    Harga = 100000,
                    CreateDate = DateTime.Now,
                    ImagePath = "Images/Produk/aa.jpg" // Path yang benar
                },
                new Produk
                {
                    Id = 2,
                    NameProduk = "Produk Sample 2",
                    Supplier = "Supplier B",
                    TanggalKadalwarsa = DateTime.Now.AddYears(2),
                    Harga = 150000,
                    CreateDate = DateTime.Now,
                    ImagePath = "Images/Produk/aa.jpg" // Path yang benar
                }
            );
        }


    }
}
