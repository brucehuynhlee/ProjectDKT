//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectDKT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class News
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        [UIHint("Image")]
        public string UrlImage { get; set; }
        public string UrlLink { get; set; }
        public System.DateTime DateUpload { get; set; }
        public bool IsActive { get; set; }
        public int TypeNewsID { get; set; }
    
        public virtual Typenew Typenew { get; set; }
    }
}
