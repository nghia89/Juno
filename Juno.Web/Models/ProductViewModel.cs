using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juno.Web.Models
{
    public class ProductViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Code { get; set; }

        public string Alias { set; get; }

        public string MetaTitle { get; set; }

        public int CategoryID { set; get; }

        public string DefaultImage { set; get; }
        
        public string HoverImage { get; set; }

        public string DeatailImage { get; set; }

        public string MoreImages { set; get; }

        public decimal Price { set; get; }

        public decimal? PromotionPrice { set; get; }

        public int? Warranty { set; get; }

        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        public string Tags { set; get; }

        public int Quantity { set; get; }

        public decimal OriginalPrice { set; get; }

        public string KieuDang { get; set; }

        public string ChatLieu { get; set; }

        public string MauSac { get; set; }

        public string KichCo { get; set; }

        public string DoCao { get; set; }

        public bool BestWeek { get; set; }

        public bool NewBest { get; set; }


        public virtual IEnumerable<FavoriteColorViewModel> FavoriteColors { set; get; }

        public virtual IEnumerable<PictureViewModel> Pictures { set; get; }

        public virtual ProductCategoryViewModel ProductCategory { set; get; }

        public virtual IEnumerable<SizeViewModel> Sizes { set; get; }

        public virtual IEnumerable<ProductTagViewModel> ProductTags { set; get; }
    }
}