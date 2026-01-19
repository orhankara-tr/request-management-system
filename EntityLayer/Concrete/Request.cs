using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    [Table("Requests")]
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [StringLength(20)]
        public string RequestNo { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }

        [Required]
        public RequestType RequestTypeId { get; set; } = RequestType.Diger; // 1=Hata, 2=Özellik, 3=Destek, 4=Diğer

        [Required]
        public Priority PriorityId { get; set; } = Priority.Dusuk;  // 1=Düşük, 2=Orta, 3=Yüksek

        [Required]
        public RequestStatus StatusId { get; set; } = RequestStatus.Taslak;  // 1=Taslak, 2=Onay Bekliyor, 3=Onaylandı, 4=Reddedildi

        [Required]
        public int CreatedByUserId { get; set; }

        public int? ProcessedByUserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string ApprovalNotes { get; set; }  // Yöneticinin red ederken yazdığı notlar

        public DateTime? ProcessedDate { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey("ProcessedByUserId")]
        public virtual User ProcessedByUser { get; set; }

        public virtual ICollection<RequestHistory> RequestHistories { get; set; }
    }
}
