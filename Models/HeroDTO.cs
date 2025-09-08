namespace Dota2WinPredictor.Models
{
    public class HeroDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        //public string AttackType { get; set; }
        public string? Icon { get; set; }

        public string? Potrait { get; set; }

        public int? PickCount { get; set; }

        public int? WinCount { get; set; }

        public double? WinRate { get; set; }

        public double? AdjustedWinRate { get; set; }

        public double? LogOdds { get; set; }
    }
}
