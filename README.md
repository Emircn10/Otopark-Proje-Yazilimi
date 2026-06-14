# 🅿️ OtoparkApp — Otopark Yönetim Sistemi

<p align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-Razor_Pages-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-Express-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-9.0-68217A?style=for-the-badge" />
  <img src="https://img.shields.io/badge/lisans-MIT-green?style=for-the-badge" />
</p>

> **OtoparkApp**, otopark işletmelerinin araç giriş-çıkış takibini, abonelik yönetimini, tarife konfigürasyonunu ve personel yönetimini kolayca yapabilmesi için geliştirilmiş web tabanlı bir yönetim sistemidir.

---

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Ekran Görüntüleri](#-ekran-görüntüleri)
- [Teknoloji Stack](#-teknoloji-stack)
- [Proje Yapısı](#-proje-yapısı)
- [Veri Modeli](#-veri-modeli)
- [Kurulum](#-kurulum)
- [Kullanım](#-kullanım)
- [Kullanıcı Rolleri](#-kullanıcı-rolleri)
- [Servis Katmanı](#-servis-katmanı)
- [Katkıda Bulunma](#-katkıda-bulunma)

---

## ✨ Özellikler

### 🚗 Araç Yönetimi
- Araç plaka, tipi ve sahip bilgilerinin kaydedilmesi
- Araç geçmiş kayıtlarının görüntülenmesi
- Araç bazlı abonelik ve giriş istatistikleri

### 🔐 Araç Giriş / Çıkış Takibi
- Plakayla anlık araç girişi kaydı (`Entry`)
- Araç çıkışında otomatik süre ve ücret hesabı (`Exit`)
- Aboneliği olan araçlar için ücretsiz / indirimli geçiş
- Aboneliği olmayan ziyaretçi araçlar için walk-in girişi (`WalkIn`)
- Detaylı giriş/çıkış geçmişi (`History`)

### 📋 Abonelik Yönetimi
- Aylık / yıllık veya özel tarih aralıklı abonelik oluşturma
- Abonelik aktiflik durumu takibi
- Aboneliğin araçla ilişkilendirilmesi

### 💰 Dinamik Tarife Sistemi
- Araç tipine göre (otomobil, motosiklet vb.) farklı tarifeler
- Kademeli saatlik ücret yapısı:
  - İlk 1 saat: sabit ücret
  - 1–3 saat arası: saatlik ücret
  - 3 saatten sonrası: farklı saatlik ücret
  - Günlük maksimum ücret tavanı
- Geçerlilik başlangıç/bitiş tarihleri ile sezonluk tarife desteği

### 👥 Kullanıcı Yönetimi & Kimlik Doğrulama
- ASP.NET Core Identity ile güvenli giriş/çıkış
- Rol tabanlı erişim kontrolü (`Admin` ve `Calisan`)
- Uygulama başlangıcında otomatik seed kullanıcı oluşturma

---

## 🛠️ Teknoloji Stack

| Katman | Teknoloji |
|---|---|
| Web Framework | ASP.NET Core 9.0 — Razor Pages |
| ORM | Entity Framework Core 9.0 |
| Veritabanı | Microsoft SQL Server (Express) |
| Kimlik Doğrulama | ASP.NET Core Identity |
| Mimari | Repository / Service Pattern |
| Dil | C# 13 / .NET 9 |

---

## 📁 Proje Yapısı

```
OtoparkSistemi/
├── Data/                        # Veritabanı bağlamı ve seed verileri
│   └── AppDbContext.cs
├── Migrations/                  # EF Core migration dosyaları
├── Models/                      # Veri modelleri (entity'ler)
│   ├── Arac.cs                  # Araç modeli
│   ├── Abonelik.cs              # Abonelik modeli
│   ├── GirisKayit.cs            # Giriş/çıkış kaydı modeli
│   ├── Tarife.cs                # Ücret tarife modeli
│   └── ApplicationUser.cs       # Identity kullanıcı modeli
├── Pages/                       # Razor Pages (UI)
│   ├── Account/                 # Giriş, kayıt, erişim reddedildi sayfaları
│   ├── Parking/                 # Araç giriş, çıkış, geçmiş sayfaları
│   │   ├── Entry.cshtml         # Araç giriş formu
│   │   ├── Exit.cshtml          # Araç çıkış & ücret hesaplama
│   │   ├── WalkIn.cshtml        # Aboneliksiz ziyaretçi girişi
│   │   ├── History.cshtml       # Geçmiş kayıtlar
│   │   └── Index.cshtml         # Otopark paneli
│   ├── Subscriptions/           # Abonelik yönetimi sayfaları
│   ├── Vehicles/                # Araç yönetimi sayfaları
│   ├── Pricing/                 # Tarife yönetimi sayfaları
│   └── Shared/                  # Ortak layout ve bileşenler
├── Services/                    # İş mantığı katmanı
│   ├── Interfaces/              # Servis arayüzleri
│   └── Implementations/         # Servis implementasyonları
├── ViewModels/                  # Sayfa bazlı görünüm modelleri
├── wwwroot/                     # Statik dosyalar (CSS, JS, görseller)
├── Program.cs                   # Uygulama başlangıç noktası
├── appsettings.json             # Uygulama yapılandırması
└── OtoparkSistemi.csproj        # Proje dosyası
```

---

## 🗃️ Veri Modeli

```
┌─────────────────┐       ┌───────────────────┐
│     Arac        │──────▶│    GirisKayit      │
│─────────────────│  1:N  │───────────────────│
│ AracId (PK)     │       │ KayitId (PK)       │
│ Plaka           │       │ AracId (FK)        │
│ AracTipi        │       │ GirisTarihi        │
│ SahibiAdi       │       │ CikisTarihi        │
│ SahibiTelefon   │       │ ToplamSure         │
│ OlusturmaTarihi │       │ OdenenUcret        │
└─────────────────┘       │ AbonelikKullanildi │
         │                │ ZiyaretciMi        │
         │ 1:N            │ OdemeDurumu        │
         ▼                └───────────────────┘
┌─────────────────┐
│    Abonelik     │       ┌───────────────────┐
│─────────────────│       │     Tarife        │
│ AbonelikId (PK) │       │───────────────────│
│ AracId (FK)     │       │ TarifeId (PK)     │
│ BaslangicTarihi │       │ AracTipi          │
│ BitisTarihi     │       │ Ilk1SaatUcret     │
│ AbonelikTipi    │       │ Saat1_3Ucret      │
│ Ucret           │       │ Saat3PlusSaatlik  │
│ AktifMi         │       │ GunlukMaksimum    │
└─────────────────┘       │ GecerlilikBaslang │
                          │ GecerlilikBitis   │
                          └───────────────────┘
```

---

## 🚀 Kurulum

### Ön Gereksinimler

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server Express](https://www.microsoft.com/tr-tr/sql-server/sql-server-downloads) (veya tam sürüm)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) veya [VS Code](https://code.visualstudio.com/)

### 1. Repoyu Klonlayın

```bash
git clone https://github.com/Emircn10/OtoparkApp.git
cd OtoparkApp/OtoparkSistemi
```

### 2. Veritabanı Bağlantısını Yapılandırın

`appsettings.json` dosyasındaki bağlantı dizesini kendi SQL Server örneğinize göre güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=Otopark;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

> **Not:** SQL Server Express yerine farklı bir örnek kullanıyorsanız `Server` değerini değiştirin (örn. `Server=localhost` veya `Server=.\SQLSERVER2022`).

### 3. Veritabanı Migration'larını Uygulayın

```bash
dotnet ef database update
```

> Migration'lar zaten mevcut olduğundan bu komut veritabanını otomatik oluşturacaktır. Uygulama ilk çalıştırıldığında da migration'lar ve seed verileri otomatik uygulanır.

### 4. Uygulamayı Çalıştırın

```bash
dotnet run
```

Tarayıcınızda `https://localhost:5001` adresini açın.

---

## 🖥️ Kullanım

### İlk Girişte Otomatik Oluşturulan Kullanıcılar

Uygulama ilk kez çalıştırıldığında aşağıdaki kullanıcılar otomatik oluşturulur:

| Kullanıcı Adı | Şifre | Rol |
|---|---|---|
| `admin` | `Admin123!` | Admin |
| `calisan` | `Calisan123!` | Calisan |

> ⚠️ **Güvenlik Uyarısı:** Üretim ortamında bu şifreleri mutlaka değiştirin.

### Temel İş Akışı

```
1. Araç Kaydı Oluştur (Vehicles → Yeni Araç)
      ↓
2. (Opsiyonel) Abonelik Tanımla (Subscriptions → Yeni Abonelik)
      ↓
3. Araç Girişi Kaydet (Parking → Entry veya WalkIn)
      ↓
4. Araç Çıkışı İşle + Ücret Hesapla (Parking → Exit)
      ↓
5. Geçmişi Görüntüle (Parking → History)
```

---

## 👤 Kullanıcı Rolleri

### 🔑 Admin
- Tüm modüllere tam erişim
- Tarife oluşturma / düzenleme / silme
- Araç, abonelik, giriş kayıtlarını yönetme
- Kullanıcı yönetimi

### 👷 Çalışan (Calisan)
- Araç giriş/çıkış işlemleri
- Geçmiş kayıt görüntüleme
- Araç sorgulama

---

## ⚙️ Servis Katmanı

Uygulama, iş mantığını UI katmanından ayırmak için **Interface / Implementation** servis deseni kullanır:

| Servis | Açıklama |
|---|---|
| `IAracService` / `AracService` | Araç CRUD işlemleri |
| `IAbonelikService` / `AbonelikService` | Abonelik yönetimi ve aktiflik kontrolü |
| `IGirisKayitService` / `GirisKayitService` | Giriş/çıkış kaydı ve ücret hesaplama |
| `ITarifeService` / `TarifeService` | Tarife sorgulama ve aktif tarife belirleme |

---

## 💡 Tarife Hesaplama Mantığı

Ücret hesaplaması `GirisKayitService` tarafından aşağıdaki kurallara göre yapılır:

```
Süre ≤ 1 saat      → İlk 1 Saat sabit ücreti
1 < Süre ≤ 3 saat  → İlk 1 Saat + (kalan süre × Saat1_3 ücreti)
Süre > 3 saat      → Yukarıdaki + (3 saatten sonraki süre × Saat3Plus ücreti)
Günlük tavan       → Hesaplanan ücret > GunlukMaksimum ise → GunlukMaksimum uygulanır
Abonelik sahibi    → Ücret = 0 (geçerli abonelik varsa)
```

---

## 🤝 Katkıda Bulunma

1. Bu repoyu fork'layın
2. Yeni bir feature branch oluşturun: `git checkout -b feature/yeni-ozellik`
3. Değişikliklerinizi commit edin: `git commit -m "feat: yeni özellik eklendi"`
4. Branch'inizi push edin: `git push origin feature/yeni-ozellik`
5. Pull Request açın

---

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

---

<p align="center">
  <b>Emircn10</b> tarafından geliştirilmiştir &nbsp;•&nbsp; 
  <a href="https://github.com/Emircn10/OtoparkApp">GitHub'da Görüntüle</a>
</p>
