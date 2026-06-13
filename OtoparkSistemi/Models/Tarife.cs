using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtoparkSistemi.Models
{
    public class Tarife
    {
        [Key]
        public int TarifeId { get; set; }

        [Required(ErrorMessage = "Araç tipi zorunludur.")]
        [Display(Name = "Araç Tipi")]
        [StringLength(50)]
        public string AracTipi { get; set; } = string.Empty;

        /// <summary>0-1 saat arası sabit ücret</summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "İlk 1 Saat (₺)")]
        [Range(0, double.MaxValue, ErrorMessage = "Geçerli bir ücret girin.")]
        public decimal Ilk1SaatUcret { get; set; }

        /// <summary>1-3 saat arası saatlik ücret</summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "1-3 Saat Arası (₺/saat)")]
        [Range(0, double.MaxValue)]
        public decimal Saat1_3Ucret { get; set; }

        /// <summary>3 saatten sonraki her saat için ücret</summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "3 Saat Sonrası (₺/saat)")]
        [Range(0, double.MaxValue)]
        public decimal Saat3PlusSaatlik { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Günlük Maksimum (₺)")]
        [Range(0, double.MaxValue)]
        public decimal GunlukMaksimum { get; set; }

        [Required]
        [Display(Name = "Geçerlilik Başlangıç")]
        [DataType(DataType.Date)]
        public DateTime GecerlilikBaslangic { get; set; }

        [Display(Name = "Geçerlilik Bitiş")]
        [DataType(DataType.Date)]
        public DateTime? GecerlilikBitis { get; set; }
    }
}
