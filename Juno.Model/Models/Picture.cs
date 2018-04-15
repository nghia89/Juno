using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juno.Model.Models
{
    [Table("Picture")]
    public partial class Picture
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string image { get; set; }

        public int? DisplayOrder { get; set; }

        public long? ProductID { get; set; }

        public virtual Product Product { set; get; }
    }
}