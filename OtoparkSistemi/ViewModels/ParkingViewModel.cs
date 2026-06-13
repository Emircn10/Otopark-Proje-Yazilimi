using System.ComponentModel.DataAnnotations;

namespace OtoparkSistemi.ViewModels
{
    public class GirisViewModel
    {
        [Required(ErrorMessage = "Plaka zorunludur.")]
        [Display(Name = "Plaka")]
        [StringLength(15)]
        public string Plaka { get; set; } = string.Empty;

        public string? MesajTuru { get; set; }
        public string? Mesaj { get; set; }
        public bool AbonelikVar { get; set; }
        public string? AbonelikTipi { get; set; }
    }

    public class CikisViewModel
    {
        public int KayitId { get; set; }
        public string Plaka { get; set; } = string.Empty;
        public string AracTipi { get; set; } = string.Empty;
        public DateTime GirisTarihi { get; set; }
        public DateTime CikisTarihi { get; set; } = DateTime.Now;
        public int ToplamSureDakika { get; set; }
        public decimal HesaplananUcret { get; set; }
        public bool AbonelikKullanildi { get; set; }
    }
}
