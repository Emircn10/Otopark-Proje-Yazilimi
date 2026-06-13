using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtoparkSistemi.Models
{
    public class GirisKayit
    {
        [Key]
        public int KayitId { get; set; }

        [Required]
        public int AracId { get; set; }

        [Required]
        [Display(Name = "Giriş Tarihi")]
        public DateTime GirisTarihi { get; set; }

        [Display(Name = "Çıkış Tarihi")]
        public DateTime? CikisTarihi { get; set; }

        [Display(Name = "Toplam Süre (dk)")]
        public int? ToplamSure { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Ödenen Ücret (₺)")]
        public decimal? OdenenUcret { get; set; }

        [Display(Name = "Abonelik Kullanıldı mı?")]
        public bool AbonelikKullanildiMi { get; set; } = false;

        [Display(Name = "Ziyaretçi mi?")]
        public bool ZiyaretciMi { get; set; } = false;

        [Display(Name = "Ödeme Durumu")]
        [StringLength(50)]
        public string OdemeDurumu { get; set; } = "Bekliyor";

        [ForeignKey("AracId")]
        public Arac? Arac { get; set; }
    }
}
