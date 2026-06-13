using Microsoft.EntityFrameworkCore;
using ProjeYonetimAPI.Data;
using ProjeYonetimAPI.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ── Veritabanı ─────────────────────────────────────────────────────────────
builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<ProjeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── CORS — file:// protokolü + localhost için ───────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true)   // file:// dahil her origin
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// ── Veritabanı Migrate + Seed ───────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjeDbContext>();
    db.Database.Migrate();
    SeedData.Initialize(db);
}

app.UseCors("AllowAll");

// ════════════════════════════════════════════════════════════════════════════
// API ENDPOINTS
// ════════════════════════════════════════════════════════════════════════════

// ── Ekip Üyeleri ────────────────────────────────────────────────────────────
app.MapGet("/api/ekip", async (ProjeDbContext db) =>
    Results.Ok(await db.EkipUyeleri.OrderBy(e => e.Id).ToListAsync()));

app.MapGet("/api/ekip/{id}", async (int id, ProjeDbContext db) =>
    await db.EkipUyeleri.FindAsync(id) is EkipUyesi u ? Results.Ok(u) : Results.NotFound());

app.MapPost("/api/ekip", async (EkipUyesi uye, ProjeDbContext db) =>
{
    db.EkipUyeleri.Add(uye);
    await db.SaveChangesAsync();
    return Results.Created($"/api/ekip/{uye.Id}", uye);
});

app.MapPut("/api/ekip/{id}", async (int id, EkipUyesi input, ProjeDbContext db) =>
{
    var uye = await db.EkipUyeleri.FindAsync(id);
    if (uye is null) return Results.NotFound();
    uye.AdSoyad = input.AdSoyad; uye.Kisaltma = input.Kisaltma;
    uye.Rol = input.Rol; uye.Uzmanliklar = input.Uzmanliklar;
    uye.AylikUcret = input.AylikUcret; uye.ProjedekiAy = input.ProjedekiAy;
    uye.RenkKod = input.RenkKod; uye.RenkKod2 = input.RenkKod2;
    await db.SaveChangesAsync();
    return Results.Ok(uye);
});

app.MapDelete("/api/ekip/{id}", async (int id, ProjeDbContext db) =>
{
    var uye = await db.EkipUyeleri.FindAsync(id);
    if (uye is null) return Results.NotFound();
    db.EkipUyeleri.Remove(uye);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// ── Sprintler ───────────────────────────────────────────────────────────────
app.MapGet("/api/sprintler", async (ProjeDbContext db) =>
    Results.Ok(await db.Sprintler
        .Include(s => s.Gorevler.OrderBy(g => g.SiraNo))
        .OrderBy(s => s.SprintNo)
        .ToListAsync()));

app.MapGet("/api/sprintler/{id}", async (int id, ProjeDbContext db) =>
    await db.Sprintler.Include(s => s.Gorevler).FirstOrDefaultAsync(s => s.Id == id) is Sprint s
        ? Results.Ok(s) : Results.NotFound());

app.MapPost("/api/sprintler", async (Sprint sprint, ProjeDbContext db) =>
{
    db.Sprintler.Add(sprint);
    await db.SaveChangesAsync();
    return Results.Created($"/api/sprintler/{sprint.Id}", sprint);
});

app.MapPut("/api/sprintler/{id}", async (int id, Sprint input, ProjeDbContext db) =>
{
    var sprint = await db.Sprintler.FindAsync(id);
    if (sprint is null) return Results.NotFound();
    sprint.Baslik = input.Baslik; sprint.Aciklama = input.Aciklama;
    sprint.BaslangicTarihi = input.BaslangicTarihi; sprint.BitisTarihi = input.BitisTarihi;
    sprint.Durum = input.Durum; sprint.HaftaSayisi = input.HaftaSayisi;
    await db.SaveChangesAsync();
    return Results.Ok(sprint);
});

// ── Sprint Görevleri ────────────────────────────────────────────────────────
app.MapGet("/api/sprintgorevler", async (ProjeDbContext db) =>
    Results.Ok(await db.SprintGorevler.OrderBy(g => g.SprintId).ThenBy(g => g.SiraNo).ToListAsync()));

app.MapPost("/api/sprintgorevler", async (SprintGorev gorev, ProjeDbContext db) =>
{
    db.SprintGorevler.Add(gorev);
    await db.SaveChangesAsync();
    return Results.Created($"/api/sprintgorevler/{gorev.Id}", gorev);
});

app.MapPut("/api/sprintgorevler/{id}", async (int id, SprintGorev input, ProjeDbContext db) =>
{
    var gorev = await db.SprintGorevler.FindAsync(id);
    if (gorev is null) return Results.NotFound();
    gorev.GorevAdi = input.GorevAdi; gorev.Sorumlu = input.Sorumlu;
    gorev.SorumluKisaltma = input.SorumluKisaltma; gorev.SorumluRenk = input.SorumluRenk;
    gorev.Durum = input.Durum; gorev.SiraNo = input.SiraNo;
    await db.SaveChangesAsync();
    return Results.Ok(gorev);
});

app.MapDelete("/api/sprintgorevler/{id}", async (int id, ProjeDbContext db) =>
{
    var gorev = await db.SprintGorevler.FindAsync(id);
    if (gorev is null) return Results.NotFound();
    db.SprintGorevler.Remove(gorev);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// ── Haftalık Raporlar ───────────────────────────────────────────────────────
app.MapGet("/api/raporlar", async (ProjeDbContext db) =>
    Results.Ok(await db.HaftalikRaporlar.OrderBy(r => r.HaftaNo).ToListAsync()));

app.MapGet("/api/raporlar/{id}", async (int id, ProjeDbContext db) =>
    await db.HaftalikRaporlar.FindAsync(id) is HaftalikRapor r ? Results.Ok(r) : Results.NotFound());

app.MapPost("/api/raporlar", async (HaftalikRapor rapor, ProjeDbContext db) =>
{
    db.HaftalikRaporlar.Add(rapor);
    await db.SaveChangesAsync();
    return Results.Created($"/api/raporlar/{rapor.Id}", rapor);
});

app.MapPut("/api/raporlar/{id}", async (int id, HaftalikRapor input, ProjeDbContext db) =>
{
    var rapor = await db.HaftalikRaporlar.FindAsync(id);
    if (rapor is null) return Results.NotFound();
    rapor.HaftaAdi = input.HaftaAdi; rapor.Tarihler = input.Tarihler;
    rapor.Sprint = input.Sprint; rapor.TamamlananGorev = input.TamamlananGorev;
    rapor.DevamEdenGorev = input.DevamEdenGorev; rapor.Engelleyici = input.Engelleyici;
    rapor.Verimlilik = input.Verimlilik; rapor.Notlar = input.Notlar;
    await db.SaveChangesAsync();
    return Results.Ok(rapor);
});

app.MapDelete("/api/raporlar/{id}", async (int id, ProjeDbContext db) =>
{
    var rapor = await db.HaftalikRaporlar.FindAsync(id);
    if (rapor is null) return Results.NotFound();
    db.HaftalikRaporlar.Remove(rapor);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// ── Bütçe Kalemleri ─────────────────────────────────────────────────────────
app.MapGet("/api/butce", async (ProjeDbContext db) =>
    Results.Ok(await db.BütçeKalemleri.OrderBy(b => b.SiraNo).ToListAsync()));

app.MapPost("/api/butce", async (BütçeKalemi kalem, ProjeDbContext db) =>
{
    db.BütçeKalemleri.Add(kalem);
    await db.SaveChangesAsync();
    return Results.Created($"/api/butce/{kalem.Id}", kalem);
});

app.MapPut("/api/butce/{id}", async (int id, BütçeKalemi input, ProjeDbContext db) =>
{
    var kalem = await db.BütçeKalemleri.FindAsync(id);
    if (kalem is null) return Results.NotFound();
    kalem.Kategori = input.Kategori; kalem.Tutar = input.Tutar;
    kalem.Aciklama = input.Aciklama; kalem.RenkKod = input.RenkKod;
    await db.SaveChangesAsync();
    return Results.Ok(kalem);
});

app.MapDelete("/api/butce/{id}", async (int id, ProjeDbContext db) =>
{
    var kalem = await db.BütçeKalemleri.FindAsync(id);
    if (kalem is null) return Results.NotFound();
    db.BütçeKalemleri.Remove(kalem);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// ── Proje Görevleri (Bakım/Güncelleme) ─────────────────────────────────────
app.MapGet("/api/gorevler", async (ProjeDbContext db) =>
    Results.Ok(await db.ProjeGorevler.OrderByDescending(g => g.OlusturmaTarihi).ToListAsync()));

app.MapGet("/api/gorevler/{id}", async (int id, ProjeDbContext db) =>
    await db.ProjeGorevler.FindAsync(id) is ProjeGorev g ? Results.Ok(g) : Results.NotFound());

app.MapPost("/api/gorevler", async (ProjeGorev gorev, ProjeDbContext db) =>
{
    gorev.OlusturmaTarihi = DateTime.Now;
    db.ProjeGorevler.Add(gorev);
    await db.SaveChangesAsync();
    return Results.Created($"/api/gorevler/{gorev.Id}", gorev);
});

app.MapPut("/api/gorevler/{id}", async (int id, ProjeGorev input, ProjeDbContext db) =>
{
    var gorev = await db.ProjeGorevler.FindAsync(id);
    if (gorev is null) return Results.NotFound();
    gorev.Baslik = input.Baslik; gorev.Aciklama = input.Aciklama;
    gorev.GorevTuru = input.GorevTuru; gorev.AtananKisi = input.AtananKisi;
    gorev.AtananKisaltma = input.AtananKisaltma; gorev.AtananRenk = input.AtananRenk;
    gorev.Oncelik = input.Oncelik; gorev.Durum = input.Durum;
    gorev.Modul = input.Modul;
    gorev.BaslangicTarihi = input.BaslangicTarihi; gorev.BitisTarihi = input.BitisTarihi;
    gorev.GuncellenmeTarihi = DateTime.Now;
    await db.SaveChangesAsync();
    return Results.Ok(gorev);
});

// Sadece durum güncelleme (PATCH benzeri)
app.MapPut("/api/gorevler/{id}/durum", async (int id, DurumGuncelle input, ProjeDbContext db) =>
{
    var gorev = await db.ProjeGorevler.FindAsync(id);
    if (gorev is null) return Results.NotFound();
    gorev.Durum = input.Durum;
    gorev.GuncellenmeTarihi = DateTime.Now;
    await db.SaveChangesAsync();
    return Results.Ok(gorev);
});

app.MapDelete("/api/gorevler/{id}", async (int id, ProjeDbContext db) =>
{
    var gorev = await db.ProjeGorevler.FindAsync(id);
    if (gorev is null) return Results.NotFound();
    db.ProjeGorevler.Remove(gorev);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// ── Özet / Dashboard ────────────────────────────────────────────────────────
app.MapGet("/api/ozet", async (ProjeDbContext db) =>
{
    var sprintSayisi   = await db.Sprintler.CountAsync();
    var gorevSayisi    = await db.SprintGorevler.CountAsync();
    var ekipSayisi     = await db.EkipUyeleri.CountAsync();
    var toplamButce    = await db.BütçeKalemleri.Where(b => b.Kategori != "Bütçe Tasarrufu").SumAsync(b => b.Tutar);
    var tasarruf       = await db.BütçeKalemleri.Where(b => b.Kategori == "Bütçe Tasarrufu").SumAsync(b => b.Tutar);
    var personelMaliyeti = await db.BütçeKalemleri.Where(b => b.Kategori == "Personel Maliyeti").SumAsync(b => b.Tutar);
    var aktifGorev     = await db.ProjeGorevler.CountAsync(g => g.Durum != "done");
    var tamamGorev     = await db.ProjeGorevler.CountAsync(g => g.Durum == "done");

    return Results.Ok(new
    {
        SprintSayisi = sprintSayisi,
        SprintGorevSayisi = gorevSayisi,
        EkipSayisi = ekipSayisi,
        ToplamButce = toplamButce + tasarruf,
        HarcananButce = toplamButce,
        Tasarruf = tasarruf,
        PersonelMaliyeti = personelMaliyeti,
        AktifBakimGorev = aktifGorev,
        TamamBakimGorev = tamamGorev
    });
});

app.Run();

record DurumGuncelle(string Durum);
