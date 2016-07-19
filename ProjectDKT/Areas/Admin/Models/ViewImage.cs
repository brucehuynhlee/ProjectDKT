using ProjectDKT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectDKT.Areas.Admin.Models
{
    public class ViewImage
    {
        
        public int ImageID { get; set; }
        public HttpPostedFileBase UrlResizeImage{ get; set; }
        public HttpPostedFileBase UrlBigImage { get; set; }
        public System.DateTime DateUpload { get; set; }
        public bool IsLibrary { get; set; }
        public Nullable<int> CatalogID { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}