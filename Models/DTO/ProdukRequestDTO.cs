namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO
{
    public class ProdukRequestDTO
    {
        public string NameProduk { get; set; }
        public string Supplier { get; set; }
        public DateTime TanggalKadalwarsa { get; set; }
        public decimal Harga { get; set; }
        public IFormFile ImageFile { get; set; } // Untuk upload file
    }
}
