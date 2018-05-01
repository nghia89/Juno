using Juno.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juno.Model.Models
{
    [Table("Products")]
    public class Product : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Bi danh")]
        public string Alias { set; get; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Required]
        public int CategoryID { set; get; }

        [MaxLength(256)]
        public string DefaultImage { set; get; }

        [MaxLength(256)]
        public string HoverImage { get; set; }

        [MaxLength(256)]
        public string DeatailImage { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }

        public decimal Price { set; get; }

        public decimal? PromotionPrice { set; get; }

        public int? Warranty { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        public string Tags { set; get; }

        public int Quantity { set; get; }

        public decimal OriginalPrice { set; get; }

        [StringLength(200)]
        public string KieuDang { get; set; }

        [StringLength(200)]
        public string ChatLieu { get; set; }

        [StringLength(200)]
        public string MauSac { get; set; }

        [StringLength(200)]
        public string KichCo { get; set; }

        [StringLength(200)]
        public string DoCao { get; set; }

        public bool BestWeek { get; set; }

        public bool NewBest { get; set; }



        public virtual IEnumerable<FavoriteColor> FavoriteColors { set; get; }

        public virtual IEnumerable<Picture> Pictures { set; get; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { set; get; }

        public virtual IEnumerable<Size> Sizes { set; get; }

        public virtual IEnumerable<ProductTag> ProductTags { set; get; }

    }
}