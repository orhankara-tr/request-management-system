TALEP YÖNETİM SİSTEMİ
- ASP.NET MVC tabanlı talep yönetim sistemi projesi ile sistemde kayıtlı kullanıcılar yeni talep oluşturup düzenleyebilirler.
- Rol bazlı çalışan bu sistemde Personel, Yönetici ve Admin olmak üzere 3 rol tipi mevcuttur.
- Her kulllanıcı sisteme giriş yapar ve rol tipine göre sol menüde yapacakları işlemler görünür.
- Admin sadece kullanıcı ekleme, düzenleme, kullaınıcı rol değiştirme ve silme işlemlerini yapar.
- Kullanıcı sadece ilgili konu başlığı, açıklaması ve seçtiği durum önceliklerine göre yöneticilerine taleplerini onaya gönderir. 
- Yöneticiler kullancılardan gelen talepleri onaylayabilir.
- Red edebilirler ve red ederken açıklama girmeleri zorunludur.
- Yapılan tüm bu işlemler veri tabanında RequestHistory tablosunda log olarak tutulur ve yönetici panelinden görüntüleyebilir.

Kullanılan Teknolojiler
- Visual Studio 2022, .NET Framework 4.7.2 , ASP.NET MVC , MS SQL Server, ORM (Entity Framework 6) , Code First , FluentValidation, PagedList, bootstrap 5.2.3 , Razor Views , AdminLTE 3 Teması , SweetAlert

Veri Tabanı Yapısı 
- Db       : DbRequestManagementSystem
- Tablolar : Users, Requests, RequestHistories
  
Mimari Yapı (Katmanlar)
- EntityLayer         : User, Request, RequestHistory, (Enums)
- DataAccessLayer     : Context Sınıfı
- EF Repository/DAL   : EFUserDal, EFRequestDal, EFRequestHistoryDal
- BusinessLayer       : Abstract(IUserService, IRequestService, IRequestHistoryService ), Concrete(UserManager, RequestManager, RequestHistoryManager), ValidationRules(UserValidator, RequestValidator)

  Yetkilendirme ve Roller
- Kullanıcı (Kullanici = 1)  : Kendi taleplerini oluşturur ve görüntüler.
- Yönetici  (Yonetici = 2)   : Talepleri onaylar/reddeder. Kayıt geçmişi ekranına erişebilir.
- Admin     (Admin = 3)      : Kullanıcı yönetimi (ekle/düzenle/sil, aktif/pasif)

Talep (Request) Süreci
- Kullanıcı talep oluşturur. (Başlık, Açıklama, Tür, Öncelik)
- Talep belirli statülerle ilerler. bu verilerden yazım kurallarına yanlış olan bir veri varsa FluentValidation ile oluşturulan mesajlar ile kullanıcıya bilgiler verilir.
- Yönetici Onay veya Red verir. Red verirse açıklama yazması zorunlu yapıldı.
- Talep No olarak TYS-2026-00000014 gibi bir format oluşturuldu. Veri tabanındaki RequestId derğerini taleplerin okunabilirliğini artırmak için böyle bir format kullandım.

Kayıt Geçmişi 
- Talep üzerinde gerçekleşen önemli işlemler `RequestHistory` tablosunda tutuluyor.
- Yönetici, `/RequestHistory/Index` ekranından geçmiş kayıtlarını:
  - Tarih
  - TalepId
  - KullanıcıId
  - İşlem türü
  - Alan adı
  - Eski/Yeni değer
  - Not
  görebilir.

Kurulum 
- Web.config connection string ayarlanır.
	<connectionStrings>
		<add name="Context" connectionString="data source = DESKTOP-XXXX\SQLEXPRESS; initial catalog = DbRequestManagementSystem; integrated security = true" providerName="System.Data.SqlClient" />
	</connectionStrings>
- Package Manager Console: Update-Package -reinstall çalıştırılmalı
- Package Manager Console: DataAccessLayer Seçilmeli
   - İlk kurulum: `Enable-Migrations` → `Add-Migration InitialCreate` → `Update-Database`
  
- IIS Express ile çalıştır.

Hazırlayan
Orhan Kara
orhankara.tr@gmail.com
 
