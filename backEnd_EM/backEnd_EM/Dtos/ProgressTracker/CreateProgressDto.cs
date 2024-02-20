using System;
using System.ComponentModel.DataAnnotations;
namespace backEnd_EM.Properties.Models
{
    public class CreateProgressDto
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Date { get; set; } = string.Empty;

        public string WorkOut { get; set; } = string.Empty;

        public float Value_Dor_Date { get; set; }
    }
}