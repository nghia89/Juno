using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juno.Web.Models
{
    public class FavoriteColorViewModel
    {
        public int ID { get; set; }
        public string Color { get; set; }

        public string CodeColor { get; set; }

        public int PoductID { get; set; }
        public string Images { set; get; }
        public virtual ProductViewModel Product { set; get; }
    }
}