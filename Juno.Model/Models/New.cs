using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Juno.Model.Models
{

    [Table("News")]
    public partial class New
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        public string TitleNew { get; set; }

        public string TitleImages { get; set; }

        public string HTMLContent { get; set; }

        public string Describe { get; set; }

        public string NamePost { get; set; }

        public DateTime? DatePost { get; set; }

        public long? CategoryNewID { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? ShowHomePage { get; set; }

        public bool? Popular { get; set; }

        public bool? ViewBest { get; set; }

        public bool? Trash { get; set; }

        [StringLength(200)]
        public string Update_by { get; set; }

        public int? View { get; set; }

        public bool? Status { get; set; }

        [StringLength(200)]
        public string LinkNews { get; set; }
    }
}