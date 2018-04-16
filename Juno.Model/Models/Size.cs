using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juno.Model.Models
{
    [Table("Sizes")]
   public class Size
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int PoductID { get; set; }

        public byte Sizes { get; set; }
      
        [ForeignKey("PoductID")]
        public virtual Product Product { set; get; }
    }
}
