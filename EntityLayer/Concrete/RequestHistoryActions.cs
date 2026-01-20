using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public static class RequestHistoryActions
    {
        public const string Olusturuldu = "Oluşturuldu";
        public const string Guncellendi = "Güncellendi";
        public const string DurumDegistirildi = "Durumu Değiştirildi";
        public const string Onaylandi = "Onaylandı";
        public const string Reddedildi = "Reddedildi";
        public const string Gonderildi = "Gönderildi";
    }

    public static class RequestHistoryFields
    {
        public const string Durum = "Status";
        public const string Baslik = "Title";
        public const string Aciklama = "Description";
        public const string TalepTuru = "RequestType";
        public const string Oncelik = "Priority";
    }
}
