namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DB
{
    public class Pesanan
    {
        public int Id { get; set; }
        public string NamePembeli { get; set; }
        public string AlamatPembeli { get; set; }
        public int IdProduk { get; set; }
        public int JumlaH {  get; set; }
        public DateTime? TanggalPesanan { get; set; }
    }
}
