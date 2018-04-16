using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juno.Model.Models
{
    [Table("FavoriteColors")]
    public partial class FavoriteColor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string CodeColor { get; set; }

        public int PoductID { get; set; }

        [MaxLength(256)]
        public string Images { set; get; }
        [ForeignKey("PoductID")]
        public virtual Product Product { set; get; }
    }
}