namespace ProjectASP.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public int ScorePlayerOne { get; set; }
        public int ScorePlayerTwo { get; set; }
        public bool IsPlayed { get; set; }
        public DateTime Datum { get; set; } 
    }
}
