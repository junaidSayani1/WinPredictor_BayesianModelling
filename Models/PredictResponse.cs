namespace Dota2WinPredictor.Models
{
    public class PredictResponse
    {
        public List<HeroDTO>? TeamA { get; set; }
        public List<HeroDTO>? TeamB { get; set; }

        public double? TeamAAdvantage { get; set; }

        public double? TeamAWinChance { get; set; }

        public double? TeamBWinChance { get; set; }

    }
}
