using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juno.Model.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string URL { set; get; }

        public int? DisplayOrder { set; get; }

        [StringLength(250)]
        public string image { get; set; }

        [Required]
        public int GroupID { set; get; }

        public int? TypeID { get; set; }

        public long ParentID { get; set; }

        [ForeignKey("GroupID")]
        public virtual MenuGroup MenuGroup { set; get; }//khoá ngoại

        [MaxLength(10)]
        public string Target { set; get; }


        public bool Status { set; get; }
    }
}