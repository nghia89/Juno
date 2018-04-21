using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juno.Web.Models
{
    public class PictureViewModel
    {
        public int ID { get; set; }

        public string image { get; set; }

        public int? DisplayOrder { get; set; }

        public int ProductID { get; set; }

        public virtual ProductViewModel Product { set; get; }
    }
}