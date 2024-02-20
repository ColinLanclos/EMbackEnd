using backEnd_EM.Dtos.Analyse;
using backEnd_EM.Properties.Models;

namespace backEnd_EM.Mapper
{

    public static class AnalyseMapper
    {
        public static AnalyseDto ToAnalyseDto(this Analyse analyseModel)
        {
            return new AnalyseDto
            {
                videoURL = analyseModel.videoURL,
                Title = analyseModel.Title,
                BreakDown = analyseModel.BreakDown,
                Day = analyseModel.Day,
                Date = analyseModel.Date
            };
        }

        public static Analyse ToAnalyseFromCreate(this CreateAnalyseDto createAnalyseDto, int id)
        {
            return new Analyse
            {
                videoURL = createAnalyseDto.videoURL,
                Title = createAnalyseDto.Title,
                BreakDown = createAnalyseDto.BreakDown,
                Day = createAnalyseDto.Day,
                Date = DateOnly.Parse(createAnalyseDto.Date),
                AthletesId = id
            };
        }

        public static DayListDto ToDayList(this Analyse analyseModel)
        {
            return new DayListDto
            {
                Day = analyseModel.Day
            };
        }
    }
}