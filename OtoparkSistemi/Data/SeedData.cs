using OtoparkSistemi.Models;

namespace OtoparkSistemi.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Araclar.Any()) return;

            // Tarifeler
            var tarifeler = new List<Tarife>
            {
                new() { AracTipi = "Otomobil",   Ilk1SaatUcret = 20m, Saat1_3Ucret = 25m, Saat3PlusSaatlik = 15m, GunlukMaksimum = 120m, GecerlilikBaslangic = new DateTime(2024, 1, 1) },
                new() { AracTipi = "Motosiklet", Ilk1SaatUcret = 10m, Saat1_3Ucret = 12m, Saat3PlusSaatlik = 8m,  GunlukMaksimum = 60m,  GecerlilikBaslangic = new DateTime(2024, 1, 1) },
                new() { AracTipi = "Kamyonet",   Ilk1SaatUcret = 30m, Saat1_3Ucret = 35m, Saat3PlusSaatlik = 25m, GunlukMaksimum = 180m, GecerlilikBaslangic = new DateTime(2024, 1, 1) },
            };
            context.Tarifeler.AddRange(tarifeler);
            context.SaveChanges();

            // Araçlar
            var araclar = new List<Arac>
            {
                new() { Plaka = "34ABC123", AracTipi = "Otomobil",   SahibiAdi = "Ahmet Yılmaz",  SahibiTelefon = "05551112233", OlusturmaTarihi = DateTime.Now.AddMonths(-6) },
                new() { Plaka = "06DEF456", AracTipi = "Otomobil",   SahibiAdi = "Fatma Kaya",    SahibiTelefon = "05442223344", OlusturmaTarihi = DateTime.Now.AddMonths(-5) },
                new() { Plaka = "35GHI789", AracTipi = "Motosiklet", SahibiAdi = "Mehmet Demir",  SahibiTelefon = "05333334455", OlusturmaTarihi = DateTime.Now.AddMonths(-4) },
                new() { Plaka = "16JKL012", AracTipi = "Kamyonet",   SahibiAdi = "Ayşe Çelik",   SahibiTelefon = "05224445566", OlusturmaTarihi = DateTime.Now.AddMonths(-3) },
                new() { Plaka = "07MNO345", AracTipi = "Otomobil",   SahibiAdi = "Ali Şahin",    SahibiTelefon = "05115556677", OlusturmaTarihi = DateTime.Now.AddMonths(-3) },
                new() { Plaka = "01PQR678", AracTipi = "Motosiklet", SahibiAdi = "Zeynep Arslan", SahibiTelefon = "05006667788", OlusturmaTarihi = DateTime.Now.AddMonths(-2) },
                new() { Plaka = "34STU901", AracTipi = "Otomobil",   SahibiAdi = "Mustafa Koç",  SahibiTelefon = "05427778899", OlusturmaTarihi = DateTime.Now.AddMonths(-2) },
                new() { Plaka = "06VWX234", AracTipi = "Kamyonet",   SahibiAdi = "Elif Öztürk",  SahibiTelefon = "05328889900", OlusturmaTarihi = DateTime.Now.AddMonths(-1) },
                new() { Plaka = "35YZA567", AracTipi = "Otomobil",   SahibiAdi = "Hasan Yıldız", SahibiTelefon = "05559990011", OlusturmaTarihi = DateTime.Now.AddMonths(-1) },
                new() { Plaka = "16BCD890", AracTipi = "Otomobil",   SahibiAdi = "Hatice Güneş", SahibiTelefon = "05440001122", OlusturmaTarihi = DateTime.Now.AddDays(-15)  },
            };
            context.Araclar.AddRange(araclar);
            context.SaveChanges();

            // Abonelikler
            var abonelikler = new List<Abonelik>
            {
                new() { AracId = araclar[0].AracId, BaslangicTarihi = DateTime.Now.AddMonths(-1), BitisTarihi = DateTime.Now.AddMonths(1),  AbonelikTipi = "Aylık",   Ucret = 500m,  AktifMi = true  },
                new() { AracId = araclar[1].AracId, BaslangicTarihi = DateTime.Now.AddMonths(-2), BitisTarihi = DateTime.Now.AddMonths(1),  AbonelikTipi = "3 Aylık", Ucret = 1200m, AktifMi = true  },
                new() { AracId = araclar[4].AracId, BaslangicTarihi = DateTime.Now.AddYears(-1),  BitisTarihi = DateTime.Now.AddDays(-5),   AbonelikTipi = "Yıllık",  Ucret = 4000m, AktifMi = false },
                new() { AracId = araclar[6].AracId, BaslangicTarihi = DateTime.Now.AddDays(-10),  BitisTarihi = DateTime.Now.AddDays(20),   AbonelikTipi = "Aylık",   Ucret = 500m,  AktifMi = true  },
                new() { AracId = araclar[8].AracId, BaslangicTarihi = DateTime.Now.AddMonths(-3), BitisTarihi = DateTime.Now.AddMonths(9),  AbonelikTipi = "Yıllık",  Ucret = 4000m, AktifMi = true  },
            };
            context.Abonelikler.AddRange(abonelikler);
            context.SaveChanges();

            // Giriş Kayıtları (geçmiş)
            var rand = new Random(42);
            int[] abonelikliIndeksler = { 0, 1, 6, 8 };
            var kayitlar = new List<GirisKayit>();

            for (int i = 0; i < 18; i++)
            {
                var aracIndex = rand.Next(0, 10);
                var arac = araclar[aracIndex];
                var girisTarihi = DateTime.Now.AddDays(-rand.Next(1, 30)).AddHours(-rand.Next(2, 10));
                var sure = rand.Next(30, 480);
                var cikis = girisTarihi.AddMinutes(sure);
                bool abonelikVar = abonelikliIndeksler.Contains(aracIndex);
                decimal ucret = abonelikVar ? 0m : Math.Min(Math.Round((sure / 60m) * 20m, 2), 120m);

                kayitlar.Add(new GirisKayit
                {
                    AracId = arac.AracId,
                    GirisTarihi = girisTarihi,
                    CikisTarihi = cikis,
                    ToplamSure = sure,
                    AbonelikKullanildiMi = abonelikVar,
                    OdenenUcret = ucret,
                    OdemeDurumu = abonelikVar ? "Abonelik" : "Ödendi"
                });
            }

            // 2 aktif (çıkış yok)
            kayitlar.Add(new GirisKayit { AracId = araclar[2].AracId, GirisTarihi = DateTime.Now.AddHours(-2), OdemeDurumu = "Bekliyor", AbonelikKullanildiMi = false });
            kayitlar.Add(new GirisKayit { AracId = araclar[5].AracId, GirisTarihi = DateTime.Now.AddHours(-1), OdemeDurumu = "Bekliyor", AbonelikKullanildiMi = false });

            context.GirisKayitlari.AddRange(kayitlar);
            context.SaveChanges();
        }
    }
}
