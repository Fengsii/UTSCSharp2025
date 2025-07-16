namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO
{
    public class ProdukDTO
    {
        public string Id { get; set; }
        public string NameProduk { get; set; }
        public string Supplier { get; set; }
        public DateTime TanggalKadalwarsa { get; set; }
        public decimal Harga { get; set; }
        public string ImageUrl { get; set; }
        public string CreateDate { get; set; }
    }
}
