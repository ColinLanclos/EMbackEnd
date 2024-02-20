using System;
using System.ComponentModel.DataAnnotations;
namespace backEnd_EM.Properties.Models
{
    public class UpdateProgressTrackerDto
    {
        public string WorkOut { get; set; } = string.Empty;
        public float Value_Dor_Date { get; set; }
    }
}