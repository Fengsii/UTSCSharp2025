namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DB
{
    public class Produk
    {
        public int Id { get; set; }
        public string NameProduk { get; set; }
        public string Supplier {  get; set; }
        public DateTime TanggalKadalwarsa { get; set; }
        public decimal Harga { get; set; }
        public string ImagePath { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
