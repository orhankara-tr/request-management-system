namespace DataAccessLayer.Migrations
{
    using EntityLayer.Concrete;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.concrete.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccessLayer.concrete.Context context)
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
                    RoleId = 3,
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 5, 10, 9, 0, 0)
                },
                new User
                {
                    Username = "ayse.demir",
                    PasswordHash = "Adm2024!",
                    FullName = "Ayşe Demir",
                    Email = "ayse.demir@teyas.com.tr",
                    RoleId = 2,
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 8, 15, 10, 30, 0)
                },
                new User
                {
                    Username = "orhan.kara",
                    PasswordHash = "Okr2024!",
                    FullName = "Orhan Kara",
                    Email = "orhan.kara@teyas.com.tr",
                    RoleId = 1,
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 11, 18, 10, 15, 0)
                },
                new User
                {
                    Username = "fatma.celik",
                    PasswordHash = "Fck2024!",
                    FullName = "Fatma Çelik",
                    Email = "fatma.celik@teyas.com.tr",
                    RoleId = 1,
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
                    RequestTypeId = 1,
                    PriorityId = 3,
                    StatusId = 3,
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
                    RequestTypeId = 2,
                    PriorityId = 2,
                    StatusId = 2,
                    CreatedByUserId = 2,
                    CreatedDate = new DateTime(2025, 1, 8, 9, 30, 0)
                },
                new Request
                {
                    RequestNo = "TYS-2025-00000003",
                    Title = "Mobil uygulama Android 15 uyumluluk hatası",
                    Description = "Diyanet mobil uygulaması Android 15'te açılmıyor.",
                    RequestTypeId = 1,
                    PriorityId = 3,
                    StatusId = 2,
                    CreatedByUserId = 4,
                    CreatedDate = new DateTime(2025, 1, 10, 14, 45, 0)
                },
                new Request
                {
                    RequestNo = "TYS-2025-00000004",
                    Title = "VPN bağlantı hatası",
                    Description = "Taşra teşkilatından VPN ile merkeze bağlanırken hata alınıyor.",
                    RequestTypeId = 3,
                    PriorityId = 3,
                    StatusId = 3,
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
                    RequestTypeId = 1,
                    PriorityId = 2,
                    StatusId = 1,
                    CreatedByUserId = 4,
                    CreatedDate = new DateTime(2025, 1, 16, 14, 30, 0)
                }
            );

            context.SaveChanges();
        }
    }
}
