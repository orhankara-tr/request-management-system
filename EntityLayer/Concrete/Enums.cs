using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public enum UserRole
    {
        [Display(Name = "Kullanıcı")]
        Kullanici = 1,

        [Display(Name = "Yönetici")]
        Yonetici = 2,

        [Display(Name = "Admin")]
        Admin = 3
    }

    public enum RequestType
    {
        [Display(Name = "Lütfen Seçin")]
        None = 0,

        [Display(Name = "Hata Raporu")]
        HataRaporu = 1,

        [Display(Name = "Özellik Talebi")]
        OzellikTalebi = 2,

        [Display(Name = "Destek")]
        Destek = 3,

        [Display(Name = "Diğer")]
        Diger = 4
    }

    public enum Priority
    {
        [Display(Name = "Lütfen Seçin")]
        None = 0,

        [Display(Name = "Düşük")]
        Dusuk = 1,

        [Display(Name = "Orta")]
        Orta = 2,

        [Display(Name = "Yüksek")]
        Yuksek = 3
    }

    public enum RequestStatus
    {
        [Display(Name = "Taslak")]
        Taslak = 1,

        [Display(Name = "Onay Bekliyor")]
        OnayBekliyor = 2,

        [Display(Name = "Onaylandı")]
        Onaylandi = 3,

        [Display(Name = "Reddedildi")]
        Reddedildi = 4,

        [Display(Name = "Devam Ediyor")]
        DevamEdiyor = 5,

        [Display(Name = "Tamamlandı")]
        Tamamlandi = 6
    }
}

