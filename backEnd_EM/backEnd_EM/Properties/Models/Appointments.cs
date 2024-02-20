using System;
using System.ComponentModel.DataAnnotations; // Required for the [Key] attribute

namespace backEnd_EM.Properties.Models
{
    public class Appointments
    {
        [Key]
        public int AppointmentId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string TypeApointment { get; set; } = string.Empty;

        public float Time { get; set; }

        public int? AthletesId { get; set; }
    }
}
