using System.ComponentModel.DataAnnotations;

namespace ProjeYonetimAPI.Models
{
    // ── Ekip Üyesi ──────────────────────────────
    public class EkipUyesi
    {
        public int Id { get; set; }
        [Required, MaxLength(100)] public string AdSoyad { get; set; } = "";
        [MaxLength(10)] public string Kisaltma { get; set; } = "";
        [MaxLength(150)] public string Rol { get; set; } = "";
        [MaxLength(500)] public string? Uzmanliklar { get; set; }   // virgülle ayrılmış
        public decimal AylikUcret { get; set; }
        public int ProjedekiAy { get; set; }
        [MaxLength(30)] public string RenkKod { get; set; } = "#3b82f6";
        [MaxLength(30)] public string RenkKod2 { get; set; } = "#06b6d4";
    }

    // ── Sprint ───────────────────────────────────
    public class Sprint
    {
        public int Id { get; set; }
        public int SprintNo { get; set; }
        [Required, MaxLength(200)] public string Baslik { get; set; } = "";
        [MaxLength(500)] public string? Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        /// <summary>done | active | pending</summary>
        [MaxLength(20)] public string Durum { get; set; } = "pending";
        public int HaftaSayisi { get; set; }
        public ICollection<SprintGorev> Gorevler { get; set; } = new List<SprintGorev>();
    }

    // ── Sprint Görevi ────────────────────────────
    public class SprintGorev
    {
        public int Id { get; set; }
        public int SprintId { get; set; }
        public Sprint? Sprint { get; set; }
        [Required, MaxLength(300)] public string GorevAdi { get; set; } = "";
        [MaxLength(100)] public string Sorumlu { get; set; } = "";
        [MaxLength(10)] public string SorumluKisaltma { get; set; } = "";
        [MaxLength(30)] public string SorumluRenk { get; set; } = "#3b82f6";
        /// <summary>done | active | pending</summary>
        [MaxLength(20)] public string Durum { get; set; } = "done";
        public int SiraNo { get; set; }
        
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
    }

    // ── Haftalık Rapor ───────────────────────────
    public class HaftalikRapor
    {
        public int Id { get; set; }
        public int HaftaNo { get; set; }
        [MaxLength(50)] public string HaftaAdi { get; set; } = "";
        [MaxLength(100)] public string Tarihler { get; set; } = "";
        [MaxLength(50)] public string Sprint { get; set; } = "";
        public int TamamlananGorev { get; set; }
        public int DevamEdenGorev { get; set; }
        public int Engelleyici { get; set; }
        public int Verimlilik { get; set; }
        [MaxLength(2000)] public string? Notlar { get; set; }
    }

    // ── Bütçe Kalemi ─────────────────────────────
    public class BütçeKalemi
    {
        public int Id { get; set; }
        [MaxLength(150)] public string Kategori { get; set; } = "";
        [MaxLength(20)] public string KategoriIkon { get; set; } = "💼";
        public decimal Tutar { get; set; }
        [MaxLength(300)] public string? Aciklama { get; set; }
        [MaxLength(30)] public string RenkKod { get; set; } = "#3b82f6";
        public int SiraNo { get; set; }
    }

    // ── Proje Görevi (Bakım/Güncelleme) ──────────
    public class ProjeGorev
    {
        public int Id { get; set; }
        [Required, MaxLength(300)] public string Baslik { get; set; } = "";
        [MaxLength(2000)] public string? Aciklama { get; set; }
        [Required, MaxLength(100)] public string GorevTuru { get; set; } = "";
        [Required, MaxLength(100)] public string AtananKisi { get; set; } = "";
        [MaxLength(10)] public string AtananKisaltma { get; set; } = "";
        [MaxLength(30)] public string AtananRenk { get; set; } = "#3b82f6";
        /// <summary>low | medium | high | critical</summary>
        [MaxLength(20)] public string Oncelik { get; set; } = "medium";
        /// <summary>open | inprogress | done</summary>
        [MaxLength(20)] public string Durum { get; set; } = "open";
        [MaxLength(100)] public string? Modul { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
