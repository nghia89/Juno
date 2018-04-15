using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juno.Model.Models
{
    [Table("SizeIAndImage")]
    public partial class SizeIAndImage
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string CodeColor { get; set; }

        public long? PoductID { get; set; }

        public byte? Size { get; set; }

        [Column(TypeName = "image")]
        public byte[] Images { get; set; }
        public virtual Product Product { set; get; }
    }
}