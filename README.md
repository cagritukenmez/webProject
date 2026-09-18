# Berber Salonu Randevu Sistemi

ASP.NET Core MVC ile geliştirilmiş, birden fazla berber salonunu yönetmeye ve müşterilerin online randevu almasına olanak tanıyan bir web uygulaması.

Kullanıcılar kayıt olup salon, personel ve hizmet seçerek randevu alabilir; yöneticiler ise salonları, personeli, hizmetleri, çalışma günlerini ve randevu onaylarını tek bir panelden yönetir. Uygulamada ayrıca yüklenen fotoğraf üzerinde yapay zekâ ile saç modeli denemesi yapılabilen bir bölüm bulunur.

## Özellikler

**Müşteri (Member)**
- Kayıt olma, giriş / çıkış yapma
- Salonlar arasında gezinme; salonun hizmetleri, çalışma saatleri ve iletişim bilgilerini görme
- Salon → personel → hizmet → tarih/saat seçerek randevu alma (seçimler AJAX ile dinamik olarak yüklenir)
- Kendi randevularını ve onay durumlarını görüntüleme
- Fotoğraf yükleyerek yapay zekâ destekli saç modeli önizlemesi alma

**Yönetici (Admin)**
- Berber salonu ekleme (ad, adres, telefon, e-posta, açılış/kapanış saati)
- Personel ekleme, düzenleme ve silme
- Personele bağlı hizmet (ad, süre, fiyat) ekleme, düzenleme ve silme
- Belirli bir tarihe kadar randevuya açık günleri oluşturma, günleri açıp kapatma
- Tüm randevuları görüntüleme ve onaylama
- Salon bazında personel/gün kırılımlı gelir tablosu, günlük gelir ve genel toplam

Randevu alınırken şu kontroller yapılır: seçilen gün salon için açık mı, aynı saatte başka randevu var mı, tarih geçmişte mi.

## Kullanılan Teknolojiler

- .NET 8, ASP.NET Core MVC (Razor Views)
- Entity Framework Core 9 (Code First, migrations)
- Microsoft SQL Server / LocalDB
- Bootstrap, jQuery, jQuery Validation
- [Hairstyle Changer Pro](https://rapidapi.com/) (RapidAPI) — saç modeli değiştirme servisi
- Newtonsoft.Json

## Proje Yapısı

```
myProject/
├── myProject.sln
└── myProject/
    ├── Controllers/
    │   ├── BerberSalonuController.cs   # Ana sayfa, admin paneli, salon ekleme, gelir raporu
    │   ├── KullanıcıController.cs      # Kayıt, giriş, çıkış
    │   ├── PersonelController.cs       # Personel CRUD
    │   ├── HizmetController.cs         # Hizmet CRUD
    │   ├── RandevuController.cs        # Randevu alma/listeleme/onaylama + AJAX uç noktaları
    │   ├── UygunTarihController.cs     # Çalışma günlerini açma/kapama
    │   └── PhotoController.cs          # Yapay zekâ ile saç modeli
    ├── Models/                         # Entity'ler, view model'ler ve myyDbContext
    ├── Migrations/                     # EF Core migration dosyaları
    ├── Views/                          # Razor görünümleri
    ├── wwwroot/                        # Statik dosyalar (css, js, lib)
    ├── Program.cs
    └── appsettings.json
```

### Veri Modeli

| Tablo | Açıklama |
|---|---|
| `BerberSalonları` | Salon bilgileri ve çalışma saatleri |
| `Personeller` | Bir salona bağlı çalışanlar |
| `Hizmetler` | Bir personele ve salona bağlı hizmetler (süre, fiyat) |
| `UygunTarih` | Salonun randevuya açık/kapalı günleri |
| `Randevular` | Kullanıcı, salon, personel ve hizmeti ilişkilendiren randevular (`onaylimi` alanı ile) |
| `Kullanıcı` | Kullanıcı hesapları; `rol` alanı `Admin` veya `Member` |

## Kurulum

### Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server veya SQL Server LocalDB (Visual Studio ile birlikte gelir)
- (İsteğe bağlı) Visual Studio 2022

### Adımlar

1. Depoyu klonlayın:

   ```bash
   git clone <repo-url>
   cd webProject/myProject/myProject
   ```

2. Veritabanı bağlantısını ayarlayın.

   > **Not:** `Models/myyDbContext.cs` içindeki `OnConfiguring` metodu, `appsettings.json`'daki `DefaultConnection` değerini ezer ve uygulama varsayılan olarak aşağıdaki LocalDB veritabanını kullanır:
   >
   > ```
   > Server=(localdb)\mssqllocaldb;Database=BerberSalonuDB;Trusted_Connection=True;
   > ```
   >
   > Farklı bir SQL Server kullanmak isterseniz bu satırı değiştirin (ya da `OnConfiguring`'i kaldırıp `appsettings.json` içindeki `DefaultConnection` değerini düzenleyin).

3. EF Core aracını kurup veritabanını oluşturun:

   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```

   Visual Studio kullanıyorsanız Package Manager Console'dan `Update-Database` komutunu da çalıştırabilirsiniz.

4. Uygulamayı başlatın:

   ```bash
   dotnet run
   ```

   Uygulama varsayılan olarak `http://localhost:5081` (https profiliyle `https://localhost:7008`) adresinde açılır.

### Yönetici Hesabı Oluşturma

Kayıt formundan oluşturulan tüm hesaplar `Member` rolüyle açılır. Yönetici hesabı için önce normal şekilde kayıt olun, ardından veritabanında ilgili kullanıcının rolünü güncelleyin:

```sql
UPDATE Kullanıcı SET rol = 'Admin' WHERE kullaniciAdi = 'kullanici_adiniz';
```

Admin olarak giriş yaptığınızda doğrudan Admin Paneli'ne yönlendirilirsiniz. İlk salonu ekledikten sonra sırasıyla personel, hizmet ve uygun tarihleri tanımlayarak salonu randevuya açabilirsiniz.

### Yapay Zekâ (Saç Modeli) Özelliği

Bu özellik RapidAPI üzerindeki Hairstyle Changer Pro servisini kullanır. Kendi API anahtarınızı `Controllers/PhotoController.cs` içindeki `API_KEY` sabitine girmeniz gerekir. Anahtarı kaynak koda yazmak yerine [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) veya ortam değişkeni kullanmanız önerilir.

## Kullanım Akışı

1. **Admin:** Salon ekle → Personel ekle → Personele hizmet ekle → Uygun tarihleri aç
2. **Müşteri:** Kayıt ol → Giriş yap → Randevu Al → Salon, personel, hizmet, tarih ve saati seç
3. **Admin:** Randevuları görüntüle → Onayla → Gelir tablosundan salon kazancını takip et

## Bilinen Kısıtlamalar

Bu proje bir ders/ödev projesi olarak geliştirilmiştir ve üretim ortamı için hazır değildir:

- Oturum yönetimi, kullanıcı ID'sini tutan basit bir cookie ile yapılır; ASP.NET Core Identity veya cookie authentication kullanılmaz.
- Şifreler veritabanında düz metin olarak saklanır.
- POST işlemlerinde anti-forgery (CSRF) doğrulaması ve bazı yönetici işlemlerinde yetki kontrolü bulunmaz.
