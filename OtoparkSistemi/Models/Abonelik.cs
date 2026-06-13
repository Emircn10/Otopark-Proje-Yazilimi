using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtoparkSistemi.Models
{
    public class Abonelik
    {
        [Key]
        public int AbonelikId { get; set; }

        [Required]
        public int AracId { get; set; }

        [Required(ErrorMessage = "Başlangıç tarihi zorunludur.")]
        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BaslangicTarihi { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi zorunludur.")]
        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BitisTarihi { get; set; }

        [Required(ErrorMessage = "Abonelik tipi zorunludur.")]
        [Display(Name = "Abonelik Tipi")]
        [StringLength(50)]
        public string AbonelikTipi { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Ücret (₺)")]
        [Range(0, double.MaxValue, ErrorMessage = "Ücret 0 veya daha büyük olmalıdır.")]
        public decimal Ucret { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool AktifMi { get; set; } = true;

        [ForeignKey("AracId")]
        public Arac? Arac { get; set; }
    }
}
