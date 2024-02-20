using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using backEnd_EM.Models;
namespace backEnd_EM.Properties.Models
{
    public class Athletes
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Guardian { get; set; } = string.Empty;

        public string Birthday { get; set; } = string.Empty;

        public string Classification { get; set; } = string.Empty;

        public string Height { get; set; } = string.Empty;

        public float Weight { get; set; }

        public string School { get; set; } = string.Empty;

        public long Phone { get; set; }

        public string Email { get; set; } = string.Empty;

        public byte[] Password { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];

        public string? PasswordResetToken { get; set; }

        public DateTime? ResetTokenExperies { get; set; }

        public ICollection<Analyse> Analyses { get; set; }

        public ICollection<Appointments> Appointments { get; set; }

        public ICollection<ProgressTracker> ProgressTrackers { get; set; }

        public ICollection<Analytics> Analytics { get; set; }
    }
}