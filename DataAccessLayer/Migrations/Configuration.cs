namespace DataAccessLayer.Migrations
{
    using EntityLayer.Concrete;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.Concrete.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccessLayer.Concrete.Context context)
        {
            // KULLANICILAR
            context.Users.AddOrUpdate(
                u => u.Username,
                new User
                {
                    Username = "mehmet.koc",
                    PasswordHash = "Mkc2024!",
                    FullName = "Mehmet Koç",
                    Email = "mehmet.koc@teyas.com.tr",
                    RoleId = (UserRole)3,  // Admin
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 5, 10, 9, 0, 0)
                },
                new User
                {
                    Username = "ayse.demir",
                    PasswordHash = "Adm2024!",
                    FullName = "Ayşe Demir",
                    Email = "ayse.demir@teyas.com.tr",
                    RoleId = (UserRole)2, // Yonetici
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 8, 15, 10, 30, 0)
                },
                new User
                {
                    Username = "orhan.kara",
                    PasswordHash = "Okr2024!",
                    FullName = "Orhan Kara",
                    Email = "orhan.kara@teyas.com.tr",
                    RoleId = (UserRole)1, // Kullanici
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 11, 18, 10, 15, 0)
                },
                new User
                {
                    Username = "fatma.celik",
                    PasswordHash = "Fck2024!",
                    FullName = "Fatma Çelik",
                    Email = "fatma.celik@teyas.com.tr",
                    RoleId = (UserRole)1, // Kullanici
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 12, 5, 11, 20, 0)
                }
            );

            context.SaveChanges();

            // TALEPLER
            context.Requests.AddOrUpdate(
                r => r.RequestNo,
                new Request
                {
                    RequestNo = "TYS-2025-00000001",
                    Title = "Personel bilgi sistemi şifre sıfırlama sorunu",
                    Description = "PERBİS uygulamasında şifre sıfırlama işlemi gerçekleştirilemiyor.",
                    RequestTypeId = (RequestType)1, // Yazılım Hatası
                    PriorityId = (Priority)3,       // Yüksek
                    StatusId = (RequestStatus)3,    // İşlem Tamamlandı
                    CreatedByUserId = 3,
                    ProcessedByUserId = 2,
                    CreatedDate = new DateTime(2025, 1, 6, 10, 15, 0),
                    ProcessedDate = new DateTime(2025, 1, 7, 14, 30, 0),
                    ApprovalNotes = "BT ekibine iletildi. Token süresi 24 saate çıkarıldı."
                },
                new Request
                {
                    RequestNo = "TYS-2025-00000002",
                    Title = "Cami görevlendirme modülü geliştirme",
                    Description = "İl müftülüklerinin cami görevlendirmelerini dijital ortamda yapabilmesi için yeni modül gerekiyor.",
                    RequestTypeId = (RequestType)2, // Özellik Talebi
                    PriorityId = (Priority)2,       // Orta
                    StatusId = (RequestStatus)2,    // Onay Bekliyor
                    CreatedByUserId = 2,
                    CreatedDate = new DateTime(2025, 1, 8, 9, 30, 0)
                },
                new Request
                {
                    RequestNo = "TYS-2025-00000003",
                    Title = "Mobil uygulama Android 15 uyumluluk hatası",
                    Description = "Diyanet mobil uygulaması Android 15'te açılmıyor.",
                    RequestTypeId = (RequestType)1,  // Yazılım Hatası
                    PriorityId = (Priority)3,        // Yüksek
                    StatusId = (RequestStatus)2,     // Onay Bekliyor
                    CreatedByUserId = 4,
                    CreatedDate = new DateTime(2025, 1, 10, 14, 45, 0)
                },
                new Request
                {
                    RequestNo = "TYS-2025-00000004",
                    Title = "VPN bağlantı hatası",
                    Description = "Taşra teşkilatından VPN ile merkeze bağlanırken hata alınıyor.",
                    RequestTypeId = (RequestType)3, // Destek Talebi
                    PriorityId = (Priority)3,       // Yüksek
                    StatusId = (RequestStatus)3,    // İşlem Tamamlandı
                    CreatedByUserId = 3,
                    ProcessedByUserId = 1,
                    CreatedDate = new DateTime(2025, 1, 13, 8, 50, 0),
                    ProcessedDate = new DateTime(2025, 1, 14, 10, 0, 0),
                    ApprovalNotes = "Sertifika yenilendi, çözüldü."
                },
                new Request
                {
                    RequestNo = "TYS-2025-00000005",
                    Title = "İzin takip sistemi yıllık izin hesaplama hatası",
                    Description = "Personel izin takip modülünde yıllık izin günleri yanlış hesaplanıyor.",
                    RequestTypeId = (RequestType)1, // Yazılım Hatası
                    PriorityId = (Priority)2,       // Orta
                    StatusId = (RequestStatus)1,    // Taslak
                    CreatedByUserId = 4,
                    CreatedDate = new DateTime(2025, 1, 16, 14, 30, 0)
                }
            );

            context.SaveChanges();
        }
    }
}
