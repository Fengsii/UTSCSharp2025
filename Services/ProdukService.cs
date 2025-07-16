using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DB;
using Microsoft.AspNetCore.Hosting; // Tambahkan ini untuk IWebHostEnvironment
using Microsoft.AspNetCore.Http; // Tambahkan ini untuk IFormFile

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Services
{
    public class ProdukService
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProdukService(ApplicationContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // Method SaveImage ditempatkan di sini (di dalam class ProdukService)
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "Images", "Produk");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("Images/Produk", uniqueFileName);
        }


        public async Task<bool> CreateProduk(ProdukRequestDTO product)
        {
            try
            {
                var imagePath = await SaveImage(product.ImageFile);

                var newProduct = new Produk
                {
                    NameProduk = product.NameProduk,
                    Supplier = product.Supplier,
                    TanggalKadalwarsa = product.TanggalKadalwarsa,
                    Harga = product.Harga,
                    CreateDate = DateTime.Now,
                    ImagePath = imagePath // Tambahkan ini
                };

                _context.Produks.Add(newProduct);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
                ImageUrl = !string.IsNullOrEmpty(x.ImagePath)
                            ? "/" + x.ImagePath.Replace("\\", "/")
                            : null
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
                ImageUrl = !string.IsNullOrEmpty(data.ImagePath)
                            ? "/" + data.ImagePath.Replace("\\", "/")
                            : null
            };
        }


        public async Task<bool> UpdateProdukAsync(int id, ProdukRequestDTO product)
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

                    if (product.ImageFile != null)
                    {
                        string imagePath = await SaveImage(product.ImageFile);
                        existingProduct.ImagePath = imagePath;
                    }

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
                    // Hapus file gambarnya juga jika ada
                    if (!string.IsNullOrEmpty(product.ImagePath))
                    {
                        var fullImagePath = Path.Combine(_environment.WebRootPath, product.ImagePath.Replace("/", "\\"));
                        if (File.Exists(fullImagePath))
                        {
                            File.Delete(fullImagePath);
                        }
                    }

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















        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>














        //public List<ProdukDTO> GetListProduk()
        //{
        //    var data = _context.Produks.Select(x => new ProdukDTO
        //    {
        //        Id = x.Id.ToString(),
        //        NameProduk = x.NameProduk,
        //        Supplier = x.Supplier,
        //        TanggalKadalwarsa = x.TanggalKadalwarsa,
        //        Harga = x.Harga,
        //        CreateDate = x.CreateDate != null ? x.CreateDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",

        //    }).ToList();

        //    return data;
        //}

        //public ProdukDTO GetProdukById(int id)
        //{
        //    var data = _context.Produks.FirstOrDefault(x => x.Id == id);
        //    if (data == null)
        //    {
        //        return null;
        //    }

        //    return new ProdukDTO
        //    {
        //        Id = data.Id.ToString(),
        //        NameProduk = data.NameProduk,
        //        Supplier = data.Supplier,
        //        TanggalKadalwarsa = data.TanggalKadalwarsa,
        //        Harga = data.Harga,
        //        CreateDate = data.CreateDate != null ? data.CreateDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
        //    };
        //}

        //public bool CreateProduk(ProdukRequestDTO product)
        //{
        //    try
        //    {
        //        var newProduct = new Produk
        //        {
        //            NameProduk = product.NameProduk,
        //            Supplier = product.Supplier,
        //            TanggalKadalwarsa = product.TanggalKadalwarsa,
        //            Harga = product.Harga,
        //            CreateDate = DateTime.Now,
        //        };
        //        _context.Produks.Add(newProduct);
        //        _context.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public bool UpdateProduk(int id, ProdukRequestDTO product)
        //{
        //    try
        //    {
        //        var existingProduct = _context.Produks.FirstOrDefault(x => x.Id == id);
        //        if (existingProduct != null)
        //        {
        //           existingProduct.NameProduk = product.NameProduk;
        //           existingProduct.Supplier = product.Supplier;
        //           existingProduct.TanggalKadalwarsa = product.TanggalKadalwarsa;
        //           existingProduct.Harga = product.Harga;


        //            _context.SaveChanges();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public bool DeleteProduk(int id)
        //{
        //    try
        //    {
        //        var product = _context.Produks.FirstOrDefault(x => x.Id == id);
        //        if (product != null)
        //        {
        //            _context.Produks.Remove(product);
        //            _context.SaveChanges();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
