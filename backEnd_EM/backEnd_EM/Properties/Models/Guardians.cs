using System;
using System.ComponentModel.DataAnnotations;


namespace backEnd_EM.Properties.Models
{
    public class Guardians
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public long Number { get; set; }

        public int? AthletesId { get; set; }
    }
}