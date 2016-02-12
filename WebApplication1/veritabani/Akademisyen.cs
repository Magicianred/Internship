//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.veritabani
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Akademisyen
    {
        public int aka_id { get; set; }
        [Required]
        public string aka_ad { get; set; }
        [Required]
        public string aka_soyad { get; set; }
        [Required]
        public string aka_tc { get; set; }
        [Required]
        public bool aka_sorumlumu { get; set; }
        [Required]
        public string bol_kod { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string aka_parola { get; set; }
        [Required]
      
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}", ErrorMessage = "Yanl�� Mail Format�")]
        public string aka_mail { get; set; }
    
        public virtual Bolum Bolum { get; set; }


    }
}
