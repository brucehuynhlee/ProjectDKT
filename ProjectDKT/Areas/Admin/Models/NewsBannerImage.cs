using ProjectDKT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDKT.Areas.Admin.Models
{
    public class NewsBannerImage
    {
        public List<News> New { get; set; }
        public List<Banner> Banner { get; set; }
    }
}