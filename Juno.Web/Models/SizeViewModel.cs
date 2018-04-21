using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Juno.Web.Models
{
    public class SizeViewModel
    {
        public int ID { get; set; }

        public int PoductID { get; set; }

        public byte Sizes { get; set; }

        public virtual ProductViewModel Product { set; get; }
    }
}