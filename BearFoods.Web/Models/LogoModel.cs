using System;
using System.ComponentModel.DataAnnotations;

namespace BearFoods.Web.Models
{
    public class LogoModel
    {
        [Display(Name = "Batch Nr")]
        public int BatchNr { get; set; }
        [Display(Name ="Produktionsdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Production { get; set; }
    }
}
