using Microsoft.EntityFrameworkCore;
using ProjeYonetimAPI.Data;
using ProjeYonetimAPI.Models;

namespace ProjeYonetimAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(ProjeDbContext db)
        {
            // ── Ekip Üyeleri ───────────────────────────────────────────────────────
            if (!db.EkipUyeleri.Any())
            {
                db.EkipUyeleri.AddRange(
                    new EkipUyesi { AdSoyad="Can Öztürk",   Kisaltma="CÖ", Rol="Yazılım Mimarı & Backend Lead",    Uzmanliklar="Node.js,TypeScript,REST API,JWT,Mimari",      AylikUcret=52000, ProjedekiAy=6, RenkKod="#3b82f6", RenkKod2="#06b6d4" },
                    new EkipUyesi { AdSoyad="Ahmet Yılmaz", Kisaltma="AY", Rol="Kıdemli Backend Geliştirici",      Uzmanliklar="PostgreSQL,Prisma ORM,Redis,WebSocket,İyzico API", AylikUcret=44000, ProjedekiAy=4, RenkKod="#8b5cf6", RenkKod2="#ec4899" },
                    new EkipUyesi { AdSoyad="Selin Erdoğan",Kisaltma="SE", Rol="Backend Geliştirici",              Uzmanliklar="Node.js,Rezervasyon,Plaka API,Abonelik",      AylikUcret=38000, ProjedekiAy=4, RenkKod="#06b6d4", RenkKod2="#10b981" },
                    new EkipUyesi { AdSoyad="Elif Şahin",   Kisaltma="EŞ", Rol="UI/UX & Frontend Tasarımcı",      Uzmanliklar="Figma,React.js,CSS/SCSS,Animasyon",           AylikUcret=35000, ProjedekiAy=4, RenkKod="#ec4899", RenkKod2="#8b5cf6" },
                    new EkipUyesi { AdSoyad="Fatma Demir",  Kisaltma="FD", Rol="Mobil Uygulama Geliştirici",      Uzmanliklar="React Native,Expo,FCM,iOS/Android",           AylikUcret=40000, ProjedekiAy=2, RenkKod="#f59e0b", RenkKod2="#ec4899" },
                    new EkipUyesi { AdSoyad="Yusuf Arslan", Kisaltma="YA", Rol="Frontend Geliştirici",            Uzmanliklar="React.js,Harita API,Dashboard,Raporlama",     AylikUcret=34000, ProjedekiAy=4, RenkKod="#f59e0b", RenkKod2="#10b981" },
                    new EkipUyesi { AdSoyad="Mert Kaya",    Kisaltma="MK", Rol="DevOps & Güvenlik Mühendisi",     Uzmanliklar="Docker,AWS,CI/CD,SSL/TLS,Pentest",            AylikUcret=46000, ProjedekiAy=4, RenkKod="#10b981", RenkKod2="#3b82f6" },
                    new EkipUyesi { AdSoyad="Berk Toprak",  Kisaltma="BT", Rol="QA & Test Mühendisi",             Uzmanliklar="Jest,Cypress,k6,UAT",                         AylikUcret=32000, ProjedekiAy=2, RenkKod="#f59e0b", RenkKod2="#ef4444" },
                    new EkipUyesi { AdSoyad="Nisa Çelik",   Kisaltma="NÇ", Rol="Proje Yöneticisi (PM)",           Uzmanliklar="Scrum,Agile,KVKK,Finans",                     AylikUcret=48000, ProjedekiAy=6, RenkKod="#f59e0b", RenkKod2="#8b5cf6" }
                );
            }

            // ── Sprintler ──────────────────────────────────────────────────────────
            if (!db.Sprintler.Any())
            {
                var sprintler = new List<Sprint>
                {
                    new Sprint { SprintNo=1, Baslik="Analiz, Planlama & UI/UX Tasarımı",   BaslangicTarihi=new DateTime(2026,1,6),  BitisTarihi=new DateTime(2026,1,31), Durum="done", HaftaSayisi=4,
                        Gorevler = new List<SprintGorev> {
                            new SprintGorev { GorevAdi="İhtiyaç analizi ve paydaş görüşmeleri", Sorumlu="Can Öztürk",    SorumluKisaltma="CÖ", SorumluRenk="#3b82f6", Durum="done", SiraNo=1, BaslangicTarihi=new DateTime(2026,1,6), BitisTarihi=new DateTime(2026,1,10) },
                            new SprintGorev { GorevAdi="Sistem mimarisi dokümantasyonu",         Sorumlu="Can Öztürk",    SorumluKisaltma="CÖ", SorumluRenk="#3b82f6", Durum="done", SiraNo=2, BaslangicTarihi=new DateTime(2026,1,11), BitisTarihi=new DateTime(2026,1,14) },
                            new SprintGorev { GorevAdi="Veritabanı şema tasarımı",              Sorumlu="Ahmet Yılmaz",  SorumluKisaltma="AY", SorumluRenk="#8b5cf6", Durum="done", SiraNo=3, BaslangicTarihi=new DateTime(2026,1,13), BitisTarihi=new DateTime(2026,1,18) },
                            new SprintGorev { GorevAdi="Wireframe ve prototip hazırlama",       Sorumlu="Elif Şahin",    SorumluKisaltma="EŞ", SorumluRenk="#ec4899", Durum="done", SiraNo=4, BaslangicTarihi=new DateTime(2026,1,15), BitisTarihi=new DateTime(2026,1,22) },
                            new SprintGorev { GorevAdi="UI/UX tasarım sistemi oluşturma",       Sorumlu="Elif Şahin",    SorumluKisaltma="EŞ", SorumluRenk="#ec4899", Durum="done", SiraNo=5, BaslangicTarihi=new DateTime(2026,1,23), BitisTarihi=new DateTime(2026,1,28) },
                            new SprintGorev { GorevAdi="Sprint planlaması ve görev dağılımı",   Sorumlu="Nisa Çelik",    SorumluKisaltma="NÇ", SorumluRenk="#f59e0b", Durum="done", SiraNo=6, BaslangicTarihi=new DateTime(2026,1,28), BitisTarihi=new DateTime(2026,1,29) },
                            new SprintGorev { GorevAdi="Teknoloji stack seçimi",                Sorumlu="Can Öztürk",    SorumluKisaltma="CÖ", SorumluRenk="#3b82f6", Durum="done", SiraNo=7, BaslangicTarihi=new DateTime(2026,1,20), BitisTarihi=new DateTime(2026,1,24) },
                            new SprintGorev { GorevAdi="CI/CD pipeline tasarımı",               Sorumlu="Mert Kaya",     SorumluKisaltma="MK", SorumluRenk="#10b981", Durum="done", SiraNo=8, BaslangicTarihi=new DateTime(2026,1,25), BitisTarihi=new DateTime(2026,1,31) },
                        }
                    },
                    new Sprint { SprintNo=2, Baslik="Veritabanı & Backend Altyapı",          BaslangicTarihi=new DateTime(2026,2,3),  BitisTarihi=new DateTime(2026,2,28), Durum="done", HaftaSayisi=4,
                        Gorevler = new List<SprintGorev> {
                            new SprintGorev { GorevAdi="PostgreSQL veritabanı kurulumu",        Sorumlu="Ahmet Yılmaz",  SorumluKisaltma="AY", SorumluRenk="#8b5cf6", Durum="done", SiraNo=1, BaslangicTarihi=new DateTime(2026,2,3), BitisTarihi=new DateTime(2026,2,5) },
                            new SprintGorev { GorevAdi="ORM (Prisma) entegrasyonu",             Sorumlu="Ahmet Yılmaz",  SorumluKisaltma="AY", SorumluRenk="#8b5cf6", Durum="done", SiraNo=2, BaslangicTarihi=new DateTime(2026,2,6), BitisTarihi=new DateTime(2026,2,10) },
                            new SprintGorev { GorevAdi="JWT kimlik doğrulama sistemi",          Sorumlu="Can Öztürk",    SorumluKisaltma="CÖ", SorumluRenk="#3b82f6", Durum="done", SiraNo=3, BaslangicTarihi=new DateTime(2026,2,5), BitisTarihi=new DateTime(2026,2,12) },
                            new SprintGorev { GorevAdi="RESTful API temel yapısı",              Sorumlu="Can Öztürk",    SorumluKisaltma="CÖ", SorumluRenk="#3b82f6", Durum="done", SiraNo=4, BaslangicTarihi=new DateTime(2026,2,10), BitisTarihi=new DateTime(2026,2,18) },
                            new SprintGorev { GorevAdi="Kullanıcı yönetim modülü",              Sorumlu="Selin Erdoğan", SorumluKisaltma="SE", SorumluRenk="#06b6d4", Durum="done", SiraNo=5, BaslangicTarihi=new DateTime(2026,2,15), BitisTarihi=new DateTime(2026,2,22) },
                            new SprintGorev { GorevAdi="Rol tabanlı yetkilendirme (RBAC)",      Sorumlu="Mert Kaya",     SorumluKisaltma="MK", SorumluRenk="#10b981", Durum="done", SiraNo=6, BaslangicTarihi=new DateTime(2026,2,20), BitisTarihi=new DateTime(2026,2,25) },
                            new SprintGorev { GorevAdi="Docker & konteyner ortamı",             Sorumlu="Mert Kaya",     SorumluKisaltma="MK", SorumluRenk="#10b981", Durum="done", SiraNo=7, BaslangicTarihi=new DateTime(2026,2,15), BitisTarihi=new DateTime(2026,2,22) },
                            new SprintGorev { GorevAdi="Redis önbellek katmanı",                Sorumlu="Ahmet Yılmaz",  SorumluKisaltma="AY", SorumluRenk="#8b5cf6", Durum="done", SiraNo=8, BaslangicTarihi=new DateTime(2026,2,22), BitisTarihi=new DateTime(2026,2,26) },
                            new SprintGorev { GorevAdi="Unit test altyapısı (Jest)",            Sorumlu="Berk Toprak",   SorumluKisaltma="BT", SorumluRenk="#f59e0b", Durum="done", SiraNo=9, BaslangicTarihi=new DateTime(2026,2,25), BitisTarihi=new DateTime(2026,2,28) },
                        }
                    },
                    new Sprint { SprintNo=3, Baslik="Otopark Çekirdek Modülleri",            BaslangicTarihi=new DateTime(2026,3,3),  BitisTarihi=new DateTime(2026,3,31), Durum="done", HaftaSayisi=4,
                        Gorevler = new List<SprintGorev> {
                            new SprintGorev { GorevAdi="Park alanı yönetim modülü",             Sorumlu="Selin Erdoğan", SorumluKisaltma="SE", SorumluRenk="#06b6d4", Durum="done", SiraNo=1, BaslangicTarihi=new DateTime(2026,3,3), BitisTarihi=new DateTime(2026,3,10) },
                            new SprintGorev { GorevAdi="Araç giriş/çıkış sistemi (plaka tanıma)",Sorumlu="Selin Erdoğan",SorumluKisaltma="SE", SorumluRenk="#06b6d4", Durum="done", SiraNo=2, BaslangicTarihi=new DateTime(2026,3,8), BitisTarihi=new DateTime(2026,3,16) },
                            new SprintGorev { GorevAdi="Gerçek zamanlı doluluk takibi (WS)",    Sorumlu="Ahmet Yılmaz",  SorumluKisaltma="AY", SorumluRenk="#8b5cf6", Durum="done", SiraNo=3, BaslangicTarihi=new DateTime(2026,3,14), BitisTarihi=new DateTime(2026,3,22) },
                            new SprintGorev { GorevAdi="Rezervasyon sistemi çekirdek",          Sorumlu="Selin Erdoğan", SorumluKisaltma="SE", SorumluRenk="#06b6d4", Durum="done", SiraNo=4, BaslangicTarihi=new DateTime(2026,3,20), BitisTarihi=new DateTime(2026,3,28) },
                            new SprintGorev { GorevAdi="Admin dashboard (React)",               Sorumlu="Yusuf Arslan",  SorumluKisaltma="YA", SorumluRenk="#f59e0b", Durum="done", SiraNo=5, BaslangicTarihi=new DateTime(2026,3,5), BitisTarihi=new DateTime(2026,3,20) },
                            new SprintGorev { GorevAdi="Otopark harita görünümü",               Sorumlu="Elif Şahin",    SorumluKisaltma="EŞ", SorumluRenk="#ec4899", Durum="done", SiraNo=6, BaslangicTarihi=new DateTime(2026,3,18), BitisTarihi=new DateTime(2026,3,28) },
                            new SprintGorev { GorevAdi="Raporlama & analitik modülü",           Sorumlu="Yusuf Arslan",  SorumluKisaltma="YA", SorumluRenk="#f59e0b", Durum="done", SiraNo=7, BaslangicTarihi=new DateTime(2026,3,25), BitisTarihi=new DateTime(2026,3,31) },
                        }
                    },
                    new Sprint { SprintNo=4, Baslik="Ödeme, Abonelik & Entegrasyonlar",      BaslangicTarihi=new DateTime(2026,4,7),  BitisTarihi=new DateTime(2026,5,9),  Durum="done", HaftaSayisi=5,
                        Gorevler = new List<SprintGorev> {
                            new SprintGorev { GorevAdi="İyzico ödeme entegrasyonu",             Sorumlu="Ahmet Yılmaz",  SorumluKisaltma="AY", SorumluRenk="#8b5cf6", Durum="done", SiraNo=1, BaslangicTarihi=new DateTime(2026,4,7), BitisTarihi=new DateTime(2026,4,15) },
                            new SprintGorev { GorevAdi="Kredi kartı tokenizasyonu",             Sorumlu="Ahmet Yılmaz",  SorumluKisaltma="AY", SorumluRenk="#8b5cf6", Durum="done", SiraNo=2, BaslangicTarihi=new DateTime(2026,4,14), BitisTarihi=new DateTime(2026,4,20) },
                            new SprintGorev { GorevAdi="Aylık abonelik planları",               Sorumlu="Selin Erdoğan", SorumluKisaltma="SE", SorumluRenk="#06b6d4", Durum="done", SiraNo=3, BaslangicTarihi=new DateTime(2026,4,10), BitisTarihi=new DateTime(2026,4,22) },
                            new SprintGorev { GorevAdi="Fatura & makbuz sistemi (PDF)",         Sorumlu="Yusuf Arslan",  SorumluKisaltma="YA", SorumluRenk="#f59e0b", Durum="done", SiraNo=4, BaslangicTarihi=new DateTime(2026,4,18), BitisTarihi=new DateTime(2026,4,28) },
                            new SprintGorev { GorevAdi="Vergi & KDV hesaplama",                 Sorumlu="Nisa Çelik",    SorumluKisaltma="NÇ", SorumluRenk="#f59e0b", Durum="done", SiraNo=5, BaslangicTarihi=new DateTime(2026,4,25), BitisTarihi=new DateTime(2026,4,30) },
                            new SprintGorev { GorevAdi="İndirim & kampanya modülü",             Sorumlu="Selin Erdoğan", SorumluKisaltma="SE", SorumluRenk="#06b6d4", Durum="done", SiraNo=6, BaslangicTarihi=new DateTime(2026,4,28), BitisTarihi=new DateTime(2026,5,5) },
                            new SprintGorev { GorevAdi="Banka havalesi / EFT entegrasyonu",     Sorumlu="Ahmet Yılmaz",  SorumluKisaltma="AY", SorumluRenk="#8b5cf6", Durum="done", SiraNo=7, BaslangicTarihi=new DateTime(2026,5,2), BitisTarihi=new DateTime(2026,5,8) },
                        }
                    },
                    new Sprint { SprintNo=5, Baslik="Mobil Uygulama & Bildirimler",          BaslangicTarihi=new DateTime(2026,5,12), BitisTarihi=new DateTime(2026,6,7),  Durum="done", HaftaSayisi=4,
                        Gorevler = new List<SprintGorev> {
                            new SprintGorev { GorevAdi="React Native kurulum & navigation",     Sorumlu="Fatma Demir",   SorumluKisaltma="FD", SorumluRenk="#f59e0b", Durum="done", SiraNo=1, BaslangicTarihi=new DateTime(2026,5,12), BitisTarihi=new DateTime(2026,5,16) },
                            new SprintGorev { GorevAdi="Kullanıcı giriş/kayıt ekranları",       Sorumlu="Fatma Demir",   SorumluKisaltma="FD", SorumluRenk="#f59e0b", Durum="done", SiraNo=2, BaslangicTarihi=new DateTime(2026,5,15), BitisTarihi=new DateTime(2026,5,22) },
                            new SprintGorev { GorevAdi="Push bildirim servisi (FCM)",           Sorumlu="Fatma Demir",   SorumluKisaltma="FD", SorumluRenk="#f59e0b", Durum="done", SiraNo=3, BaslangicTarihi=new DateTime(2026,5,20), BitisTarihi=new DateTime(2026,5,28) },
                            new SprintGorev { GorevAdi="Harita entegrasyonu (Google Maps API)", Sorumlu="Yusuf Arslan",  SorumluKisaltma="YA", SorumluRenk="#f59e0b", Durum="done", SiraNo=4, BaslangicTarihi=new DateTime(2026,5,18), BitisTarihi=new DateTime(2026,5,26) },
                            new SprintGorev { GorevAdi="QR kod ile giriş sistemi",              Sorumlu="Fatma Demir",   SorumluKisaltma="FD", SorumluRenk="#f59e0b", Durum="done", SiraNo=5, BaslangicTarihi=new DateTime(2026,5,26), BitisTarihi=new DateTime(2026,6,2) },
                            new SprintGorev { GorevAdi="Mobil ödeme akışı (Apple/Google Pay)",  Sorumlu="Ahmet Yılmaz",  SorumluKisaltma="AY", SorumluRenk="#8b5cf6", Durum="done", SiraNo=6, BaslangicTarihi=new DateTime(2026,5,28), BitisTarihi=new DateTime(2026,6,4) },
                            new SprintGorev { GorevAdi="Favori otopark kaydetme",               Sorumlu="Fatma Demir",   SorumluKisaltma="FD", SorumluRenk="#f59e0b", Durum="done", SiraNo=7, BaslangicTarihi=new DateTime(2026,6,1), BitisTarihi=new DateTime(2026,6,5) },
                            new SprintGorev { GorevAdi="iOS & Android build işlemleri",         Sorumlu="Mert Kaya",     SorumluKisaltma="MK", SorumluRenk="#10b981", Durum="done", SiraNo=8, BaslangicTarihi=new DateTime(2026,6,4), BitisTarihi=new DateTime(2026,6,7) },
                        }
                    },
                    // SPRINT 6: IN PROGRESS (Almost Finished)
                    new Sprint { SprintNo=6, Baslik="Test, Güvenlik & Canlıya Alım",         BaslangicTarihi=new DateTime(2026,6,10), BitisTarihi=new DateTime(2026,6,30), Durum="active", HaftaSayisi=3,
                        Gorevler = new List<SprintGorev> {
                            new SprintGorev { GorevAdi="End-to-end testler (Cypress)",          Sorumlu="Berk Toprak",   SorumluKisaltma="BT", SorumluRenk="#f59e0b", Durum="done", SiraNo=1, BaslangicTarihi=new DateTime(2026,6,10), BitisTarihi=new DateTime(2026,6,14) },
                            new SprintGorev { GorevAdi="Güvenlik penetrasyon testi",            Sorumlu="Mert Kaya",     SorumluKisaltma="MK", SorumluRenk="#10b981", Durum="done", SiraNo=2, BaslangicTarihi=new DateTime(2026,6,12), BitisTarihi=new DateTime(2026,6,16) },
                            new SprintGorev { GorevAdi="KVKK uyumluluk denetimi",              Sorumlu="Nisa Çelik",    SorumluKisaltma="NÇ", SorumluRenk="#f59e0b", Durum="done", SiraNo=3, BaslangicTarihi=new DateTime(2026,6,10), BitisTarihi=new DateTime(2026,6,13) },
                            new SprintGorev { GorevAdi="Yük ve performans testleri (k6)",       Sorumlu="Berk Toprak",   SorumluKisaltma="BT", SorumluRenk="#f59e0b", Durum="active", SiraNo=4, BaslangicTarihi=new DateTime(2026,6,15), BitisTarihi=null },
                            new SprintGorev { GorevAdi="SSL/TLS sertifika yapılandırması",      Sorumlu="Mert Kaya",     SorumluKisaltma="MK", SorumluRenk="#10b981", Durum="active", SiraNo=5, BaslangicTarihi=new DateTime(2026,6,16), BitisTarihi=null },
                            new SprintGorev { GorevAdi="Canlı ortam (AWS) production kurulumu", Sorumlu="Mert Kaya",     SorumluKisaltma="MK", SorumluRenk="#10b981", Durum="pending", SiraNo=6, BaslangicTarihi=null, BitisTarihi=null },
                            new SprintGorev { GorevAdi="Kullanıcı kabul testi (UAT)",           Sorumlu="Nisa Çelik",    SorumluKisaltma="NÇ", SorumluRenk="#f59e0b", Durum="pending", SiraNo=7, BaslangicTarihi=null, BitisTarihi=null },
                            new SprintGorev { GorevAdi="Bakım & SLA dokümantasyonu",            Sorumlu="Can Öztürk",    SorumluKisaltma="CÖ", SorumluRenk="#3b82f6", Durum="pending", SiraNo=8, BaslangicTarihi=null, BitisTarihi=null },
                        }
                    },
                };
                db.Sprintler.AddRange(sprintler);
            }

            // ── Haftalık Raporlar ─────────────────────────────────────────────────
            // Hafta 14 silindi çünkü proje bitmedi.
            if (!db.HaftalikRaporlar.Any())
            {
                db.HaftalikRaporlar.AddRange(
                    new HaftalikRapor { HaftaNo=1,  HaftaAdi="Hafta 1",  Tarihler="6–10 Ocak 2026",          Sprint="Sprint 1", TamamlananGorev=6,  DevamEdenGorev=2, Engelleyici=0, Verimlilik=94,  Notlar="Proje resmi olarak başlatıldı. Kick-off toplantısı yapıldı. İhtiyaç analizi belgesi oluşturuldu. Paydaşlarla ilk görüşmeler tamamlandı." },
                    new HaftalikRapor { HaftaNo=2,  HaftaAdi="Hafta 2",  Tarihler="13–17 Ocak 2026",         Sprint="Sprint 1", TamamlananGorev=8,  DevamEdenGorev=3, Engelleyici=0, Verimlilik=96,  Notlar="Veritabanı şema tasarımı tamamlandı. UI/UX wireframe çalışmaları başladı. Teknoloji stack kararları finalize edildi. Docker ortamı hazırlandı." },
                    new HaftalikRapor { HaftaNo=3,  HaftaAdi="Hafta 3",  Tarihler="20–24 Ocak 2026",         Sprint="Sprint 1", TamamlananGorev=7,  DevamEdenGorev=2, Engelleyici=1, Verimlilik=88,  Notlar="Figma prototip taslağı hazırlandı. Bir blocker: Plaka tanıma API seçimi gecikti, alternatif tedarikçi değerlendirme yapıldı. Çözüme kavuşturuldu." },
                    new HaftalikRapor { HaftaNo=4,  HaftaAdi="Hafta 4",  Tarihler="27–31 Ocak 2026",         Sprint="Sprint 1", TamamlananGorev=10, DevamEdenGorev=0, Engelleyici=0, Verimlilik=100, Notlar="Sprint 1 başarıyla tamamlandı. Tüm tasarım dokümanları onaylandı. Plaka API seçimi yapıldı. Sprint 2 planlaması tamamlandı." },
                    new HaftalikRapor { HaftaNo=5,  HaftaAdi="Hafta 5",  Tarihler="3–7 Şubat 2026",          Sprint="Sprint 2", TamamlananGorev=9,  DevamEdenGorev=3, Engelleyici=0, Verimlilik=91,  Notlar="PostgreSQL kurulumu ve Prisma ORM entegrasyonu tamamlandı. JWT token sistemi çalışır hale getirildi. CI/CD pipeline tasarımı başladı." },
                    new HaftalikRapor { HaftaNo=6,  HaftaAdi="Hafta 6",  Tarihler="10–14 Şubat 2026",        Sprint="Sprint 2", TamamlananGorev=8,  DevamEdenGorev=4, Engelleyici=0, Verimlilik=89,  Notlar="RESTful API temel endpointleri yazıldı. RBAC uygulandı. Docker Compose yapılandırması çalışır durumda. Unit test altyapısı kuruldu." },
                    new HaftalikRapor { HaftaNo=7,  HaftaAdi="Hafta 7",  Tarihler="17–28 Şubat 2026",        Sprint="Sprint 2", TamamlananGorev=11, DevamEdenGorev=0, Engelleyici=0, Verimlilik=100, Notlar="Sprint 2 tamamlandı. Redis entegrasyonu yapıldı. Kullanıcı yönetim modülü tamamlandı. GitHub Actions CI/CD devreye alındı. Swagger dokümantasyonu oluşturuldu." },
                    new HaftalikRapor { HaftaNo=8,  HaftaAdi="Hafta 8",  Tarihler="3–14 Mart 2026",          Sprint="Sprint 3", TamamlananGorev=14, DevamEdenGorev=2, Engelleyici=0, Verimlilik=92,  Notlar="Park alanı yönetim modülü tamamlandı. Araç giriş/çıkış endpointleri yazıldı. Admin dashboard teslim edildi. WebSocket bağlantısı test edildi." },
                    new HaftalikRapor { HaftaNo=9,  HaftaAdi="Hafta 9",  Tarihler="17–31 Mart 2026",         Sprint="Sprint 3", TamamlananGorev=12, DevamEdenGorev=0, Engelleyici=0, Verimlilik=100, Notlar="Sprint 3 tamamlandı. Gerçek zamanlı doluluk takibi aktif. Rezervasyon sistemi çekirdeği hazır. Harita görünümü finalize edildi." },
                    new HaftalikRapor { HaftaNo=10, HaftaAdi="Hafta 10", Tarihler="7–21 Nisan 2026",         Sprint="Sprint 4", TamamlananGorev=9,  DevamEdenGorev=3, Engelleyici=1, Verimlilik=84,  Notlar="İyzico sandbox entegrasyonu tamamlandı. Abonelik planları backend hazır. Blocker: İyzico production onayı beklendi. Fatura PDF modülü başladı. Çözüme kavuşturuldu." },
                    new HaftalikRapor { HaftaNo=11, HaftaAdi="Hafta 11", Tarihler="22 Nisan–9 Mayıs 2026",  Sprint="Sprint 4", TamamlananGorev=13, DevamEdenGorev=0, Engelleyici=0, Verimlilik=100, Notlar="Sprint 4 tamamlandı. İyzico production onayı alındı. Tüm ödeme modülleri, abonelik planları, fatura sistemi ve EFT entegrasyonu çalışıyor." },
                    new HaftalikRapor { HaftaNo=12, HaftaAdi="Hafta 12", Tarihler="12–31 Mayıs 2026",        Sprint="Sprint 5", TamamlananGorev=11, DevamEdenGorev=3, Engelleyici=0, Verimlilik=90,  Notlar="React Native uygulaması kuruldu. Giriş/kayıt ekranları, FCM push bildirimleri ve Google Maps entegrasyonu tamamlandı. QR kod sistemi başladı." },
                    new HaftalikRapor { HaftaNo=13, HaftaAdi="Hafta 13", Tarihler="1–7 Haziran 2026",        Sprint="Sprint 5", TamamlananGorev=8,  DevamEdenGorev=0, Engelleyici=0, Verimlilik=100, Notlar="Sprint 5 tamamlandı. QR kod girişi, Apple/Google Pay entegrasyonu, favori otopark özelliği ve iOS & Android build işlemleri tamamlandı." }
                );
            }

            // ── Bütçe Kalemleri ───────────────────────────────────────────────────
            if (!db.BütçeKalemleri.Any())
            {
                db.BütçeKalemleri.AddRange(
                    new BütçeKalemi { Kategori="Personel Maliyeti",        KategoriIkon="💼", Tutar=519000, Aciklama="9 ekip üyesi toplam maaş ödemeleri",   RenkKod="#3b82f6", SiraNo=1 },
                    new BütçeKalemi { Kategori="Altyapı & Lisans",         KategoriIkon="🖥️", Tutar=86000,  Aciklama="AWS, lisanslar, API'lar",              RenkKod="#8b5cf6", SiraNo=2 },
                    new BütçeKalemi { Kategori="Araç & Yazılım",           KategoriIkon="⚡",  Tutar=42000,  Aciklama="Jira, Figma, GitHub vb.",              RenkKod="#10b981", SiraNo=3 },
                    new BütçeKalemi { Kategori="Test & Güvenlik",          KategoriIkon="🧪", Tutar=28000,  Aciklama="Penetrasyon testi, sertifika",          RenkKod="#f59e0b", SiraNo=4 },
                    new BütçeKalemi { Kategori="Eğitim & Dokümantasyon",   KategoriIkon="📢", Tutar=14000,  Aciklama="Kullanıcı kılavuzları, eğitimler",      RenkKod="#06b6d4", SiraNo=5 },
                    new BütçeKalemi { Kategori="Bütçe Tasarrufu",          KategoriIkon="🎉", Tutar=11000,  Aciklama="Bütçe altında teslim",                 RenkKod="#10b981", SiraNo=6 }
                );
            }

            db.SaveChanges();
        }
    }
}
