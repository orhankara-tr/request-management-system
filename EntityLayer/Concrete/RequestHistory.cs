using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    [Table("RequestHistories")]
    public class RequestHistory
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int UserId { get; set; }  // İşlemi yapan kullanıcı

        [Required]
        [StringLength(100)]
        public string Action { get; set; }  // "Oluşturuldu", "Durumu Değiştirildi", "Onaylandı", "Reddedildi"

        [StringLength(100)]
        public string FieldName { get; set; }  // Değişen alan adı:  "Status", "AssignedTo", "Priority"

        [StringLength(100)]
        public string OldValue { get; set; }  // Eski değer

        [StringLength(100)]
        public string NewValue { get; set; }  // Yeni değer

        [Column(TypeName = "nvarchar(MAX)")]
        public string Notes { get; set; }  // Ek açıklama (red nedeni vb.)

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        [ForeignKey("RequestId")]
        public virtual Request Request { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
