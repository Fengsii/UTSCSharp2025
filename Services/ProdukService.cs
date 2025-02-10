using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DB;

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Services
{
    public class ProdukService
    {
        private readonly ApplicationContext _context;

        public ProdukService(ApplicationContext context)
        {
            _context = context;
        }

        public List<ProdukDTO> GetListProduk()
        {
            var data = _context.Produks.Select(x => new ProdukDTO
            {
                Id = x.Id.ToString(),
                NameProduk = x.NameProduk,
                Supplier = x.Supplier,
                TanggalKadalwarsa = x.TanggalKadalwarsa,
                Harga = x.Harga,
                CreateDate = x.CreateDate != null ? x.CreateDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",

            }).ToList();

            return data;
        }

        public ProdukDTO GetProdukById(int id)
        {
            var data = _context.Produks.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            return new ProdukDTO
            {
                Id = data.Id.ToString(),
                NameProduk = data.NameProduk,
                Supplier = data.Supplier,
                TanggalKadalwarsa = data.TanggalKadalwarsa,
                Harga = data.Harga,
                CreateDate = data.CreateDate != null ? data.CreateDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
            };
        }

        public bool CreateProduk(ProdukRequestDTO product)
        {
            try
            {
                var newProduct = new Produk
                {
                    NameProduk = product.NameProduk,
                    Supplier = product.Supplier,
                    TanggalKadalwarsa = product.TanggalKadalwarsa,
                    Harga = product.Harga,
                    CreateDate = DateTime.Now,
                };
                _context.Produks.Add(newProduct);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateProduk(int id, ProdukRequestDTO product)
        {
            try
            {
                var existingProduct = _context.Produks.FirstOrDefault(x => x.Id == id);
                if (existingProduct != null)
                {
                   existingProduct.NameProduk = product.NameProduk;
                   existingProduct.Supplier = product.Supplier;
                   existingProduct.TanggalKadalwarsa = product.TanggalKadalwarsa;
                   existingProduct.Harga = product.Harga;
                    

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

        public bool DeleteProduk(int id)
        {
            try
            {
                var product = _context.Produks.FirstOrDefault(x => x.Id == id);
                if (product != null)
                {
                    _context.Produks.Remove(product);
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
