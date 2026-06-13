namespace OtoparkSistemi.ViewModels
{
    public class DashboardViewModel
    {
        public int OtoparkdakiAracSayisi { get; set; }
        public decimal BugunkuGelir { get; set; }
        public int AktifAbonelikSayisi { get; set; }
        public int ToplamKapasite { get; set; }
        public double DolulukOrani => ToplamKapasite > 0 ? (double)OtoparkdakiAracSayisi / ToplamKapasite * 100 : 0;
        public List<SonKayitViewModel> SonKayitlar { get; set; } = new();
    }

    public class SonKayitViewModel
    {
        public int KayitId { get; set; }
        public string Plaka { get; set; } = string.Empty;
        public string AracTipi { get; set; } = string.Empty;
        public DateTime GirisTarihi { get; set; }
        public DateTime? CikisTarihi { get; set; }
        public decimal? OdenenUcret { get; set; }
        public string OdemeDurumu { get; set; } = string.Empty;
        public bool Aktif => CikisTarihi == null;
    }
}
