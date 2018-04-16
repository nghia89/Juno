using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juno.Model.Models
{
    [Table("provinces")]
    public partial class province
    {
        [Key]
        [StringLength(5)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string provinceid { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(30)]
        public string type { get; set; }
    }
}