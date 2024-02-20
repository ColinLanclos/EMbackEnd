using System;
using System.ComponentModel.DataAnnotations;
namespace backEnd_EM.Properties.Models
{
    public class ProgressTracker
    {
        [Key]
        public int ProgressTrackerId { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly Date { get; set; }

        public string WorkOut { get; set; } = string.Empty;

        public float Value_Dor_Date { get; set; }

        public int? AthletesId { get; set; }
    }
}

