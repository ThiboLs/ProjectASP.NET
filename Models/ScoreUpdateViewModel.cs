namespace ProjectASP.Models
{
    public class ScoreUpdateViewModel
    {
        public int MatchId { get; set; }
        public int ScorePlayerOne { get; set; }
        public int ScorePlayerTwo { get; set; }
        public bool IsPlayed { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}
