using System.ComponentModel.DataAnnotations;

namespace backEnd_EM.Dtos.Analyse
{
    public class AnalyseUpdateDto
    {
        public string videoURL { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string BreakDown { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Date { get; set; }
    }
}