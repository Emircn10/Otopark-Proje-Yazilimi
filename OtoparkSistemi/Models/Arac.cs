using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtoparkSistemi.Models
{
    public class Arac
    {
        [Key]
        public int AracId { get; set; }

        [Required(ErrorMessage = "Plaka zorunludur.")]
        [StringLength(15, ErrorMessage = "Plaka en fazla 15 karakter olabilir.")]
        [Display(Name = "Plaka")]
        public string Plaka { get; set; } = string.Empty;

        [Required(ErrorMessage = "Araç tipi zorunludur.")]
        [Display(Name = "Araç Tipi")]
        [StringLength(50)]
        public string AracTipi { get; set; } = string.Empty;

        [Display(Name = "Sahibi Adı")]
        [StringLength(100)]
        public string? SahibiAdi { get; set; }

        [Display(Name = "Sahibi Telefon")]
        [StringLength(20)]
        public string? SahibiTelefon { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public ICollection<Abonelik> Abonelikler { get; set; } = new List<Abonelik>();
        public ICollection<GirisKayit> GirisKayitlari { get; set; } = new List<GirisKayit>();
    }
}
