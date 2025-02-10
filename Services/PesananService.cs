using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DB;

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Services
{
    public class PesananService
    {
        private readonly ApplicationContext _context;

        public PesananService(ApplicationContext context)
        {
            _context = context;
        }

        public List<PesananDTO> GetListPesanan()
        {
            var data = _context.Pesanans.Select(x => new PesananDTO
            {
                Id = x.Id.ToString(),
                NamePembeli = x.NamePembeli,
                AlamatPembeli = x.AlamatPembeli,
                IdProduk = x.IdProduk,
                JumlaH = x.JumlaH,
                TanggalPesanan = x.TanggalPesanan != null ? x.TanggalPesanan.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",

            }).ToList();

            return data;
        }

        public PesananDTO GetPesananById(int id)
        {
            var data = _context.Pesanans.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            return new PesananDTO
            {
                Id = data.Id.ToString(),
                NamePembeli = data.NamePembeli,
                AlamatPembeli = data.AlamatPembeli,
                IdProduk = data.IdProduk,
                JumlaH = data.JumlaH,
                TanggalPesanan = data.TanggalPesanan != null ? data.TanggalPesanan.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
            };
        }

        public bool CheckIDProduk(int IdProduk)
        {
            return _context.Produks.Any(p => p.Id == IdProduk);
        }

        public bool CreatePesanan(PesananRequestDTO pesanan)
        {
            try
            {
                // Cek apakah produk yang dipesan ada dalam database
                if(!CheckIDProduk(pesanan.IdProduk))
                {
                    return false;
                }

                var newProduct = new Pesanan
                {

                    NamePembeli = pesanan.NamePembeli,
                    AlamatPembeli = pesanan.AlamatPembeli,
                    IdProduk = pesanan.IdProduk,
                    JumlaH = pesanan.JumlaH,
                    TanggalPesanan = DateTime.Now,

       
                };
                _context.Pesanans.Add(newProduct);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePesanan(int id, PesananRequestDTO product)
        {
            try
            {
                var existingProduct = _context.Pesanans.FirstOrDefault(x => x.Id == id);
                if (existingProduct != null)
                {
                    existingProduct.NamePembeli = product.NamePembeli;
                    existingProduct.AlamatPembeli = product.AlamatPembeli;
                    existingProduct.IdProduk = product.IdProduk;
                    existingProduct.JumlaH = product.JumlaH;
                    existingProduct.TanggalPesanan = DateTime.Now;
       

                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeletePesanan(int id)
        {
            try
            {
                var product = _context.Pesanans.FirstOrDefault(x => x.Id == id);
                if (product != null)
                {
                    _context.Pesanans.Remove(product);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
