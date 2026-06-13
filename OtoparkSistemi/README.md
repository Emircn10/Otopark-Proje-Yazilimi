# Otopark Giriş-Çıkış ve Abonelik Sistemi

Üniversite **Veri Tabanı Temelleri** ders projesi.  
ASP.NET Core 9 Razor Pages · EF Core 9 · SQL Server LocalDB · Bootstrap 5

---

## Kurulum Adımları

### Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9)
- SQL Server LocalDB (Visual Studio ile gelir)
- `dotnet-ef` tool

### 1. EF Core CLI Kurulumu
```bash
dotnet tool install --global dotnet-ef
```

### 2. Bağlantı Dizesi
`appsettings.json` içinde zaten hazır:
```
Server=(localdb)\MSSQLLocalDB;Database=OtoparkSistemi;Trusted_Connection=True;...
```

### 3. Migration Oluştur
```bash
cd OtoparkSistemi
dotnet ef migrations add InitialCreate
```

### 4. Veritabanını Güncelle
```bash
dotnet ef database update
```
> **Not:** Uygulama ilk çalıştığında `db.Database.Migrate()` çağrısı ile migration otomatik uygulanır.

### 5. Uygulamayı Başlat
```bash
dotnet run
```
Tarayıcıda: https://localhost:5001

---

## Seed Data (Örnek Veriler)
Uygulama ilk başlatıldığında otomatik eklenir:
- **10 Araç** – Farklı tipler (Otomobil, Motosiklet, Kamyonet)
- **5 Abonelik** – 1 pasif, 4 aktif
- **20 Park Kaydı** – 18 tamamlanmış + 2 aktif
- **3 Tarife** – Her araç tipi için

---

## Proje Yapısı
```
OtoparkSistemi/
├── Data/           AppDbContext, SeedData
├── Models/         Arac, Abonelik, GirisKayit, Tarife
├── ViewModels/     Dashboard, Parking ViewModels
├── Services/
│   ├── Interfaces/     4 arayüz
│   └── Implementations/ 4 servis
├── Pages/
│   ├── Shared/     _Layout, _Sidebar
│   ├── Vehicles/   CRUD (5 sayfa)
│   ├── Parking/    Entry, Exit, History
│   ├── Subscriptions/ CRUD
│   └── Pricing/    CRUD
└── wwwroot/        CSS (lacivert tema), JS
```

---

## Kimlik Doğrulama
Bu projede Identity kullanılmamıştır (opsiyonel istendi, DB odaklı proje).

## Teknik Notlar
- Araç girişinde aktif abonelik otomatik kontrol edilir
- Aynı anda iki açık kayıt oluşturulamaz  
- Abonelik bitiş kontrolü her dashboard açılışında çalışır
- Ücret = min(SaatlikUcret × saat, GünlükMaksimum)
